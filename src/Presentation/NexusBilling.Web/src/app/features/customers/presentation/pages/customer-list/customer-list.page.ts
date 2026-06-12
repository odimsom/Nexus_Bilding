import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerService, CustomerListItem, CustomerFormData } from '../../../data/customer.service';
import { CustomerSortField } from '../../../domain/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Clientes</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Lista de Clientes</h1>
        <p class="nx-page-subtitle">
          @if (svc.loading()) { Cargando… }
          @else { {{ svc.totalItems() }} clientes encontrados }
        </p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="exportCsv()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>
          Exportar
        </button>
        <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openModal()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Cliente
        </button>
      </div>
    </div>

    <div class="filter-bar">
      <div class="nx-inputgroup" style="flex:1; min-width:220px; max-width:360px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input
          type="search" class="nx-input" style="padding-left:36px;"
          placeholder="Buscar por nombre, No., ciudad…"
          [(ngModel)]="searchText"
          (ngModelChange)="onSearchChange()"
        />
      </div>

      <select class="nx-select" style="width:160px;" [(ngModel)]="showBlocked" (ngModelChange)="onFilterChange()">
        <option value="all">Todos</option>
        <option value="active">Solo activos</option>
        <option value="blocked">Solo bloqueados</option>
      </select>

      @if (activeTagsCount() > 0) {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">
          Limpiar filtros
          <span class="nx-badge nx-badge--info" style="margin-left:4px;">{{ activeTagsCount() }}</span>
        </button>
      }
    </div>

    @if (svc.error()) {
      <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
        {{ svc.error() }}
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="reload()" style="margin-left:auto;">Reintentar</button>
      </div>
    }

    <div class="nx-card" style="overflow:hidden;">
      @if (svc.loading()) {
        <div class="nx-empty">
          <div class="nx-spinner"></div>
          <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando clientes…</p>
        </div>
      } @else if (sorted().length === 0) {
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
                <th class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                <th class="sortable" (click)="setSort('name')">Nombre {{ si('name') }}</th>
                <th class="sortable" (click)="setSort('city')">Ciudad {{ si('city') }}</th>
                <th>Contacto</th>
                <th>Vendedor</th>
                <th>Cond. Pago</th>
                <th class="num sortable" (click)="setSort('balance')">Saldo {{ si('balance') }}</th>
                <th class="num sortable" (click)="setSort('balanceDue')">Saldo Vencido {{ si('balanceDue') }}</th>
                <th>Estado</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (c of sorted(); track c.no) {
                <tr>
                  <td class="nx-td--doc">
                    <a [routerLink]="['/customers', c.no]" class="nx-link">{{ c.no }}</a>
                  </td>
                  <td style="font-weight:var(--nx-weight-medium);color:var(--nx-text-strong);">{{ c.name }}</td>
                  <td>{{ c.city || '—' }}</td>
                  <td style="color:var(--nx-text-muted);">{{ c.contact || '—' }}</td>
                  <td>
                    @if (c.salespersonCode) {
                      <span class="nx-badge nx-badge--info">{{ c.salespersonCode }}</span>
                    } @else { <span style="color:var(--nx-text-muted);">—</span> }
                  </td>
                  <td style="font-size:var(--nx-text-sm);">{{ c.paymentTermsCode || '—' }}</td>
                  <td class="num" [class.text-danger]="c.balance < 0">{{ c.balance | currency:'DOP':'symbol':'1.2-2' }}</td>
                  <td class="num" [style.color]="c.balanceDue > 0 ? 'var(--nx-red-600)' : 'inherit'">
                    {{ c.balanceDue | currency:'DOP':'symbol':'1.2-2' }}
                  </td>
                  <td>
                    @if (c.blocked) {
                      <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
                    } @else {
                      <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
                    }
                  </td>
                  <td>
                    <a [routerLink]="['/customers', c.no]" class="nx-iconbtn nx-iconbtn--sm" aria-label="Ver ficha">
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

    <!-- Modal: Nuevo / Editar Cliente -->
    @if (showModal()) {
      <div class="modal-backdrop" (click)="closeModal()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Nuevo Cliente</h2>
            <button class="nx-iconbtn" (click)="closeModal()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (modalError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ modalError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field">
                <label class="nx-label">No. Cliente *</label>
                <input class="nx-input" [(ngModel)]="form.no" placeholder="C-00011" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Nombre *</label>
                <input class="nx-input" [(ngModel)]="form.name" placeholder="Razón Social o Nombre" />
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Dirección</label>
                <input class="nx-input" [(ngModel)]="form.address" placeholder="Calle, No., Sector" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Ciudad</label>
                <input class="nx-input" [(ngModel)]="form.city" placeholder="Santo Domingo" />
              </div>
              <div class="nx-field">
                <label class="nx-label">País</label>
                <input class="nx-input" [(ngModel)]="form.countryRegionCode" placeholder="DO" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Contacto</label>
                <input class="nx-input" [(ngModel)]="form.contact" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Teléfono</label>
                <input class="nx-input" type="tel" [(ngModel)]="form.phoneNo" placeholder="809-555-0000" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Correo Electrónico</label>
                <input class="nx-input" type="email" [(ngModel)]="form.email" />
              </div>
              <div class="nx-field">
                <label class="nx-label">RNC / Cédula</label>
                <input class="nx-input" [(ngModel)]="form.vatRegistrationNo" placeholder="1-30-XXXXX-X" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Límite de Crédito (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.creditLimit" placeholder="0.00" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Condición de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentTermsCode">
                  <option value="">— Seleccionar —</option>
                  <option>15 DIAS</option>
                  <option>30 DIAS</option>
                  <option>45 DIAS</option>
                  <option>60 DIAS</option>
                  <option>CONTADO</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Método de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentMethodCode">
                  <option value="">— Seleccionar —</option>
                  <option>EFECTIVO</option>
                  <option>TRANSFERENCIA</option>
                  <option>CHEQUE</option>
                  <option>TARJETA</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Vendedor</label>
                <select class="nx-select" [(ngModel)]="form.salespersonCode">
                  <option value="">— Sin asignar —</option>
                  <option>RMT</option>
                  <option>MAV</option>
                  <option>JAR</option>
                  <option>LGM</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Grupo Contabilización</label>
                <select class="nx-select" [(ngModel)]="form.customerPostingGroup">
                  <option value="">— Seleccionar —</option>
                  <option>DOMESTIC</option>
                  <option>EXPORT</option>
                  <option>GOVERNMENT</option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeModal()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveCustomer()">
              @if (saving()) { Guardando… } @else { Guardar Cliente }
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
    .text-danger { color:var(--nx-red-600); }
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
export class CustomerListPage implements OnInit {
  readonly svc = inject(CustomerService);

  searchText = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'all';
  sortField: CustomerSortField = 'name';
  sortAsc = true;

  showModal = signal(false);
  saving = signal(false);
  modalError = signal<string | null>(null);

  form: CustomerFormData = this.emptyForm();

  private _searchTimeout?: ReturnType<typeof setTimeout>;

  activeTagsCount = computed(() => {
    let n = 0;
    if (this.searchText) n++;
    if (this.showBlocked !== 'all') n++;
    return n;
  });

  sorted(): CustomerListItem[] {
    return this.svc.sorted(this.sortField, this.sortAsc);
  }

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

  setSort(f: CustomerSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: CustomerSortField): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.showBlocked = 'all';
    this.reload();
  }

  goToPage(page: number): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked, page });
  }

  exportCsv(): void {
    const rows = this.sorted();
    const header = 'No.,Nombre,Ciudad,Contacto,Vendedor,Condición Pago,Saldo,Saldo Vencido,Estado';
    const body = rows.map(r =>
      [r.no, r.name, r.city, r.contact, r.salespersonCode, r.paymentTermsCode,
       r.balance, r.balanceDue, r.blocked ? 'Bloqueado' : 'Activo'].join(',')
    ).join('\n');
    const blob = new Blob([header + '\n' + body], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a'); a.href = url; a.download = 'clientes.csv'; a.click();
    URL.revokeObjectURL(url);
  }

  openModal(): void {
    this.form = this.emptyForm();
    this.modalError.set(null);
    this.showModal.set(true);
  }

  closeModal(): void { this.showModal.set(false); }

  async saveCustomer(): Promise<void> {
    if (!this.form.no?.trim() || !this.form.name?.trim()) {
      this.modalError.set('El No. de cliente y el nombre son obligatorios.');
      return;
    }
    this.saving.set(true);
    this.modalError.set(null);
    try {
      await this.svc.create(this.form);
      this.closeModal();
      this.reload();
    } catch (e: any) {
      this.modalError.set(e?.error?.error?.message ?? 'Error al guardar el cliente.');
    } finally {
      this.saving.set(false);
    }
  }

  private emptyForm(): CustomerFormData {
    return {
      no: '', name: '', address: '', city: '', contact: '',
      phoneNo: '', email: '', creditLimit: 0, vatRegistrationNo: '',
      paymentTermsCode: '', paymentMethodCode: '', salespersonCode: '',
      currencyCode: '', customerPostingGroup: 'DOMESTIC', countryRegionCode: 'DO'
    };
  }
}
