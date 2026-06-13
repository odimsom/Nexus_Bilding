import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService, CreateSalesOrderData } from '../../../data/invoice.service';

@Component({
  selector: 'app-sales-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Órdenes de Venta</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Órdenes de Venta</h1>
        <p class="nx-page-subtitle">{{ filtered().length }} documentos</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--primary" (click)="openNew()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nueva Orden
        </button>
      </div>
    </div>

    <!-- Tabs -->
    <div class="nx-tabs" style="margin-bottom:var(--nx-space-4);">
      @for (tab of tabs; track tab.id) {
        <button class="nx-tab" [attr.aria-selected]="activeTab === tab.id" (click)="setTab(tab.id)">
          {{ tab.label }}
          <span class="nx-tab__count">{{ countForTab(tab.id) }}</span>
        </button>
      }
    </div>

    <div style="display:flex;gap:var(--nx-space-3);margin-bottom:var(--nx-space-4);flex-wrap:wrap;">
      <div class="nx-input-icon" style="flex:1;min-width:220px;max-width:360px;">
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
        <input type="search" class="nx-input" placeholder="Buscar No., cliente…" [(ngModel)]="searchText" (ngModelChange)="applyFilter()" />
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (filtered().length === 0) {
      <div class="nx-empty">
        <div class="nx-empty__icon">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
        </div>
        <p class="nx-empty__title">No hay órdenes</p>
        <p class="nx-empty__text">Crea una nueva orden de venta para comenzar.</p>
        <button class="nx-btn nx-btn--primary nx-btn--sm" style="margin-top:var(--nx-space-4);" (click)="openNew()">Nueva Orden</button>
      </div>
    } @else {
      <div class="nx-card" style="overflow:hidden;">
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Órdenes de venta">
            <thead>
              <tr>
                <th>No.</th>
                <th>Tipo</th>
                <th>Cliente</th>
                <th>Fecha</th>
                <th>Estado</th>
                <th>Moneda</th>
                <th class="nx-th--num">Subtotal</th>
                <th class="nx-th--num">Total c/IVA</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (order of filtered(); track order.no) {
                <tr style="cursor:pointer;" (click)="router.navigate(['/sales', order.no])">
                  <td class="nx-td--doc">{{ order.no }}</td>
                  <td>
                    <span class="nx-badge nx-badge--outline">{{ docTypeLabel(order.documentType) }}</span>
                  </td>
                  <td style="font-weight:var(--nx-weight-medium);">
                    <a [routerLink]="['/customers', order.sellToCustomerNo]" class="nx-link" (click)="$event.stopPropagation()">{{ order.sellToCustomerName }}</a>
                  </td>
                  <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ order.postingDate }}</td>
                  <td>
                    <span class="nx-badge" [class]="statusClass(order.status)">
                      <span class="nx-badge__dot"></span>{{ statusLabel(order.status) }}
                    </span>
                  </td>
                  <td>
                    @if (order.currencyCode) {
                      <span class="nx-badge nx-badge--outline">{{ order.currencyCode }}</span>
                    } @else {
                      <span style="color:var(--nx-text-faint);font-size:var(--nx-text-sm);">DOP</span>
                    }
                  </td>
                  <td class="nx-td--num">{{ order.amount | number:'1.2-2' }}</td>
                  <td class="nx-td--num" style="font-weight:var(--nx-weight-semibold);">{{ order.amountIncludingVat | number:'1.2-2' }}</td>
                  <td>
                    <a [routerLink]="['/sales', order.no]" class="nx-iconbtn nx-iconbtn--sm" (click)="$event.stopPropagation()">
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 18l6-6-6-6"/></svg>
                    </a>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      </div>
    }

    <!-- Modal nueva orden -->
    @if (showNewModal()) {
      <div class="modal-backdrop" (click)="closeNew()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Nueva Orden de Venta</h2>
            <button class="nx-iconbtn" (click)="closeNew()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (createError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ createError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field">
                <label class="nx-label">Tipo de documento *</label>
                <select class="nx-select" [(ngModel)]="newForm.documentType">
                  <option value="Order">Pedido</option>
                  <option value="Quote">Cotización</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">No. Cliente *</label>
                <input class="nx-input" [(ngModel)]="newForm.sellToCustomerNo" placeholder="Ej. C-00001" />
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Nombre del cliente</label>
                <input class="nx-input" [(ngModel)]="newForm.sellToCustomerName" placeholder="Nombre o razón social" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de contabilización *</label>
                <input class="nx-input" type="date" [(ngModel)]="newForm.postingDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de vencimiento</label>
                <input class="nx-input" type="date" [(ngModel)]="newForm.dueDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Ref. externa</label>
                <input class="nx-input" [(ngModel)]="newForm.externalDocumentNo" placeholder="No. pedido del cliente" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Moneda</label>
                <input class="nx-input" [(ngModel)]="newForm.currencyCode" placeholder="DOP (vacío = LCY)" />
              </div>
            </div>
            <div class="nx-callout nx-callout--info" style="margin-top:var(--nx-space-4);">
              <strong>Secuencia automática:</strong> El número de orden será asignado por el sistema. Las líneas de detalle se agregan desde la ficha del documento.
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeNew()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving() || !newForm.sellToCustomerNo" (click)="saveNew()">
              @if (saving()) { Guardando… } @else { Crear Orden }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:2rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(620px,96vw);display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { padding:var(--nx-space-5) var(--nx-space-6); }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
})
export class SalesOrderListPage implements OnInit {
  readonly svc = inject(InvoiceService);
  readonly router = inject(Router);

