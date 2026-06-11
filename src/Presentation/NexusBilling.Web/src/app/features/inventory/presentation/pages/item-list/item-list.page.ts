import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ItemService } from '../../data/item.service';
import { Item, ItemFilter, ItemSortField } from '../../domain/item.model';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <!-- Breadcrumb -->
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Artículos</span>
    </nav>

    <!-- Page header -->
    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Lista de Artículos</h1>
        <p class="nx-page-subtitle">{{ filtered().length }} artículos</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">Ajuste Inventario</button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Artículo
        </button>
      </div>
    </div>

    <!-- Filter bar -->
    <div class="filter-bar">
      <!-- Search -->
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:380px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input id="item-search" type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar por No. o descripción…" [(ngModel)]="searchText" (ngModelChange)="applyFilter()" />
      </div>

      <!-- Category -->
      <select id="item-category-filter" class="nx-select" style="width:190px;" [(ngModel)]="selectedCategory" (ngModelChange)="applyFilter()">
        <option value="">Todas las categorías</option>
        @for (cat of categories; track cat) {
          <option [value]="cat">{{ cat }}</option>
        }
      </select>

      <!-- Type -->
      <select id="item-type-filter" class="nx-select" style="width:160px;" [(ngModel)]="selectedType" (ngModelChange)="applyFilter()">
        <option value="">Todos los tipos</option>
        <option value="Inventory">Inventario</option>
        <option value="Service">Servicio</option>
        <option value="Non-Inventory">No Inventario</option>
      </select>

      <!-- Blocked toggle -->
      <select id="item-blocked-filter" class="nx-select" style="width:160px;" [(ngModel)]="showBlocked" (ngModelChange)="applyFilter()">
        <option value="active">Solo activos</option>
        <option value="all">Todos</option>
        <option value="blocked">Solo bloqueados</option>
      </select>

      @if (searchText || selectedCategory || selectedType || showBlocked !== 'active') {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">Limpiar</button>
      }
    </div>

    <!-- KPI tiles -->
    <div style="display:grid;grid-template-columns:repeat(4,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-4);">
      <div class="nx-stat">
        <span class="nx-stat__label">Total Artículos</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">{{ allItems().length }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">En Stock</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-positive);">{{ allItems().filter(i => (i.inventory ?? 0) > 0).length }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Servicios</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">{{ allItems().filter(i => i.type === 'Service').length }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Bloqueados</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-negative);">{{ allItems().filter(i => i.blocked).length }}</span>
      </div>
    </div>

    <!-- Table -->
    <div class="nx-card" style="overflow:hidden;">
      @if (filtered().length === 0) {
        <div class="nx-empty">
          <div class="nx-empty__icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/></svg>
          </div>
          <p class="nx-empty__title">No se encontraron artículos</p>
          <p class="nx-empty__text">Ajusta los filtros de búsqueda.</p>
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="clearFilters()">Limpiar filtros</button>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Lista de artículos">
            <thead>
              <tr>
                <th class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                <th class="sortable" (click)="setSort('description')">Descripción {{ si('description') }}</th>
                <th>Tipo</th>
                <th>Categoría</th>
                <th>U/M Base</th>
                <th class="nx-th--num sortable" (click)="setSort('unitPrice')">Precio Venta {{ si('unitPrice') }}</th>
                <th class="nx-th--num sortable" (click)="setSort('unitCost')">Costo Unitario {{ si('unitCost') }}</th>
                <th class="nx-th--num sortable" (click)="setSort('inventory')">Inventario {{ si('inventory') }}</th>
                <th>Estado</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (item of filtered(); track item.no) {
                <tr>
                  <td class="nx-td--doc">
                    <a [routerLink]="['/inventory', item.no]" class="nx-link">{{ item.no }}</a>
                  </td>
                  <td>
                    <div style="font-weight:var(--nx-weight-medium);color:var(--nx-text-strong);">{{ item.description }}</div>
                    @if (item.description2) {
                      <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ item.description2 }}</div>
                    }
                  </td>
                  <td>
                    <span class="nx-badge nx-badge--outline">{{ typeLabel(item.type) }}</span>
                  </td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ item.itemCategoryCode || '—' }}</td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ item.baseUnitOfMeasure }}</td>
                  <td class="nx-td--num nx-num">RD$ {{ item.unitPrice | number:'1.2-2' }}</td>
                  <td class="nx-td--num nx-num" style="color:var(--nx-text-muted);">RD$ {{ item.unitCost | number:'1.2-2' }}</td>
                  <td class="nx-td--num">
                    @if (item.type === 'Service') {
                      <span style="color:var(--nx-text-faint);font-size:var(--nx-text-sm);">—</span>
                    } @else {
                      <span class="nx-num" [style.color]="(item.inventory ?? 0) === 0 ? 'var(--nx-money-negative)' : ''">
                        {{ (item.inventory ?? 0) | number:'1.0-0' }}
                      </span>
                    }
                  </td>
                  <td>
                    @if (item.blocked) {
                      <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
                    } @else {
                      <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
                    }
                  </td>
                  <td>
                    <a [routerLink]="['/inventory', item.no]" class="nx-iconbtn nx-iconbtn--sm" aria-label="Ver artículo">
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 18l6-6-6-6"/></svg>
                    </a>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>
  `,
  styles: [`
    :host { display: block; }
    .filter-bar { display:flex;align-items:center;gap:var(--nx-space-3);flex-wrap:wrap;margin-bottom:var(--nx-space-4); }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
  `]
})
export class ItemListPage implements OnInit {
  private readonly svc = inject(ItemService);

  searchText = '';
  selectedCategory = '';
  selectedType = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'active';
  sortField: ItemSortField = 'no';
  sortAsc = true;

  allItems = signal<Item[]>([]);
  filtered = signal<Item[]>([]);
  categories: string[] = [];

  ngOnInit(): void {
    this.allItems.set(this.svc.getAll());
    this.categories = this.svc.categories();
    this.applyFilter();
  }

  setSort(f: ItemSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
    this.applyFilter();
  }

  si(f: ItemSortField): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.selectedCategory = '';
    this.selectedType = '';
    this.showBlocked = 'active';
    this.applyFilter();
  }

  applyFilter(): void {
    const blocked = this.showBlocked === 'all' ? null : this.showBlocked === 'blocked';
    this.filtered.set(this.svc.filter(
      { search: this.searchText || undefined, itemCategoryCode: this.selectedCategory || undefined, type: this.selectedType || undefined, blocked },
      this.sortField, this.sortAsc
    ));
  }

  typeLabel(t?: string): string {
    return { 'Inventory': 'Inventario', 'Service': 'Servicio', 'Non-Inventory': 'No Inventario' }[t ?? ''] ?? (t ?? '—');
  }
}
