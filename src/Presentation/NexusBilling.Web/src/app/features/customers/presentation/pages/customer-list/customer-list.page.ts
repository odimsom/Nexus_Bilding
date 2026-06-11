import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../data/customer.service';
import { Customer, CustomerFilter, CustomerSortField } from '../../domain/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <!-- Breadcrumb -->
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Clientes</span>
    </nav>

    <!-- Page header -->
    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Lista de Clientes</h1>
        <p class="nx-page-subtitle">{{ filtered().length }} clientes encontrados</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>
          Exportar
        </button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Cliente
        </button>
      </div>
    </div>

    <!-- Filter bar -->
    <div class="filter-bar">
      <!-- Search -->
      <div class="nx-inputgroup" style="flex:1; min-width:220px; max-width:360px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input
          id="customer-search"
          type="search"
          class="nx-input"
          style="padding-left:36px;"
          placeholder="Buscar por nombre, No., ciudad…"
          [(ngModel)]="searchText"
          (ngModelChange)="onSearchChange()"
        />
      </div>

      <!-- City filter -->
      <select id="customer-city-filter" class="nx-select" style="width:180px;" [(ngModel)]="selectedCity" (ngModelChange)="onFilterChange()">
        <option value="">Todas las ciudades</option>
        @for (city of cities; track city) {
          <option [value]="city">{{ city }}</option>
        }
      </select>

      <!-- Blocked filter -->
      <select id="customer-blocked-filter" class="nx-select" style="width:160px;" [(ngModel)]="showBlocked" (ngModelChange)="onFilterChange()">
        <option value="all">Todos</option>
        <option value="active">Solo activos</option>
        <option value="blocked">Solo bloqueados</option>
      </select>

      <!-- Active tags -->
      @if (activeTagsCount() > 0) {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">
          Limpiar filtros
          <span class="nx-badge nx-badge--info" style="margin-left:4px;">{{ activeTagsCount() }}</span>
        </button>
      }
    </div>

    <!-- Table -->
    <div class="nx-card" style="overflow:hidden;">
      @if (filtered().length === 0) {
        <div class="nx-empty">
          <div class="nx-empty__icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>
          </div>
          <p class="nx-empty__title">No se encontraron clientes</p>
          <p class="nx-empty__text">Intenta ajustar los filtros de búsqueda.</p>
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="clearFilters()">Limpiar filtros</button>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Lista de clientes">
            <thead>
              <tr>
                <th class="sortable" (click)="setSort('no')">
                  No. {{ sortIndicator('no') }}
                </th>
                <th class="sortable" (click)="setSort('name')">
                  Nombre {{ sortIndicator('name') }}
                </th>
                <th>Contacto</th>
                <th class="sortable" (click)="setSort('city')">
                  Ciudad {{ sortIndicator('city') }}
                </th>
                <th>Condición Pago</th>
                <th>Vendedor</th>
                <th class="nx-th--num sortable" (click)="setSort('balance')">
                  Saldo {{ sortIndicator('balance') }}
                </th>
                <th class="nx-th--num sortable" (click)="setSort('balanceDue')">
                  Saldo Vencido {{ sortIndicator('balanceDue') }}
                </th>
                <th>Estado</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (customer of filtered(); track customer.no) {
                <tr>
                  <td class="nx-td--doc">
                    <a [routerLink]="['/customers', customer.no]" class="nx-link">
                      {{ customer.no }}
                    </a>
                  </td>
                  <td style="font-weight:var(--nx-weight-medium); color:var(--nx-text-strong);">
                    {{ customer.name }}
                  </td>
                  <td style="color:var(--nx-text-muted);">{{ customer.contact }}</td>
                  <td>{{ customer.city }}</td>
                  <td style="color:var(--nx-text-muted); font-size:var(--nx-text-sm);">{{ customer.paymentTermsCode }}</td>
                  <td style="color:var(--nx-text-muted); font-size:var(--nx-text-sm);">{{ customer.salespersonCode }}</td>
                  <td class="nx-td--num">
                    <span class="nx-amount" [class.nx-amount--positive]="(customer.balance ?? 0) > 0">
                      RD$ {{ (customer.balance ?? 0) | number:'1.2-2' }}
                    </span>
                  </td>
                  <td class="nx-td--num">
                    <span class="nx-amount" [class.nx-amount--negative]="(customer.balanceDue ?? 0) > 0" [class.nx-amount--zero]="(customer.balanceDue ?? 0) === 0">
                      RD$ {{ (customer.balanceDue ?? 0) | number:'1.2-2' }}
                    </span>
                  </td>
                  <td>
                    @if (customer.blocked) {
                      <span class="nx-badge nx-badge--danger">
                        <span class="nx-badge__dot"></span>Bloqueado
                      </span>
                    } @else {
                      <span class="nx-badge nx-badge--success">
                        <span class="nx-badge__dot"></span>Activo
                      </span>
                    }
                  </td>
                  <td>
                    <a [routerLink]="['/customers', customer.no]" class="nx-iconbtn nx-iconbtn--sm" aria-label="Ver ficha">
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

    .filter-bar {
      display: flex;
      align-items: center;
      gap: var(--nx-space-3);
      flex-wrap: wrap;
      margin-bottom: var(--nx-space-4);
    }

    .sortable {
      cursor: pointer;
      user-select: none;
      white-space: nowrap;
    }
    .sortable:hover { color: var(--nx-text-body); }
  `]
})
export class CustomerListPage implements OnInit {
  private readonly svc = inject(CustomerService);

  searchText = '';
  selectedCity = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'all';
  sortField: CustomerSortField = 'name';
  sortAsc = true;

  filtered = signal<Customer[]>([]);
  cities: string[] = [];

  activeTagsCount = computed(() => {
    let count = 0;
    if (this.searchText) count++;
    if (this.selectedCity) count++;
    if (this.showBlocked !== 'all') count++;
    return count;
  });

  ngOnInit(): void {
    this.cities = this.svc.cities();
    this.applyFilter();
  }

  onSearchChange(): void { this.applyFilter(); }
  onFilterChange(): void { this.applyFilter(); }

  setSort(field: CustomerSortField): void {
    if (this.sortField === field) this.sortAsc = !this.sortAsc;
    else { this.sortField = field; this.sortAsc = true; }
    this.applyFilter();
  }

  sortIndicator(field: CustomerSortField): string {
    if (this.sortField !== field) return '';
    return this.sortAsc ? '↑' : '↓';
  }

  clearFilters(): void {
    this.searchText = '';
    this.selectedCity = '';
    this.showBlocked = 'all';
    this.applyFilter();
  }

  private applyFilter(): void {
    const blocked = this.showBlocked === 'all' ? null : this.showBlocked === 'blocked';
    this.filtered.set(this.svc.filter(
      { search: this.searchText, city: this.selectedCity || undefined, blocked },
      this.sortField,
      this.sortAsc
    ));
  }
}
