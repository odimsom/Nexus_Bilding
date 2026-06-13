import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ItemService, ItemListItem, ItemFormData } from '../../../data/item.service';
import { ItemSortField } from '../../../domain/item.model';
import { ApiService } from '../../../../../core/services/api.service';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Productos</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Lista de Productos</h1>
        <p class="nx-page-subtitle">
          @if (svc.loading()) { Cargando… }
          @else { {{ svc.totalItems() }} productos }
        </p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">Ajuste Inventario</button>
        <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openModal()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Producto
        </button>
      </div>
    </div>

    <div class="filter-bar">
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:380px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar por No. o descripción…"
          [(ngModel)]="searchText" (ngModelChange)="onSearchChange()" />
      </div>

      <select class="nx-select" style="width:160px;" [(ngModel)]="typeFilter" (ngModelChange)="applyTypeFilter()">
        <option value="">Todos los tipos</option>
        <option value="Inventory">Inventario</option>
        <option value="Service">Servicio</option>
        <option value="Non-Inventory">No Inventario</option>
      </select>

      <select class="nx-select" style="width:160px;" [(ngModel)]="showBlocked" (ngModelChange)="onFilterChange()">
        <option value="active">Solo activos</option>
        <option value="all">Todos</option>
        <option value="blocked">Solo bloqueados</option>
      </select>

      @if (searchText || showBlocked !== 'active' || typeFilter) {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">Limpiar</button>
      }
    </div>

    @if (svc.error()) {
      <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
        {{ svc.error() }}
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="reload()" style="margin-left:auto;">Reintentar</button>
      </div>
    }

    <!-- KPI tiles -->
    <div class="kpi-row">
      <div class="nx-stat">
        <span class="nx-stat__label">Total Productos</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">{{ svc.totalItems() }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">En Stock</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-positive);">{{ svc.items().filter(i => i.inventory > 0).length }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Sin Stock</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-negative);">{{ svc.items().filter(i => i.inventory === 0 && !i.blocked).length }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Bloqueados</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">{{ svc.items().filter(i => i.blocked).length }}</span>
      </div>
    </div>

    <div class="nx-card" style="overflow:hidden;">
      @if (svc.loading()) {
        <div class="nx-empty">
          <div class="nx-spinner"></div>
          <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando productos…</p>
        </div>
      } @else if (visibleItems().length === 0) {
        <div class="nx-empty">
          <div class="nx-empty__icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/></svg>
          </div>
          <p class="nx-empty__title">No se encontraron productos</p>
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="clearFilters()">Limpiar filtros</button>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Lista de productos">
            <thead>
              <tr>
                <th class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                <th class="sortable" (click)="setSort('description')">Descripción {{ si('description') }}</th>
                <th>Tipo</th>
                <th>Categoría</th>
                <th>U/M</th>
                <th class="num sortable" (click)="setSort('unitPrice')">Precio {{ si('unitPrice') }}</th>
                <th class="num sortable" (click)="setSort('unitCost')">Costo {{ si('unitCost') }}</th>
                <th class="num sortable" (click)="setSort('inventory')">Inventario {{ si('inventory') }}</th>
                <th>Estado</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (item of visibleItems(); track item.no) {
                <tr>
                  <td class="nx-td--doc">
                    <a [routerLink]="['/inventory', item.no]" class="nx-link">{{ item.no }}</a>
                  </td>
                  <td style="font-weight:var(--nx-weight-medium);color:var(--nx-text-strong);">{{ item.description }}</td>
                  <td>
                    <span class="nx-badge" [class]="typeBadge(item.type)">{{ typeLabel(item.type) }}</span>
                  </td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ item.itemCategoryCode || '—' }}</td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ item.baseUnitOfMeasure }}</td>
                  <td class="num nx-num">{{ item.unitPrice | currency:'DOP':'':'1.2-2' }}</td>
                  <td class="num nx-num" style="color:var(--nx-text-muted);">{{ item.unitCost | currency:'DOP':'':'1.2-2' }}</td>
                  <td class="num">
                    <span class="nx-num" [style.color]="item.inventory === 0 ? 'var(--nx-money-negative)' : ''">
                      {{ item.inventory | number:'1.0-0' }}
                    </span>
                  </td>
                  <td>
                    @if (item.blocked) {
                      <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
                    } @else {
                      <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
                    }
                  </td>
                  <td>
                    <a [routerLink]="['/inventory', item.no]" class="nx-iconbtn nx-iconbtn--sm" aria-label="Ver producto">
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 18l6-6-6-6"/></svg>
                    </a>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>

        @if (svc.totalPages() > 1) {
          <div style="display:flex;align-items:center;justify-content:center;gap:var(--nx-space-2);padding:var(--nx-space-3) var(--nx-space-4);border-top:1px solid var(--nx-border);">
            <button class="nx-btn nx-btn--ghost nx-btn--sm" [disabled]="svc.currentPage() <= 1" (click)="goToPage(svc.currentPage() - 1)">← Anterior</button>
            <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Página {{ svc.currentPage() }} de {{ svc.totalPages() }}</span>
            <button class="nx-btn nx-btn--ghost nx-btn--sm" [disabled]="svc.currentPage() >= svc.totalPages()" (click)="goToPage(svc.currentPage() + 1)">Siguiente →</button>
          </div>
        }
      }
    </div>

    <!-- Modal: Nuevo Producto -->
    @if (showModal()) {
      <div class="modal-backdrop" (click)="closeModal()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Nuevo Producto</h2>
            <button class="nx-iconbtn" (click)="closeModal()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (modalError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">
                {{ noSeriesMissing() ? 'No hay una secuencia numérica configurada para productos (ITEM).' : modalError() }}
                @if (noSeriesMissing()) {
                  <br />
                  <a class="nx-link" style="font-size:var(--nx-text-sm);cursor:pointer;"
                     (click)="goConfigureSeries()">
                    Configurar series → Configuración
                  </a>
                }
              </div>
            }
            <div class="form-grid">
              <div class="nx-field">
                <label class="nx-label">No. Producto</label>
                <div style="display:flex;gap:var(--nx-space-2);">
                  <input
                    class="nx-input"
                    style="font-family:var(--nx-font-mono);flex:1;"
                    [(ngModel)]="form.no"
                    [placeholder]="loadingNextNo() ? 'Cargando…' : previewNo() || '(secuencia)'"
                    autocomplete="off"
                  />
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" [disabled]="loadingNextNo()" (click)="fetchNextNo()" title="Obtener siguiente número">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
                  </button>
                </div>
                
              </div>
              <div class="nx-field">
                <label class="nx-label">Tipo *</label>
                <select class="nx-select" [(ngModel)]="form.type">
                  <option value="Inventory">Inventario</option>
                  <option value="Service">Servicio</option>
                  <option value="Non-Inventory">No Inventario</option>
                </select>
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Descripción *</label>
                <input class="nx-input" [(ngModel)]="form.description" />
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Descripción 2</label>
                <input class="nx-input" [(ngModel)]="form.description2" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Unidad de Medida Base</label>
                <input class="nx-input" [(ngModel)]="form.baseUnitOfMeasure" placeholder="UND, KG, L…" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Categoría</label>
                <input class="nx-input" [(ngModel)]="form.itemCategoryCode" placeholder="ELECTRONICO, ALIMENTOS…" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Precio de Venta (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.unitPrice" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Costo Unitario (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.unitCost" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Costo Estándar (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.standardCost" />
              </div>
              <div class="nx-field">
                <label class="nx-label">No. Proveedor</label>
                <input class="nx-input" [(ngModel)]="form.vendorNo" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Grupo Contab. Inventario</label>
                <select class="nx-select" [(ngModel)]="form.inventoryPostingGroup">
                  <option value="">— Seleccionar —</option>
                  <option>FINISHED</option><option>RAW MAT</option><option>RESALE</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Grupo Contab. General Prod.</label>
                <select class="nx-select" [(ngModel)]="form.genProdPostingGroup">
                  <option value="">— Seleccionar —</option>
                  <option>RETAIL</option><option>SERVICES</option><option>WHOLESALE</option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeModal()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveItem()">
              @if (saving()) { Guardando… } @else { Guardar Producto }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .filter-bar { display:flex;align-items:center;gap:var(--nx-space-3);flex-wrap:wrap;margin-bottom:var(--nx-space-4); }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
    .num { text-align:right; font-variant-numeric:tabular-nums; }
    .kpi-row { display:grid;grid-template-columns:repeat(4,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-4); }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite; }
    @keyframes spin { to { transform:rotate(360deg); } }

    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(720px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
})
export class ItemListPage implements OnInit {
  readonly svc    = inject(ItemService);
  readonly noSeriesMissing = signal(false);
  private readonly router = inject(Router);
  private readonly api = inject(ApiService);

