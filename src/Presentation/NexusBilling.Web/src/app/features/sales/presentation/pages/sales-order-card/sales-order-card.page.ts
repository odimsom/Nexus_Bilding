import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InvoiceService } from '../../../data/invoice.service';
import { SalesOrderDetail } from '../../../domain/invoice.model';

@Component({
  selector: 'app-sales-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando orden…</p>
      </div>
    } @else if (order()) {

      <!-- Breadcrumb -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/sales">Órdenes de venta</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ order()!.no }}</span>
      </nav>

      <!-- Page header -->
      <div class="nx-page-header">
        <div style="flex:1;min-width:0;">
          <div style="display:flex;align-items:center;gap:var(--nx-space-2);margin-bottom:var(--nx-space-1);">
            <span class="nx-badge nx-badge--outline">{{ docTypeLabel(order()!.documentType) }}</span>
          </div>
          <h1 class="nx-page-title" style="font-family:var(--nx-font-mono);letter-spacing:0;">{{ order()!.no }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);">
            <span class="nx-badge" [class]="statusClass(order()!.status)">
              <span class="nx-badge__dot"></span>{{ statusLabel(order()!.status) }}
            </span>
            @if (order()!.externalDocumentNo) {
              <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
                Ref: <span style="font-family:var(--nx-font-mono);">{{ order()!.externalDocumentNo }}</span>
              </span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm" disabled>
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
            Editar
          </button>
          @if (order()!.status === 'Open') {
            <button class="nx-btn nx-btn--subtle nx-btn--sm" disabled>
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>
              Liberar
            </button>
          }
          @if (order()!.status === 'Released') {
            <button class="nx-btn nx-btn--primary nx-btn--sm" disabled>
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
              Publicar
            </button>
          }
        </div>
      </div>

      <!-- Two-column record layout -->
      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">

        <!-- Main content -->
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- General card -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Información general</div>
            </div>
            <div class="nx-card__body">
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:0 var(--nx-space-6);">
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">No.</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.no }}</dd>

                  <dt class="nx-kv__k">Tipo</dt>
                  <dd class="nx-kv__v">{{ docTypeLabel(order()!.documentType) }}</dd>

                  <dt class="nx-kv__k">Cliente</dt>
                  <dd class="nx-kv__v">
                    <a [routerLink]="['/customers', order()!.sellToCustomerNo]" class="nx-link">
                      {{ order()!.sellToCustomerName }}
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                    </a>
                  </dd>

                  <dt class="nx-kv__k">Ref. externa</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.externalDocumentNo || '—' }}</dd>

                  <dt class="nx-kv__k">Vendedor</dt>
                  <dd class="nx-kv__v">{{ order()!.salespersonCode || '—' }}</dd>
                </dl>
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">Fecha de registro</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.postingDate }}</dd>

                  <dt class="nx-kv__k">Fecha de vencimiento</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.dueDate || '—' }}</dd>

                  <dt class="nx-kv__k">Condición de pago</dt>
                  <dd class="nx-kv__v">{{ order()!.paymentTermsCode || '—' }}</dd>

                  <dt class="nx-kv__k">Método de pago</dt>
                  <dd class="nx-kv__v">{{ order()!.paymentMethodCode || '—' }}</dd>

                  <dt class="nx-kv__k">Moneda</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.currencyCode || 'DOP' }}</dd>
                </dl>
              </div>
            </div>
          </div>

          <!-- Lines card -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Líneas del documento</div>
              <div class="nx-card__actions">
                <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
                  {{ order()!.lines.length }} línea{{ order()!.lines.length !== 1 ? 's' : '' }}
                </span>
              </div>
            </div>

            @if (order()!.lines.length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-8);">
                <p class="nx-empty__text">Sin líneas de detalle.</p>
              </div>
            } @else {
              <div style="overflow-x:auto;">
                <table class="nx-table" aria-label="Líneas de la orden de venta">
                  <thead>
                    <tr>
                      <th style="width:70px;">No. línea</th>
                      <th style="width:80px;">Tipo</th>
                      <th style="width:120px;">No. artículo</th>
                      <th>Descripción</th>
                      <th class="nx-th--num" style="width:80px;">Cant.</th>
                      <th style="width:60px;">U/M</th>
                      <th class="nx-th--num" style="width:110px;">Precio unit.</th>
                      <th class="nx-th--num" style="width:70px;">% Dto.</th>
                      <th class="nx-th--num" style="width:120px;">Importe</th>
                      <th class="nx-th--num" style="width:130px;">c/IVA</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (line of order()!.lines; track line.lineNo) {
                      <tr>
                        <td class="nx-td--num" style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ line.lineNo }}</td>
                        <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ line.type }}</td>
                        <td class="nx-td--doc">
                          @if (line.type === 'Item' && line.no) {
                            <a [routerLink]="['/inventory', line.no]" class="nx-link">{{ line.no }}</a>
                          } @else {
                            {{ line.no || '—' }}
                          }
                        </td>
                        <td>{{ line.description }}</td>
                        <td class="nx-td--num">{{ line.quantity | number:'1.0-4' }}</td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ line.unitOfMeasure }}</td>
                        <td class="nx-td--num">{{ line.unitPrice | number:'1.2-2' }}</td>
                        <td class="nx-td--num">
                          @if (line.lineDiscount > 0) {
                            <span style="color:var(--nx-text-muted);">{{ line.lineDiscount | number:'1.1-2' }}%</span>
                          } @else {
                            <span style="color:var(--nx-text-faint);">—</span>
                          }
                        </td>
                        <td class="nx-td--num">{{ line.amount | number:'1.2-2' }}</td>
                        <td class="nx-td--num" style="font-weight:var(--nx-weight-semibold);">{{ line.amountIncludingVat | number:'1.2-2' }}</td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>

              <!-- Totals footer -->
              <div class="nx-card__foot" style="display:flex;justify-content:flex-end;">
                <dl class="nx-kv" style="grid-template-columns:auto auto;gap:var(--nx-space-1) var(--nx-space-6);text-align:right;min-width:280px;">
                  <dt class="nx-kv__k">Subtotal</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.amount | number:'1.2-2' }}</dd>

                  <dt class="nx-kv__k">ITBIS (18%)</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ (order()!.amountIncludingVat - order()!.amount) | number:'1.2-2' }}</dd>

                  <dt class="nx-kv__k" style="font-weight:var(--nx-weight-bold);color:var(--nx-text-strong);">Total</dt>
                  <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-lg);font-weight:var(--nx-weight-bold);color:var(--nx-text-strong);">
                    {{ order()!.currencyCode || 'RD$' }} {{ order()!.amountIncludingVat | number:'1.2-2' }}
                  </dd>
                </dl>
              </div>
            }
          </div>

        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Orden de venta</div>
            <div class="nx-factbox__title">{{ order()!.no }}</div>
          </div>

          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Importes</div>
            <dl class="nx-kv">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Subtotal</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ order()!.amount | number:'1.2-2' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">ITBIS</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ (order()!.amountIncludingVat - order()!.amount) | number:'1.2-2' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Total c/IVA</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);font-weight:var(--nx-weight-bold);">{{ order()!.amountIncludingVat | number:'1.2-2' }}</dd>
            </dl>
          </div>

          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Cliente</div>
            <dl class="nx-kv">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">No.</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ order()!.sellToCustomerNo }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Nombre</dt>
              <dd class="nx-kv__v" style="font-size:var(--nx-text-sm);">{{ order()!.sellToCustomerName }}</dd>
            </dl>
          </div>

          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Relacionado</div>
            <ul class="nx-linklist">
              <li>
                <a [routerLink]="['/customers', order()!.sellToCustomerNo]" class="nx-link">
                  Ver ficha del cliente
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                </a>
              </li>
              <li>
                <a routerLink="/invoices" class="nx-link">
                  Facturas publicadas
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                </a>
              </li>
              <li>
                <a routerLink="/sales" class="nx-link">
                  Todas las órdenes
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                </a>
              </li>
            </ul>
          </div>
        </div>

      </div>

    } @else {
      <div class="nx-empty">
        <div class="nx-empty__icon">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
        </div>
        <p class="nx-empty__title">Orden no encontrada</p>
        <p class="nx-empty__text">El documento no existe o no pertenece a este tenant.</p>
        <a routerLink="/sales" class="nx-btn nx-btn--secondary nx-btn--sm" style="margin-top:var(--nx-space-3);">Volver a órdenes</a>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner {
      width: 32px; height: 32px;
      border: 3px solid var(--nx-border); border-top-color: var(--nx-action);
      border-radius: 50%; animation: spin 0.8s linear infinite; margin: 2rem auto;
    }
    @keyframes spin { to { transform: rotate(360deg); } }
  `]
})
export class SalesOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(InvoiceService);

  order = signal<SalesOrderDetail | null>(null);
  loading = signal(true);

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    try {
      const detail = await this.svc.getOrderDetail(no);
      this.order.set(detail);
    } catch {
      this.order.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  docTypeLabel(t: string): string {
    return ({ Order: 'Pedido', Quote: 'Cotización', Invoice: 'Factura', 'Blanket Order': 'Acuerdo marco', 'Return Order': 'Dev. pedido' } as Record<string, string>)[t] ?? t;
  }

  statusClass(s: string): string {
    return ({
      Open: 'nx-badge--info',
      Released: 'nx-badge--success',
      'Pending Approval': 'nx-badge--warn',
      'Pending Prepayment': 'nx-badge--warn',
      Closed: 'nx-badge--outline',
    } as Record<string, string>)[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return ({
      Open: 'Abierta',
      Released: 'Liberada',
      'Pending Approval': 'Pend. aprobación',
      'Pending Prepayment': 'Pend. anticipo',
      Closed: 'Cerrada',
    } as Record<string, string>)[s] ?? s;
  }
}