  readonly tabs = [
    { id: 'all',   label: 'Todas' },
    { id: 'Order', label: 'Pedidos' },
    { id: 'Quote', label: 'Cotizaciones' },
  ];

  activeTab = 'all';
  searchText = '';
  filtered = signal<any[]>([]);
  showNewModal = signal(false);
  saving = signal(false);
  createError = signal('');

  newForm: CreateSalesOrderData = this.emptyForm();

  async ngOnInit(): Promise<void> { await this.reload(); }

  async reload(): Promise<void> {
    await this.svc.loadOrders();
    this.applyFilter();
  }

  setTab(id: string): void { this.activeTab = id; this.applyFilter(); }

  applyFilter(): void {
    let list = this.svc.orders();
    if (this.activeTab !== 'all') list = list.filter(o => o.documentType === this.activeTab);
    if (this.searchText) {
      const q = this.searchText.toLowerCase();
      list = list.filter(o => o.no.toLowerCase().includes(q) || (o.sellToCustomerName || '').toLowerCase().includes(q));
    }
    this.filtered.set(list);
  }

  countForTab(id: string): number {
    const all = this.svc.orders();
    return id === 'all' ? all.length : all.filter(o => o.documentType === id).length;
  }

  openNew(): void {
    this.newForm = this.emptyForm();
    this.createError.set('');
    this.showNewModal.set(true);
  }

  closeNew(): void { this.showNewModal.set(false); }

  async saveNew(): Promise<void> {
    if (!this.newForm.sellToCustomerNo) {
      this.createError.set('El número de cliente es requerido.');
      return;
    }
    this.saving.set(true);
    this.createError.set('');
    try {
      const no = await this.svc.createOrder({ ...this.newForm, lines: [] });
      this.closeNew();
      this.router.navigate(['/sales', no]);
    } catch (e: any) {
      this.createError.set(e?.error?.error?.message ?? 'Ocurrió un error al crear la orden.');
    } finally {
      this.saving.set(false);
    }
  }

  private emptyForm(): CreateSalesOrderData {
    return {
      documentType: 'Order',
      sellToCustomerNo: '',
      sellToCustomerName: '',
      externalDocumentNo: '',
      currencyCode: '',
      paymentTermsCode: '',
      paymentMethodCode: '',
      salespersonCode: '',
      postingDate: new Date().toISOString().split('T')[0],
      dueDate: null,
      seriesCode: 'ORD',
      lines: [],
    };
  }

  docTypeLabel(t: string): string {
    return { Order: 'Pedido', Quote: 'Cotización', Invoice: 'Factura' }[t] ?? t;
  }

  statusClass(s: string): string {
    return { Open: 'nx-badge--info', Released: 'nx-badge--success', Closed: 'nx-badge--outline' }[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return { Open: 'Abierta', Released: 'Liberada', Closed: 'Cerrada' }[s] ?? s;
  }
}