  loadingNextNo = signal(false);
  previewNo = signal('');

  searchText = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'active';
  typeFilter = '';
  sortField: ItemSortField = 'no';
  sortAsc = true;

  showModal = signal(false);
  saving = signal(false);
  modalError = signal<string | null>(null);

  form: ItemFormData = this.emptyForm();

  private _searchTimeout?: ReturnType<typeof setTimeout>;

  visibleItems(): ItemListItem[] {
    const sorted = this.svc.sorted(this.sortField, this.sortAsc);
    if (!this.typeFilter) return sorted;
    return sorted.filter(i => i.type === this.typeFilter);
  }

  sorted(): ItemListItem[] { return this.svc.sorted(this.sortField, this.sortAsc); }

  ngOnInit(): void { this.reload(); }

  reload(): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked });
  }

  onSearchChange(): void {
    clearTimeout(this._searchTimeout);
    this._searchTimeout = setTimeout(() => this.reload(), 350);
  }

  onFilterChange(): void { this.reload(); }
  applyTypeFilter(): void { /* client-side filter via visibleItems() */ }

  setSort(f: ItemSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: ItemSortField): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.showBlocked = 'active';
    this.typeFilter = '';
    this.reload();
  }

  goToPage(page: number): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked, page });
  }

  typeLabel(t: string): string {
    return { Inventory: 'Inventario', Service: 'Servicio', 'Non-Inventory': 'No Inv.' }[t] ?? t ?? '—';
  }

  typeBadge(t: string): string {
    if (t === 'Service') return 'nx-badge nx-badge--info';
    if (t === 'Non-Inventory') return 'nx-badge nx-badge--warn';
    return 'nx-badge nx-badge--outline';
  }

  openModal(): void {
    this.form = this.emptyForm();
    this.modalError.set(null);
    this.noSeriesMissing.set(false);
    this.previewNo.set('');
    this.showModal.set(true);
    this.fetchNextNo();
  }

  async fetchNextNo(): Promise<void> {
    this.loadingNextNo.set(true);
    try {
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>('administration/no-series/ITEM/next'));
      this.previewNo.set(res.nextNo);
      this.form.no = res.nextNo;
    } catch {
      this.previewNo.set('');
      this.form.no = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  goConfigureSeries(): void {
    this.closeModal();
    this.router.navigate(['/settings'], { queryParams: { tab: 'sequences', returnTo: '/inventory' } });
  }

  closeModal(): void { this.showModal.set(false); }

  async saveItem(): Promise<void> {
    if (!this.form.description?.trim()) {
      this.modalError.set('La descripción del producto es obligatoria.');
      return;
    }
    this.saving.set(true);
    this.modalError.set(null);
    this.noSeriesMissing.set(false);
    try {
      const no = await this.svc.create(this.form);
      this.closeModal();
      this.router.navigate(['/inventory', no]);
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? e?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('series')) {
        this.noSeriesMissing.set(true);
        this.modalError.set('No hay una secuencia de numeración configurada para productos.');
      } else if (msg.toLowerCase().includes('already') || msg.toLowerCase().includes('ya existe') || msg.toLowerCase().includes('duplicate')) {
        this.modalError.set('El número de producto ya está en uso. Haz clic en actualizar para obtener uno disponible.');
        this.fetchNextNo();
      } else if (msg) {
        this.modalError.set(msg);
      } else {
        this.modalError.set('Ocurrió un error al guardar el producto. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }

  private emptyForm(): ItemFormData {
    return {
      no: '', description: '', description2: '', baseUnitOfMeasure: 'UND',
      unitPrice: 0, unitCost: 0, standardCost: 0, type: 'Inventory',
      itemCategoryCode: '', inventoryPostingGroup: 'FINISHED',
      genProdPostingGroup: 'RETAIL', vatProdPostingGroup: '',
      vendorNo: '', vendorItemNo: ''
    };
  }
}
