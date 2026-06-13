import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { CustomerService, CustomerFormData } from '../../../data/customer.service';
import { Customer } from '../../../domain/customer.model';
import { InvoiceService, CreateSalesOrderData } from '../../../../sales/data/invoice.service';
import { ItemService, ItemListItem } from '../../../../inventory/data/item.service';
import { ApiService } from '../../../../../core/services/api.service';
import { RncPipe } from '../../../../../shared/pipes/rnc.pipe';
import { PhonePipe } from '../../../../../shared/pipes/phone.pipe';
import { RncFormatDirective } from '../../../../../shared/directives/rnc-format.directive';
import { PhoneFormatDirective } from '../../../../../shared/directives/phone-format.directive';

interface OrderLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  type: 'Item' | 'Service' | 'G/L Account';
  suggestions: ItemListItem[];
  showSuggestions: boolean;
}

@Component({
  selector: 'app-customer-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, RncPipe, PhonePipe, RncFormatDirective, PhoneFormatDirective],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando cliente…</p>
      </div>
    } @else if (customer()) {
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/customers">Clientes</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ customer()!.name }}</span>
      </nav>

      <div class="nx-page-header">
        <div class="nx-avatar nx-avatar--lg" style="background:var(--nx-emerald-100);color:var(--nx-emerald-800);flex-shrink:0;">
          {{ customer()!.name.charAt(0) }}
        </div>
        <div style="flex:1;min-width:0;">
          <h1 class="nx-page-title">{{ customer()!.name }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);flex-wrap:wrap;">
            <span class="nx-eyebrow">{{ customer()!.no }}</span>
            @if (customer()!.salespersonCode) {
              <span class="nx-badge nx-badge--info">Vendedor: {{ customer()!.salespersonCode }}</span>
            }
            @if (customer()!.blocked) {
              <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
            } @else {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="openEdit()">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
            Editar
          </button>
          <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openOrder()">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
            Nueva Factura
          </button>
        </div>
      </div>

      <!-- KPI tiles -->
      <div class="kpi-row">
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Saldo Total</div>
          <div class="nx-kpi-tile__value" [style.color]="customer()!.balance > 0 ? 'var(--nx-amber-600)' : 'inherit'">
            {{ customer()!.balance | currency:'DOP':'symbol':'1.2-2' }}
          </div>
          <div class="nx-kpi-tile__sub">Deuda pendiente</div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Saldo Vencido</div>
          <div class="nx-kpi-tile__value" [style.color]="customer()!.balanceDue > 0 ? 'var(--nx-red-600)' : 'var(--nx-emerald-600)'">
            {{ customer()!.balanceDue | currency:'DOP':'symbol':'1.2-2' }}
          </div>
          <div class="nx-kpi-tile__sub">Facturas vencidas</div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Límite de Crédito</div>
          <div class="nx-kpi-tile__value">{{ customer()!.creditLimit | currency:'DOP':'symbol':'1.2-2' }}</div>
          <div class="nx-kpi-tile__sub">
            @if (customer()!.creditLimit > 0) { {{ utilizacion() }}% utilizado } @else { Sin límite }
          </div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Disponible</div>
          <div class="nx-kpi-tile__value" [style.color]="disponible() < 0 ? 'var(--nx-red-600)' : 'var(--nx-emerald-600)'">
            {{ disponible() | currency:'DOP':'symbol':'1.2-2' }}
          </div>
          <div class="nx-kpi-tile__sub">Crédito disponible</div>
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 320px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Información General</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">No. Cliente</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.no }}</dd>
                <dt class="nx-kv__k">Nombre</dt>
                <dd class="nx-kv__v" style="font-weight:var(--nx-weight-medium);">{{ customer()!.name }}</dd>
                <dt class="nx-kv__k">Dirección</dt>
                <dd class="nx-kv__v">{{ customer()!.address || '—' }}</dd>
                <dt class="nx-kv__k">Ciudad</dt>
                <dd class="nx-kv__v">{{ customer()!.city || '—' }}</dd>
                <dt class="nx-kv__k">País</dt>
                <dd class="nx-kv__v">{{ customer()!.countryRegionCode || '—' }}</dd>
                <dt class="nx-kv__k">Contacto</dt>
                <dd class="nx-kv__v">{{ customer()!.contact || '—' }}</dd>
                <dt class="nx-kv__k">Teléfono</dt>
                <dd class="nx-kv__v nx-kv__v--mono">
                  @if (customer()!.phoneNo) {
                    <a [href]="'tel:'+customer()!.phoneNo" class="nx-link">{{ customer()!.phoneNo | phone }}</a>
                  } @else { — }
                </dd>
                <dt class="nx-kv__k">Correo electrónico</dt>
                <dd class="nx-kv__v">
                  @if (customer()!.email) {
                    <a [href]="'mailto:'+customer()!.email" class="nx-link">{{ customer()!.email }}</a>
                  } @else { — }
                </dd>
                <dt class="nx-kv__k">RNC / Cédula</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.vatRegistrationNo | rnc }}</dd>
              </dl>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Facturación y Cobro</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Condición de Pago</dt>
                <dd class="nx-kv__v">{{ customer()!.paymentTermsCode || '—' }}</dd>
                <dt class="nx-kv__k">Método de Pago</dt>
                <dd class="nx-kv__v">{{ customer()!.paymentMethodCode || '—' }}</dd>
                <dt class="nx-kv__k">Vendedor</dt>
                <dd class="nx-kv__v">{{ customer()!.salespersonCode || '—' }}</dd>
                <dt class="nx-kv__k">Moneda</dt>
                <dd class="nx-kv__v">{{ customer()!.currencyCode || 'DOP (local)' }}</dd>
                <dt class="nx-kv__k">Grupo Contabilización</dt>
                <dd class="nx-kv__v">{{ customer()!.customerPostingGroup || '—' }}</dd>
              </dl>
            </div>
          </div>
        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Información Relacionada</div>
            <div class="nx-factbox__title">Resumen Financiero</div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Saldos</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">Saldo Total</span>
                <strong>{{ customer()!.balance | currency:'DOP':'':'1.0-0' }}</strong>
              </div>
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">Vencido</span>
                <strong [style.color]="customer()!.balanceDue > 0 ? 'var(--nx-red-600)' : 'inherit'">
                  {{ customer()!.balanceDue | currency:'DOP':'':'1.0-0' }}
                </strong>
              </div>
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">Límite Crédito</span>
                <strong>{{ customer()!.creditLimit | currency:'DOP':'':'1.0-0' }}</strong>
              </div>
              @if (customer()!.creditLimit > 0) {
                <div style="margin-top:var(--nx-space-1);">
                  <div style="height:6px;background:var(--nx-border);border-radius:99px;overflow:hidden;">
                    <div [style.width]="utilizacion()+'%'"
                         [style.background]="utilizacion() > 80 ? 'var(--nx-red-500)' : 'var(--nx-emerald-500)'"
                         style="height:100%;border-radius:99px;transition:width .3s;"></div>
                  </div>
                  <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);margin-top:2px;">{{ utilizacion() }}% utilizado</div>
                </div>
              }
            </div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Acciones Rápidas</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block" (click)="openEdit()">Editar Cliente</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block" (click)="openOrder()">Nueva Factura / Pedido</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block" [routerLink]="['/invoices']">Ver Facturas</button>
              @if (customer()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { ✓ Desbloquear }
                </button>
              } @else {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" style="color:var(--nx-red-500);" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { Bloquear Cliente }
                </button>
              }
            </div>
          </div>
        </div>
      </div>

    } @else {
      <div class="nx-empty">
        <div class="nx-empty__icon">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>
        </div>
        <p class="nx-empty__title">Cliente no encontrado</p>
        <a routerLink="/customers" class="nx-btn nx-btn--secondary nx-btn--sm">Volver a Clientes</a>
      </div>
    }

    <!-- Edit Modal -->
    @if (showEdit()) {
      <div class="modal-backdrop" (click)="closeEdit()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Editar: {{ customer()!.name }}</h2>
            <button class="nx-iconbtn" (click)="closeEdit()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (editError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ editError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field"><label class="nx-label">No. Cliente</label><input class="nx-input" [value]="form.no" disabled style="opacity:.6;" /></div>
              <div class="nx-field"><label class="nx-label">Nombre *</label><input class="nx-input" [(ngModel)]="form.name" /></div>
              <div class="nx-field" style="grid-column:1/-1;"><label class="nx-label">Dirección</label><input class="nx-input" [(ngModel)]="form.address" /></div>
              <div class="nx-field"><label class="nx-label">Ciudad</label><input class="nx-input" [(ngModel)]="form.city" /></div>
              <div class="nx-field"><label class="nx-label">País</label><input class="nx-input" [(ngModel)]="form.countryRegionCode" /></div>
              <div class="nx-field"><label class="nx-label">Contacto</label><input class="nx-input" [(ngModel)]="form.contact" /></div>
              <div class="nx-field"><label class="nx-label">Teléfono</label><input class="nx-input" type="tel" nxPhone [(ngModel)]="form.phoneNo" /></div>
              <div class="nx-field"><label class="nx-label">Correo Electrónico</label><input class="nx-input" type="email" [(ngModel)]="form.email" /></div>
              <div class="nx-field"><label class="nx-label">RNC / Cédula</label><input class="nx-input" nxRnc [(ngModel)]="form.vatRegistrationNo" /></div>
              <div class="nx-field"><label class="nx-label">Límite de Crédito (DOP)</label><input class="nx-input" type="number" [(ngModel)]="form.creditLimit" /></div>
              <div class="nx-field"><label class="nx-label">Condición de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentTermsCode">
                  <option value="">— Seleccionar —</option>
                  <option>15 DIAS</option><option>30 DIAS</option><option>45 DIAS</option><option>60 DIAS</option><option>CONTADO</option><option>NET 30</option><option>NET 45</option><option>90 DIAS</option>
                </select>
              </div>
              <div class="nx-field"><label class="nx-label">Método de Pago</label>
                <select class="nx-select" [(ngModel)]="form.paymentMethodCode">
                  <option value="">— Seleccionar —</option>
                  <option>EFECTIVO</option><option>TRANSFERENCIA</option><option>CHEQUE</option><option>TARJETA</option>
                </select>
              </div>
              <div class="nx-field"><label class="nx-label">Vendedor</label>
                <select class="nx-select" [(ngModel)]="form.salespersonCode">
                  <option value="">— Sin asignar —</option>
                  <option>RMT</option><option>MAV</option><option>JAR</option><option>LGM</option>
                </select>
              </div>
              <div class="nx-field"><label class="nx-label">Grupo Contabilización</label>
                <select class="nx-select" [(ngModel)]="form.customerPostingGroup">
                  <option value="">— Seleccionar —</option>
                  <option>DOMESTIC</option><option>EXPORT</option><option>GOVERNMENT</option><option>FOREIGN</option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeEdit()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveEdit()">
              @if (saving()) { Guardando… } @else { Guardar Cambios }
            </button>
          </div>
        </div>
      </div>
    }

    <!-- Nueva Factura / Pedido Modal -->
    @if (showOrder()) {
      <div class="modal-backdrop" (click)="closeOrder()">
        <div class="modal-box modal-box--wide" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <div>
              <div class="nx-eyebrow" style="margin-bottom:var(--nx-space-1);">{{ docTypeFullLabel(orderForm.documentType) }}</div>
              <h2 class="nx-page-title" style="margin:0;font-size:var(--nx-text-xl);">
                Para <span style="color:var(--nx-action);">{{ customer()!.name }}</span>
              </h2>
            </div>
            <button class="nx-iconbtn" (click)="closeOrder()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (orderError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
                <strong>No se pudo crear el pedido.</strong>
                <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">{{ orderError() }}</p>
              </div>
            }
            @if (orderSuccess()) {
              <div class="nx-callout nx-callout--success" style="margin-bottom:var(--nx-space-4);">
                <strong>Pedido creado correctamente.</strong>
                <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">
                  No. <span style="font-family:var(--nx-font-mono);">{{ orderSuccess() }}</span>
                  &mdash; <a [routerLink]="['/sales', orderSuccess()]" class="nx-link">Ver pedido</a>
                </p>
              </div>
            }
            <!-- Header fields -->
            <div class="form-grid" style="margin-bottom:var(--nx-space-4);">
              <div class="nx-field">
                <label class="nx-label">Tipo de Documento <span style="color:var(--nx-red-500);">*</span></label>
                <select class="nx-select" [(ngModel)]="orderForm.documentType" (ngModelChange)="onDocTypeChange()">
                  <option value="Order">Pedido de Venta</option>
                  <option value="Invoice">Factura Directa</option>
                  <option value="Quote">Cotización</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">No. del Documento</label>
                <div style="display:flex;gap:var(--nx-space-2);">
                  <input class="nx-input" style="font-family:var(--nx-font-mono);flex:1;" [value]="orderForm.documentNo || '(Autogenerado)'" disabled />
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" [disabled]="loadingNextNo()" (click)="refreshNextNo()" title="Obtener siguiente número de la secuencia">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
                  </button>
                </div>
              </div>
              <div class="nx-field">
                <label class="nx-label">Ref. del cliente</label>
                <input class="nx-input" [(ngModel)]="orderForm.externalDocumentNo" placeholder="No. de orden del cliente" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha del Pedido <span style="color:var(--nx-red-500);">*</span></label>
                <input class="nx-input" type="date" [(ngModel)]="orderForm.postingDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de Vencimiento</label>
                <input class="nx-input" type="date" [(ngModel)]="orderForm.dueDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Condición de Pago</label>
                <select class="nx-select" [(ngModel)]="orderForm.paymentTermsCode">
                  <option value="">Seleccionar…</option>
                  <option>CONTADO</option><option>15 DIAS</option><option>30 DIAS</option><option>45 DIAS</option><option>60 DIAS</option><option>90 DIAS</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Método de Pago</label>
                <select class="nx-select" [(ngModel)]="orderForm.paymentMethodCode">
                  <option value="">Seleccionar…</option>
                  <option>EFECTIVO</option><option>TRANSFERENCIA</option><option>CHEQUE</option><option>TARJETA</option>
                </select>
              </div>
            </div>

            <!-- Lines -->
            <div style="margin-bottom:var(--nx-space-3);">
              <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-2);">
                <span style="font-weight:var(--nx-weight-semibold);font-size:var(--nx-text-sm);">Líneas del Pedido <span style="color:var(--nx-red-500);">*</span></span>
                <div style="display:flex;gap:var(--nx-space-2);">
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine('Item')">+ Producto</button>
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine('Service')">+ Servicio</button>
                </div>
              </div>
              <div style="overflow-x:auto;">
                <table class="nx-table" style="min-width:760px;">
                  <thead>
                    <tr>
                      <th style="width:34px;">#</th>
                      <th style="width:70px;">Tipo</th>
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
                    @for (line of orderLines; track $index; let i = $index) {
                      <tr>
                        <td style="color:var(--nx-text-faint);font-size:var(--nx-text-xs);font-family:var(--nx-font-mono);">{{ (i + 1) * 10000 }}</td>
                        <td>
                          <span class="nx-badge nx-badge--outline" style="font-size:10px;">{{ line.type === 'Item' ? 'Prod.' : 'Serv.' }}</span>
                        </td>
                        <td style="position:relative;">
                          <input
                            class="nx-input nx-input--sm"
                            style="width:100%;font-family:var(--nx-font-mono);"
                            [(ngModel)]="line.itemNo"
                            [placeholder]="line.type === 'Item' ? 'PROD-00001' : 'SRV-001'"
                            (focus)="onItemFocus(i)"
                            (input)="onItemInput(i)"
                            (blur)="closeSuggestionsDelayed(i)"
                            autocomplete="off"
                          />
                          @if (line.showSuggestions && line.suggestions.length > 0) {
                            <div class="item-dropdown">
                              @for (s of line.suggestions; track s.no) {
                                <button class="item-dropdown__item" (mousedown)="selectSuggestion(i, s)">
                                  <span style="font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-action);">{{ s.no }}</span>
                                  <span style="flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ s.description }}</span>
                                  <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ s.unitPrice | number:'1.2-2' }}</span>
                                </button>
                              }
                            </div>
                          }
                        </td>
                        <td>
                          <input class="nx-input nx-input--sm" [(ngModel)]="line.description" placeholder="Descripción" style="width:100%;" />
                        </td>
                        <td>
                          <input class="nx-input nx-input--sm" [(ngModel)]="line.unitOfMeasure" style="width:54px;" placeholder="UND" />
                        </td>
                        <td>
                          <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.quantity" type="number" min="0.001" step="0.001" style="width:64px;" />
                        </td>
                        <td>
                          <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.unitPrice" type="number" min="0" step="0.01" style="width:94px;" />
                        </td>
                        <td>
                          <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.lineDiscountPct" type="number" min="0" max="100" style="width:54px;" />
                        </td>
                        <td class="nx-td--num">
                          <span class="nx-num">{{ lineAmount(line) | number:'1.2-2' }}</span>
                        </td>
                        <td>
                          <button class="nx-iconbtn nx-iconbtn--sm" (click)="removeLine(i)" title="Eliminar" style="color:var(--nx-red-500);">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/></svg>
                          </button>
                        </td>
                      </tr>
                    }
                    @if (orderLines.length === 0) {
                      <tr><td colspan="10" style="text-align:center;color:var(--nx-text-muted);padding:var(--nx-space-5);">Agrega productos o servicios con los botones de arriba</td></tr>
                    }
                  </tbody>
                  <tfoot>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">Subtotal:</td>
                      <td class="nx-td--num nx-num">{{ orderSubtotal() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">ITBIS (18%):</td>
                      <td class="nx-td--num nx-num">{{ orderItbis() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-bold);">Total c/ITBIS:</td>
                      <td class="nx-td--num nx-num" style="font-weight:var(--nx-weight-bold);font-size:1.05rem;">{{ orderTotal() | number:'1.2-2' }}</td>
                      <td></td>
                    </tr>
                  </tfoot>
                </table>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeOrder()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="orderSaving()" (click)="saveOrder()">
              @if (orderSaving()) { Guardando… } @else { Crear {{ docTypeShortLabel(orderForm.documentType) }} }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:0 auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .kpi-row { display:grid;grid-template-columns:repeat(4,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-4); }
    .nx-kpi-tile { background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-lg);padding:var(--nx-space-4);display:flex;flex-direction:column;gap:2px; }
    .nx-kpi-tile__label { font-size:var(--nx-text-xs);color:var(--nx-text-muted);text-transform:uppercase;letter-spacing:.06em; }
    .nx-kpi-tile__value { font-size:1.4rem;font-weight:var(--nx-weight-semibold);font-variant-numeric:tabular-nums;line-height:1.2; }
    .nx-kpi-tile__sub { font-size:var(--nx-text-xs);color:var(--nx-text-muted); }
    .nx-kv--2col { display:grid;grid-template-columns:max-content 1fr; }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(720px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-box--wide { width:min(960px,96vw); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
    .nx-input--sm { padding:4px 8px;font-size:var(--nx-text-sm); }
    .nx-field__hint { font-size:var(--nx-text-xs);color:var(--nx-text-muted);margin-top:2px; }
    .item-dropdown {
      position:absolute;top:100%;left:0;right:0;z-index:200;
      background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-md);
      box-shadow:var(--nx-shadow-lg);max-height:200px;overflow-y:auto;
    }
    .item-dropdown__item {
      display:flex;align-items:center;gap:var(--nx-space-2);
      padding:6px 10px;width:100%;text-align:left;background:none;border:none;cursor:pointer;
      font-size:var(--nx-text-sm);color:var(--nx-text-base);
    }
    .item-dropdown__item:hover { background:var(--nx-canvas-alt); }
    @media (max-width:900px) { .kpi-row { grid-template-columns:1fr 1fr; } }
    @media (max-width:600px) { .kpi-row { grid-template-columns:1fr; } .form-grid { grid-template-columns:1fr; } }
  `]
})
export class CustomerCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(CustomerService);
  private readonly invoiceSvc = inject(InvoiceService);
  private readonly itemSvc = inject(ItemService);
  private readonly api = inject(ApiService);

  customer = signal<Customer | null>(null);
  loading = signal(true);
  actionLoading = signal(false);

  showEdit = signal(false);
  saving = signal(false);
  editError = signal<string | null>(null);
  form: CustomerFormData = {} as CustomerFormData;

  showOrder = signal(false);
  orderSaving = signal(false);
  orderError = signal<string | null>(null);
  orderSuccess = signal<string | null>(null);
  loadingNextNo = signal(false);
  orderLines: OrderLine[] = [];
  orderForm = {
    documentType: 'Order',
    documentNo: '',
    externalDocumentNo: '',
    postingDate: '',
    dueDate: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
  };

  private previewNoForOrder = '';
  private allItems: import('../../../../inventory/data/item.service').ItemListItem[] = [];

  utilizacion(): number {
    const c = this.customer();
    if (!c || c.creditLimit <= 0) return 0;
    return Math.min(100, Math.round((c.balance / c.creditLimit) * 100));
  }

  disponible(): number {
    const c = this.customer();
    if (!c) return 0;
    return c.creditLimit > 0 ? c.creditLimit - c.balance : 0;
  }

  lineAmount(line: OrderLine): number {
    const base = line.quantity * line.unitPrice;
    const disc = base * (line.lineDiscountPct / 100);
    return base - disc;
  }

  orderSubtotal(): number { return this.orderLines.reduce((s, l) => s + this.lineAmount(l), 0); }
  orderItbis(): number { return this.orderSubtotal() * 0.18; }
  orderTotal(): number { return this.orderSubtotal() * 1.18; }

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const c = await this.svc.getByNo(no);
    this.customer.set(c);
    this.loading.set(false);
    // Pre-fill order defaults from customer
    if (c) {
      this.orderForm.paymentTermsCode = c.paymentTermsCode;
      this.orderForm.paymentMethodCode = c.paymentMethodCode;
      this.orderForm.postingDate = new Date().toISOString().slice(0, 10);
    }
  }

  openEdit(): void {
    const c = this.customer()!;
    this.form = { no: c.no, name: c.name, address: c.address, city: c.city, contact: c.contact, phoneNo: c.phoneNo, email: c.email, creditLimit: c.creditLimit, vatRegistrationNo: c.vatRegistrationNo, paymentTermsCode: c.paymentTermsCode, paymentMethodCode: c.paymentMethodCode, salespersonCode: c.salespersonCode, currencyCode: c.currencyCode, customerPostingGroup: c.customerPostingGroup, countryRegionCode: c.countryRegionCode };
    this.editError.set(null);
    this.showEdit.set(true);
  }

  closeEdit(): void { this.showEdit.set(false); }

  async saveEdit(): Promise<void> {
    if (!this.form.name?.trim()) { this.editError.set('El nombre es obligatorio.'); return; }
    this.saving.set(true);
    this.editError.set(null);
    try {
      await this.svc.update(this.customer()!.no, this.form);
      const updated = await this.svc.getByNo(this.customer()!.no);
      this.customer.set(updated);
      this.closeEdit();
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.saving.set(false);
    }
  }

  async toggleBlock(): Promise<void> {
    const c = this.customer();
    if (!c) return;
    this.actionLoading.set(true);
    try {
      if (c.blocked) await this.svc.unblock(c.no);
      else await this.svc.block(c.no);
      const updated = await this.svc.getByNo(c.no);
      this.customer.set(updated);
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al cambiar el estado del cliente.');
    } finally {
      this.actionLoading.set(false);
    }
  }

  docTypeFullLabel(t: string): string {
    return { Order: 'Nuevo Pedido de Venta', Invoice: 'Nueva Factura Directa', Quote: 'Nueva Cotización' }[t] ?? 'Nuevo Documento';
  }
  docTypeShortLabel(t: string): string {
    return { Order: 'Pedido', Invoice: 'Factura', Quote: 'Cotización' }[t] ?? 'Documento';
  }

  openOrder(): void {
    this.orderLines = [];
    this.orderError.set(null);
    this.orderSuccess.set(null);
    this.orderForm.documentNo = '';
    this.showOrder.set(true);
    this.loadAllItems();
    this.refreshNextNo();
  }

  async onDocTypeChange(): Promise<void> {
    this.orderForm.documentNo = '';
    await this.refreshNextNo();
  }

  async refreshNextNo(): Promise<void> {
    const seriesCode = this.seriesForDocType(this.orderForm.documentType);
    this.loadingNextNo.set(true);
    try {
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>(`administration/no-series/${seriesCode}/next`));
      this.previewNoForOrder = res.nextNo;
      this.orderForm.documentNo = res.nextNo;
    } catch {
      this.previewNoForOrder = '';
      this.orderForm.documentNo = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  private seriesForDocType(docType: string): string {
    return { Order: 'PV', Quote: 'COT', Invoice: 'FAC' }[docType] ?? 'PV';
  }

  private async loadAllItems(): Promise<void> {
    if (this.allItems.length > 0) return;
    try {
      await this.itemSvc.load({ pageSize: 500 });
      this.allItems = this.itemSvc.items();
    } catch { /* ignore */ }
  }

  closeOrder(): void { this.showOrder.set(false); }

  addLine(type: 'Item' | 'Service' = 'Item'): void {
    this.orderLines = [...this.orderLines, {
      itemNo: '', description: '', quantity: 1, unitPrice: 0,
      lineDiscountPct: 0, unitOfMeasure: 'UND', type, suggestions: [], showSuggestions: false
    }];
  }

  removeLine(i: number): void {
    this.orderLines = this.orderLines.filter((_, idx) => idx !== i);
  }

  onItemFocus(i: number): void {
    const line = this.orderLines[i];
    const q = line.itemNo.toLowerCase().trim();
    let filtered = this.allItems;
    if (line.type === 'Item') filtered = filtered.filter(it => it.type === 'Inventory');
    else if (line.type === 'Service') filtered = filtered.filter(it => it.type === 'Service');

    if (!q) {
      const suggestions = filtered.slice(0, 10);
      this.orderLines[i] = { ...line, suggestions, showSuggestions: suggestions.length > 0 };
    }
  }

  onItemInput(i: number): void {
    const line = this.orderLines[i];
    const q = line.itemNo.toLowerCase().trim();
    let filtered = this.allItems;
    if (line.type === 'Item') filtered = filtered.filter(it => it.type === 'Inventory');
    else if (line.type === 'Service') filtered = filtered.filter(it => it.type === 'Service');

    if (!q) {
      const suggestions = filtered.slice(0, 10);
      this.orderLines[i] = { ...line, suggestions, showSuggestions: suggestions.length > 0 };
      return;
    }
    const suggestions = filtered
      .filter(it => it.no.toLowerCase().includes(q) || it.description.toLowerCase().includes(q))
      .slice(0, 8);
    this.orderLines[i] = { ...line, suggestions, showSuggestions: suggestions.length > 0 };
  }

  selectSuggestion(i: number, item: import('../../../../inventory/data/item.service').ItemListItem): void {
    this.orderLines[i] = {
      ...this.orderLines[i],
      itemNo: item.no,
      description: item.description,
      unitPrice: item.unitPrice,
      unitOfMeasure: item.baseUnitOfMeasure,
      suggestions: [],
      showSuggestions: false,
    };
  }

  closeSuggestionsDelayed(i: number): void {
    setTimeout(() => {
      if (this.orderLines[i]) {
        this.orderLines[i] = { ...this.orderLines[i], showSuggestions: false };
      }
    }, 200);
  }

  async saveOrder(): Promise<void> {
    this.orderError.set(null);
    this.orderSuccess.set(null);

    // Frontend validation
    if (!this.orderForm.postingDate) {
      this.orderError.set('La fecha del pedido es obligatoria.');
      return;
    }
    if (this.orderLines.length === 0) {
      this.orderError.set('Debes agregar al menos una línea al pedido.');
      return;
    }
    const emptyLine = this.orderLines.find(l => !l.description?.trim());
    if (emptyLine) {
      this.orderError.set('Todas las líneas deben tener una descripción.');
      return;
    }
    const zeroQty = this.orderLines.find(l => !l.quantity || l.quantity <= 0);
    if (zeroQty) {
      this.orderError.set('Todas las líneas deben tener una cantidad mayor a cero.');
      return;
    }

    this.orderSaving.set(true);
    try {
      const c = this.customer()!;
      const seriesCode = this.seriesForDocType(this.orderForm.documentType);
      const typedNo = this.orderForm.documentNo?.trim() || '';
      // Use seriesCode when: field is empty OR user left the preview value unchanged
      const useManual = typedNo && typedNo !== this.previewNoForOrder;
      const data: CreateSalesOrderData = {
        documentType: this.orderForm.documentType,
        sellToCustomerNo: c.no,
        sellToCustomerName: c.name,
        externalDocumentNo: this.orderForm.externalDocumentNo,
        currencyCode: c.currencyCode || '',
        paymentTermsCode: this.orderForm.paymentTermsCode,
        paymentMethodCode: this.orderForm.paymentMethodCode,
        salespersonCode: c.salespersonCode,
        postingDate: this.orderForm.postingDate,
        dueDate: this.orderForm.dueDate || null,
        seriesCode: useManual ? null : seriesCode,
        manualNo: useManual ? typedNo : null,
        lines: this.orderLines.map(l => ({
          itemNo: l.itemNo,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          unitOfMeasure: l.unitOfMeasure,
          lineType: l.type,
        })),
      };
      const no = await this.invoiceSvc.createOrder(data);
      this.orderSuccess.set(no);
      this.orderLines = [];
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('sequence')) {
        this.orderError.set('No hay una secuencia de numeración configurada para este tipo de documento. Ve a Configuración > Secuencias para crearla.');
      } else if (msg) {
        this.orderError.set(msg);
      } else {
        this.orderError.set('Ocurrió un error al guardar el pedido. Intenta de nuevo.');
      }
    } finally {
      this.orderSaving.set(false);
    }
  }
}
