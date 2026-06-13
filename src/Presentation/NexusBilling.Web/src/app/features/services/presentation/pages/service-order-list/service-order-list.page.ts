import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ServiceOrderService, CreateServiceOrderData } from '../../../data/service-order.service';
import { ServiceOrderListItem } from '../../../domain/service-order.model';
import { ApiService } from '../../../../../core/services/api.service';
import { CustomerService } from '../../../../customers/data/customer.service';
import { ItemService, ItemListItem } from '../../../../inventory/data/item.service';

interface ServiceLine {
  no: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType: number;
  lineTypeLabel: string;
  suggestions: ItemListItem[];
  showSuggestions: boolean;
}

@Component({
  selector: 'app-service-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Órdenes de Servicio</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Órdenes de Servicio</h1>
        <p class="nx-page-subtitle">{{ filtered().length }} documentos</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="reload()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
          Actualizar
        </button>
        <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openNew()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nueva Orden
        </button>
      </div>
    </div>

    <!-- Tabs -->
    <div class="nx-tabs" style="margin-bottom:var(--nx-space-4);">
      @for (tab of tabs; track tab.id) {
        <button class="nx-tab" [attr.aria-selected]="activeTab() === tab.id" (click)="setTab(tab.id)">
          {{ tab.label }}
          <span class="nx-tab__count">{{ countForTab(tab.id) }}</span>
        </button>
      }
    </div>

    <!-- Filter -->
    <div style="display:flex;gap:var(--nx-space-3);margin-bottom:var(--nx-space-4);flex-wrap:wrap;">
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:360px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar No., cliente, descripción…" [(ngModel)]="searchText" (ngModelChange)="applyFilter()" />
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (filtered().length === 0) {
      <div class="nx-empty">
        <p class="nx-empty__title">Sin órdenes de servicio</p>
        <p class="nx-empty__text">Crea una nueva orden para comenzar.</p>
        <button class="nx-btn nx-btn--primary nx-btn--sm" style="margin-top:var(--nx-space-3);" (click)="openNew()">Nueva Orden</button>
      </div>
    } @else {
      <div class="nx-card">
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Órdenes de servicio">
            <thead>
              <tr>
                <th>No.</th>
                <th>Tipo</th>
                <th>Cliente</th>
                <th>Descripción</th>
                <th>Inicio</th>
                <th>Fin</th>
                <th>Contrato</th>
                <th>Estado</th>
              </tr>
            </thead>
            <tbody>
              @for (o of filtered(); track o.no) {
                <tr style="cursor:pointer;" [routerLink]="['/services', o.no]">
                  <td class="nx-td--doc">{{ o.no }}</td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ o.documentType }}</td>
                  <td>
                    <div style="font-weight:var(--nx-weight-medium);">{{ o.customerName }}</div>
                    <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);font-family:var(--nx-font-mono);">{{ o.customerNo }}</div>
                  </td>
                  <td style="max-width:200px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ o.description || '—' }}</td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);font-family:var(--nx-font-mono);">{{ o.startingDate || '—' }}</td>
                  <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);font-family:var(--nx-font-mono);">{{ o.finishingDate || '—' }}</td>
                  <td style="font-size:var(--nx-text-sm);font-family:var(--nx-font-mono);">{{ o.contractNo || '—' }}</td>
                  <td>
                    <span class="nx-badge" [class]="statusClass(o.status)">
                      <span class="nx-badge__dot"></span>{{ o.statusLabel }}
                    </span>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      </div>
    }

    <!-- Nueva Orden Modal -->
    @if (showNew()) {
      <div class="modal-backdrop" (click)="closeNew()">
        <div class="modal-box modal-box--wide" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <div>
              <div class="nx-eyebrow" style="margin-bottom:var(--nx-space-1);">{{ docTypeLabel(form.documentType) }}</div>
              <h2 class="nx-page-title" style="margin:0;font-size:var(--nx-text-xl);">Nueva Orden de Servicio</h2>
            </div>
            <button class="nx-iconbtn" (click)="closeNew()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (formError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
                <strong>No se pudo crear la orden.</strong>
                <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">{{ formError() }}</p>
              </div>
            }
            @if (formSuccess()) {
              <div class="nx-callout nx-callout--success" style="margin-bottom:var(--nx-space-4);">
                <strong>Orden creada.</strong>
                <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">
                  No. <span style="font-family:var(--nx-font-mono);">{{ formSuccess() }}</span>
                  &mdash; <a [routerLink]="['/services', formSuccess()]" class="nx-link">Ver orden</a>
                </p>
              </div>
            }

            <!-- Header fields -->
            <div class="form-grid" style="margin-bottom:var(--nx-space-4);">
              <div class="nx-field">
                <label class="nx-label">Tipo <span style="color:var(--nx-red-500);">*</span></label>
                <select class="nx-select" [(ngModel)]="form.documentType" (ngModelChange)="onDocTypeChange()">
                  <option [value]="0">Cotización</option>
                  <option [value]="1">Orden de Servicio</option>
                  <option [value]="2">Factura de Servicio</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">No. del Documento</label>
                <div style="display:flex;gap:var(--nx-space-2);">
                  <input class="nx-input" style="font-family:var(--nx-font-mono);flex:1;" [(ngModel)]="form.documentNo" [placeholder]="loadingNextNo() ? 'Cargando…' : ''" />
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" [disabled]="loadingNextNo()" (click)="refreshNextNo()" title="Siguiente número">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
                  </button>
                </div>
              </div>
              <div class="nx-field" style="position:relative;">
                <label class="nx-label">Cliente <span style="color:var(--nx-red-500);">*</span></label>
                <input class="nx-input" [(ngModel)]="form.customerSearch"
                  placeholder="Buscar cliente…"
                  (focus)="onCustomerFocus()"
                  (input)="onCustomerInput()"
                  (blur)="closeCustomerSuggestionsDelayed()"
                  autocomplete="off" />
                @if (showCustomerSuggestions() && customerSuggestions().length > 0) {
                  <div class="item-dropdown">
                    @for (c of customerSuggestions(); track c.no) {
                      <button class="item-dropdown__item" (mousedown)="selectCustomer(c)">
                        <span style="font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-action);">{{ c.no }}</span>
                        <span style="flex:1;">{{ c.name }}</span>
                      </button>
                    }
                  </div>
                }
              </div>
              <div class="nx-field">
                <label class="nx-label">Descripción del servicio</label>
                <input class="nx-input" [(ngModel)]="form.description" placeholder="Descripción general de la orden" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de la orden <span style="color:var(--nx-red-500);">*</span></label>
                <input class="nx-input" type="date" [(ngModel)]="form.orderDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de inicio</label>
                <input class="nx-input" type="date" [(ngModel)]="form.startingDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de finalización</label>
                <input class="nx-input" type="date" [(ngModel)]="form.finishingDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">No. de Contrato</label>
                <input class="nx-input" [(ngModel)]="form.contractNo" placeholder="Opcional" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Condición de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentTermsCode">
                  <option value="">Seleccionar…</option>
                  <option>CONTADO</option><option>15 DIAS</option><option>30 DIAS</option><option>45 DIAS</option><option>60 DIAS</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Método de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentMethodCode">
                  <option value="">Seleccionar…</option>
                  <option>EFECTIVO</option><option>TRANSFERENCIA</option><option>CHEQUE</option><option>TARJETA</option>
                </select>
              </div>
            </div>

            <!-- Lines -->
            <div style="margin-bottom:var(--nx-space-3);">
              <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-2);">
                <span style="font-weight:var(--nx-weight-semibold);font-size:var(--nx-text-sm);">Líneas <span style="color:var(--nx-red-500);">*</span></span>
                <div style="display:flex;gap:var(--nx-space-2);">
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine(1)">+ Producto</button>
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine(2)">+ Recurso</button>
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine(3)">+ Costo</button>
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine(4)">+ Cuenta CG</button>
                </div>
              </div>
              <div style="overflow-x:auto;">
                <table class="nx-table" style="min-width:800px;">
                  <thead>
                    <tr>
                      <th style="width:34px;">#</th>
                      <th style="width:80px;">Tipo</th>
                      <th style="min-width:130px;">No. / Ref.</th>
                      <th style="min-width:200px;">Descripción</th>
                      <th style="width:60px;">U/M</th>
                      <th class="nx-th--num" style="width:70px;">Cant.</th>
                      <th class="nx-th--num" style="width:100px;">Precio</th>
                      <th class="nx-th--num" style="width:60px;">Desc.%</th>
                      <th class="nx-th--num" style="width:110px;">Importe</th>
                      <th style="width:36px;"></th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (line of lines; track $index; let i = $index) {
                      <tr>
                        <td style="color:var(--nx-text-faint);font-size:var(--nx-text-xs);font-family:var(--nx-font-mono);">{{ (i + 1) * 10000 }}</td>
                        <td>
                          <span class="nx-badge nx-badge--outline" style="font-size:10px;">{{ line.lineTypeLabel }}</span>
                        </td>
                        <td style="position:relative;">
                          <input
                            class="nx-input nx-input--sm"
                            style="width:100%;font-family:var(--nx-font-mono);"
                            [(ngModel)]="line.no"
                            [placeholder]="linePlaceholder(line.lineType)"
                            (focus)="onLineFocus(i)"
                            (input)="onLineInput(i)"
                            (blur)="closeLineSuggestionsDelayed(i)"
                            autocomplete="off"
                          />
                          @if (line.showSuggestions && line.suggestions.length > 0) {
                            <div class="item-dropdown">
                              @for (s of line.suggestions; track s.no) {
                                <button class="item-dropdown__item" (mousedown)="selectLineSuggestion(i, s)">
                                  <span style="font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-action);">{{ s.no }}</span>
                                  <span style="flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ s.description }}</span>
                                  <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ s.unitPrice | number:'1.2-2' }}</span>
                                </button>
                              }
                            </div>
                          }
                        </td>
                        <td><input class="nx-input nx-input--sm" [(ngModel)]="line.description" placeholder="Descripción" style="width:100%;" /></td>
                        <td><input class="nx-input nx-input--sm" [(ngModel)]="line.unitOfMeasure" style="width:54px;" placeholder="UND" /></td>
                        <td><input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.quantity" type="number" min="0.001" step="0.001" style="width:64px;" /></td>
                        <td><input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.unitPrice" type="number" min="0" step="0.01" style="width:94px;" /></td>
                        <td><input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.lineDiscountPct" type="number" min="0" max="100" style="width:54px;" /></td>
                        <td class="nx-td--num"><span class="nx-num">{{ lineAmount(line) | number:'1.2-2' }}</span></td>
                        <td>
                          <button class="nx-iconbtn nx-iconbtn--sm" (click)="removeLine(i)" style="color:var(--nx-red-500);">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/></svg>
                          </button>
                        </td>
                      </tr>
                    }
                    @if (lines.length === 0) {
                      <tr><td colspan="10" style="text-align:center;color:var(--nx-text-muted);padding:var(--nx-space-5);">Agrega productos, recursos o costos con los botones de arriba</td></tr>
                    }
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">Subtotal:</td>
                      <td class="nx-td--num nx-num">{{ subtotal() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">ITBIS (18%):</td>
                      <td class="nx-td--num nx-num">{{ itbis() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-bold);">Total c/ITBIS:</td>
                      <td class="nx-td--num nx-num" style="font-weight:var(--nx-weight-bold);font-size:1.05rem;">{{ total() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                  </tfoot>
                </table>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeNew()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="save()">
              @if (saving()) { Guardando… } @else { Crear {{ docTypeShortLabel(form.documentType) }} }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(720px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-box--wide { width:min(1000px,96vw); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
    .nx-input--sm { padding:4px 8px;font-size:var(--nx-text-sm); }
    .item-dropdown { position:absolute;top:100%;left:0;right:0;z-index:200;background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-md);box-shadow:var(--nx-shadow-lg);max-height:200px;overflow-y:auto; }
    .item-dropdown__item { display:flex;align-items:center;gap:var(--nx-space-2);padding:6px 10px;width:100%;text-align:left;background:none;border:none;cursor:pointer;font-size:var(--nx-text-sm);color:var(--nx-text-base); }
    .item-dropdown__item:hover { background:var(--nx-canvas-alt); }
    @media (max-width:600px) { .form-grid { grid-template-columns:1fr; } }
  `]
})
export class ServiceOrderListPage implements OnInit {
  readonly svc = inject(ServiceOrderService);
  private readonly api = inject(ApiService);
  private readonly customerSvc = inject(CustomerService);
  private readonly itemSvc = inject(ItemService);
  private readonly router = inject(Router);

  searchText = '';
  activeTab = signal<string>('all');
  filtered = signal<ServiceOrderListItem[]>([]);

  showNew = signal(false);
  saving = signal(false);
  formError = signal<string | null>(null);
  formSuccess = signal<string | null>(null);
  loadingNextNo = signal(false);
  private previewNo = '';

  form = {
    documentType: 1,
    documentNo: '',
    customerNo: '',
    customerSearch: '',
    customerName: '',
    description: '',
    orderDate: new Date().toISOString().slice(0, 10),
    startingDate: '',
    finishingDate: '',
    contractNo: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    salespersonCode: '',
  };

  lines: ServiceLine[] = [];
  private allItems: ItemListItem[] = [];

  showCustomerSuggestions = signal(false);
  customerSuggestions = signal<{ no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }[]>([]);
  private allCustomers: { no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }[] = [];

  readonly tabs = [
    { id: 'all', label: 'Todos' },
    { id: '0', label: 'Pendiente' },
    { id: '1', label: 'En Proceso' },
    { id: '2', label: 'Terminado' },
    { id: '3', label: 'En Espera' },
  ];

  async ngOnInit(): Promise<void> {
    await this.svc.load();
    this.applyFilter();
  }

  async reload(): Promise<void> {
    await this.svc.load();
    this.applyFilter();
  }

  setTab(id: string): void {
    this.activeTab.set(id);
    this.applyFilter();
  }

  applyFilter(): void {
    let items = this.svc.orders();
    const tab = this.activeTab();
    if (tab !== 'all') items = items.filter(o => String(o.status) === tab);
    const q = this.searchText.toLowerCase().trim();
    if (q) items = items.filter(o =>
      o.no.toLowerCase().includes(q) ||
      o.customerNo.toLowerCase().includes(q) ||
      o.customerName.toLowerCase().includes(q) ||
      o.description.toLowerCase().includes(q));
    this.filtered.set(items);
  }

  countForTab(id: string): number {
    const all = this.svc.orders();
    return id === 'all' ? all.length : all.filter(o => String(o.status) === id).length;
  }

  statusClass(status: number): string {
    return ({ 0: 'nx-badge--outline', 1: 'nx-badge--info', 2: 'nx-badge--success', 3: 'nx-badge--warn' } as Record<number, string>)[status] ?? 'nx-badge--outline';
  }

  docTypeLabel(t: number): string {
    return ({ 0: 'Nueva Cotización de Servicio', 1: 'Nueva Orden de Servicio', 2: 'Nueva Factura de Servicio' } as Record<number, string>)[t] ?? 'Nuevo Documento';
  }

  docTypeShortLabel(t: number): string {
    return ({ 0: 'Cotización', 1: 'Orden', 2: 'Factura' } as Record<number, string>)[t] ?? 'Documento';
  }

  lineTypeLabel(t: number): string {
    return ({ 1: 'Art.', 2: 'Rec.', 3: 'Costo', 4: 'CG' } as Record<number, string>)[t] ?? '';
  }

  linePlaceholder(t: number): string {
    return ({ 1: 'ART-00001', 2: 'REC-001', 3: 'COSTO-01', 4: '5010' } as Record<number, string>)[t] ?? '';
  }

  private seriesForDocType(t: number): string {
    return ({ 0: 'SCOT', 1: 'OSV', 2: 'SFAC' } as Record<number, string>)[t] ?? 'OSV';
  }

  openNew(): void {
    this.lines = [];
    this.formError.set(null);
    this.formSuccess.set(null);
    this.form.documentNo = '';
    this.form.customerNo = '';
    this.form.customerSearch = '';
    this.form.customerName = '';
    this.form.description = '';
    this.form.orderDate = new Date().toISOString().slice(0, 10);
    this.form.startingDate = '';
    this.form.finishingDate = '';
    this.form.contractNo = '';
    this.showNew.set(true);
    this.loadAllItems();
    this.loadAllCustomers();
    this.refreshNextNo();
  }

  closeNew(): void { this.showNew.set(false); }

  async onDocTypeChange(): Promise<void> {
    this.form.documentNo = '';
    await this.refreshNextNo();
  }

  async refreshNextNo(): Promise<void> {
    const series = this.seriesForDocType(this.form.documentType);
    this.loadingNextNo.set(true);
    try {
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>(`administration/no-series/${series}/next`));
      this.previewNo = res.nextNo;
      this.form.documentNo = res.nextNo;
    } catch {
      this.previewNo = '';
      this.form.documentNo = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  private async loadAllItems(): Promise<void> {
    if (this.allItems.length > 0) return;
    try {
      await this.itemSvc.load({ pageSize: 500 });
      this.allItems = this.itemSvc.items();
    } catch { /* ignore */ }
  }

  private async loadAllCustomers(): Promise<void> {
    if (this.allCustomers.length > 0) return;
    try {
      await this.customerSvc.load({ pageSize: 500 });
      this.allCustomers = this.customerSvc.items().map(c => ({
        no: c.no, name: c.name,
        paymentTermsCode: c.paymentTermsCode,
        paymentMethodCode: (c as any).paymentMethodCode || '',
        salespersonCode: c.salespersonCode,
      }));
    } catch { /* ignore */ }
  }

  onCustomerFocus(): void {
    const q = this.form.customerSearch.trim().toLowerCase();
    if (!q) {
      this.customerSuggestions.set(this.allCustomers.slice(0, 10));
      this.showCustomerSuggestions.set(this.allCustomers.length > 0);
    }
  }

  onCustomerInput(): void {
    const q = this.form.customerSearch.trim().toLowerCase();
    if (!q) {
      this.customerSuggestions.set(this.allCustomers.slice(0, 10));
      this.showCustomerSuggestions.set(this.allCustomers.length > 0);
      return;
    }
    const matches = this.allCustomers.filter(c => c.no.toLowerCase().includes(q) || c.name.toLowerCase().includes(q)).slice(0, 8);
    this.customerSuggestions.set(matches);
    this.showCustomerSuggestions.set(matches.length > 0);
  }

  selectCustomer(c: { no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }): void {
    this.form.customerNo = c.no;
    this.form.customerName = c.name;
    this.form.customerSearch = `${c.no} · ${c.name}`;
    this.form.paymentTermsCode = c.paymentTermsCode || this.form.paymentTermsCode;
    this.form.paymentMethodCode = c.paymentMethodCode || this.form.paymentMethodCode;
    this.form.salespersonCode = c.salespersonCode || this.form.salespersonCode;
    this.showCustomerSuggestions.set(false);
  }

  closeCustomerSuggestionsDelayed(): void {
    setTimeout(() => this.showCustomerSuggestions.set(false), 200);
  }

  addLine(lineType: number): void {
    this.lines = [...this.lines, {
      no: '', description: '', quantity: 1, unitPrice: 0,
      lineDiscountPct: 0, unitOfMeasure: 'UND',
      lineType, lineTypeLabel: this.lineTypeLabel(lineType),
      suggestions: [], showSuggestions: false
    }];
  }

  removeLine(i: number): void {
    this.lines = this.lines.filter((_, idx) => idx !== i);
  }

  onLineFocus(i: number): void {
    if (this.lines[i].lineType !== 1) return;
    const q = this.lines[i].no.trim();
    if (!q) {
      const suggestions = this.allItems.slice(0, 10);
      this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
    }
  }

  onLineInput(i: number): void {
    if (this.lines[i].lineType !== 1) return;
    const q = this.lines[i].no.toLowerCase().trim();
    if (!q) {
      const suggestions = this.allItems.slice(0, 10);
      this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
      return;
    }
    const suggestions = this.allItems.filter(it => it.no.toLowerCase().includes(q) || it.description.toLowerCase().includes(q)).slice(0, 8);
    this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
  }

  selectLineSuggestion(i: number, item: ItemListItem): void {
    this.lines[i] = {
      ...this.lines[i],
      no: item.no, description: item.description,
      unitPrice: item.unitPrice, unitOfMeasure: item.baseUnitOfMeasure,
      suggestions: [], showSuggestions: false,
    };
  }

  closeLineSuggestionsDelayed(i: number): void {
    setTimeout(() => {
      if (this.lines[i]) this.lines[i] = { ...this.lines[i], showSuggestions: false };
    }, 200);
  }

  lineAmount(line: ServiceLine): number {
    const base = line.quantity * line.unitPrice;
    return base - (base * (line.lineDiscountPct / 100));
  }

  subtotal(): number { return this.lines.reduce((s, l) => s + this.lineAmount(l), 0); }
  itbis(): number { return this.subtotal() * 0.18; }
  total(): number { return this.subtotal() * 1.18; }

  async save(): Promise<void> {
    this.formError.set(null);
    this.formSuccess.set(null);

    if (!this.form.customerNo) { this.formError.set('Debes seleccionar un cliente.'); return; }
    if (!this.form.orderDate) { this.formError.set('La fecha de la orden es obligatoria.'); return; }
    if (this.lines.length === 0) { this.formError.set('Debes agregar al menos una línea.'); return; }
    if (this.lines.some(l => !l.description?.trim())) { this.formError.set('Todas las líneas deben tener descripción.'); return; }
    if (this.lines.some(l => !l.quantity || l.quantity <= 0)) { this.formError.set('Todas las líneas deben tener cantidad mayor a cero.'); return; }

    this.saving.set(true);
    try {
      const typedNo = this.form.documentNo?.trim() || '';
      const useManual = typedNo && typedNo !== this.previewNo;
      const seriesCode = this.seriesForDocType(this.form.documentType);
      const data: CreateServiceOrderData = {
        documentType: this.form.documentType,
        customerNo: this.form.customerNo,
        customerName: this.form.customerName,
        description: this.form.description,
        orderDate: this.form.orderDate,
        startingDate: this.form.startingDate || null,
        finishingDate: this.form.finishingDate || null,
        paymentTermsCode: this.form.paymentTermsCode,
        paymentMethodCode: this.form.paymentMethodCode,
        salespersonCode: this.form.salespersonCode,
        currencyCode: '',
        contractNo: this.form.contractNo,
        seriesCode: useManual ? null : seriesCode,
        manualNo: useManual ? typedNo : null,
        lines: this.lines.map(l => ({
          no: l.no, description: l.description,
          quantity: l.quantity, unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct, unitOfMeasure: l.unitOfMeasure,
          lineType: l.lineType,
        })),
      };
      const no = await this.svc.create(data);
      this.formSuccess.set(no);
      this.lines = [];
      await this.reload();
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('sequence')) {
        this.formError.set('No hay secuencia configurada para este tipo de documento. Ve a Configuración > Secuencias.');
      } else if (msg) {
        this.formError.set(msg);
      } else {
        this.formError.set('Error al guardar. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }
}
