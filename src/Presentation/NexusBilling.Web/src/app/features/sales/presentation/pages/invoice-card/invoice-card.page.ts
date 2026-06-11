import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { InvoiceService } from '../../../data/invoice.service';
import { Invoice, InvoiceLine } from '../../../domain/invoice.model';

@Component({
  selector: 'app-invoice-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    @if (invoice()) {
      <!-- Breadcrumb -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/invoices">Facturas</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ invoice()!.no }}</span>
      </nav>

      <!-- Header -->
      <div class="nx-page-header">
        <div>
          <h1 class="nx-page-title">{{ invoice()!.no }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);">
            <span class="nx-badge" [class]="statusClass(invoice()!.status)">
              <span class="nx-badge__dot"></span>{{ statusLabel(invoice()!.status) }}
            </span>
            @if (invoice()!.currencyCode) {
              <span class="nx-badge nx-badge--outline">{{ invoice()!.currencyCode }}</span>
            }
            @if (invoice()!.externalDocumentNo) {
              <span class="nx-eyebrow">Ref: {{ invoice()!.externalDocumentNo }}</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="6 9 6 2 18 2 18 9"/><path d="M6 18H4a2 2 0 0 1-2-2v-5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-2"/><rect x="6" y="14" width="12" height="8"/></svg>
            Imprimir
          </button>
          <button class="nx-btn nx-btn--secondary nx-btn--sm">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/><polyline points="22,6 12,13 2,6"/></svg>
            Enviar
          </button>
          @if (invoice()!.status === 'open' || invoice()!.status === 'posted') {
            <button class="nx-btn nx-btn--primary nx-btn--sm">Registrar Pago</button>
          }
        </div>
      </div>

      <!-- Content grid -->
      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">

        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- General section -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">General</div></div>
            <div class="nx-card__body">
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:0 var(--nx-space-6);">
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">No. Factura</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.no }}</dd>

                  <dt class="nx-kv__k">No. Pedido</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.orderNo || '—' }}</dd>

                  <dt class="nx-kv__k">Ref. Externa</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.externalDocumentNo || '—' }}</dd>

                  <dt class="nx-kv__k">Vendedor</dt>
                  <dd class="nx-kv__v">{{ invoice()!.salespersonCode || '—' }}</dd>
                </dl>
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">Fecha Registro</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.postingDate }}</dd>

                  <dt class="nx-kv__k">Fecha Vencimiento</dt>
                  <dd class="nx-kv__v nx-kv__v--mono" [style.color]="invoice()!.status === 'overdue' ? 'var(--nx-red-500)' : ''">
                    {{ invoice()!.dueDate || '—' }}
                  </dd>

                  <dt class="nx-kv__k">Condición Pago</dt>
                  <dd class="nx-kv__v">{{ invoice()!.paymentTermsCode || '—' }}</dd>

                  <dt class="nx-kv__k">Método Pago</dt>
                  <dd class="nx-kv__v">{{ invoice()!.paymentMethodCode || '—' }}</dd>
                </dl>
              </div>
            </div>
          </div>

          <!-- Cliente section -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Cliente</div>
              <div class="nx-card__actions">
                <a [routerLink]="['/customers', invoice()!.sellToCustomerNo]" class="nx-btn nx-btn--ghost nx-btn--sm">Ver ficha</a>
              </div>
            </div>
            <div class="nx-card__body">
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:0 var(--nx-space-6);">
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">No. Cliente</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">
                    <a [routerLink]="['/customers', invoice()!.sellToCustomerNo]" class="nx-link">{{ invoice()!.sellToCustomerNo }}</a>
                  </dd>
                  <dt class="nx-kv__k">Nombre</dt>
                  <dd class="nx-kv__v">{{ invoice()!.sellToCustomerName }}</dd>
                  <dt class="nx-kv__k">Facturar A</dt>
                  <dd class="nx-kv__v">{{ invoice()!.billToName }}</dd>
                </dl>
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">Moneda</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.currencyCode || 'DOP' }}</dd>
                </dl>
              </div>
            </div>
          </div>

          <!-- Lines -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Líneas de Factura</div></div>
            @if (lines().length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-8);">
                <p class="nx-empty__text">No hay líneas de detalle disponibles.</p>
              </div>
            } @else {
              <div style="overflow-x:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th>No. Línea</th>
                      <th>Tipo</th>
                      <th>No. Artículo</th>
                      <th>Descripción</th>
                      <th class="nx-th--num">Cantidad</th>
                      <th>U/M</th>
                      <th class="nx-th--num">Precio Unit.</th>
                      <th class="nx-th--num">% Dto</th>
                      <th class="nx-th--num">Importe</th>
                      <th class="nx-th--num">Importe c/IVA</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (line of lines(); track line.lineNo) {
                      <tr>
                        <td class="nx-td--doc nx-num">{{ line.lineNo }}</td>
                        <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ line.type }}</td>
                        <td class="nx-td--doc">
                          @if (line.type === 'Item') {
                            <a [routerLink]="['/inventory', line.no]" class="nx-link">{{ line.no }}</a>
                          } @else {
                            {{ line.no }}
                          }
                        </td>
                        <td>{{ line.description }}</td>
                        <td class="nx-td--num nx-num">{{ line.quantity | number:'1.0-2' }}</td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ line.unitOfMeasureCode }}</td>
                        <td class="nx-td--num nx-num">{{ line.unitPrice | number:'1.2-2' }}</td>
                        <td class="nx-td--num nx-num">{{ line.lineDiscountPct | number:'1.1-1' }}%</td>
                        <td class="nx-td--num nx-num">{{ line.amount | number:'1.2-2' }}</td>
                        <td class="nx-td--num nx-num" style="font-weight:var(--nx-weight-semibold);">{{ line.amountIncludingVat | number:'1.2-2' }}</td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
              <!-- Totals footer -->
              <div class="nx-card__foot" style="justify-content:flex-end;">
                <dl class="nx-kv" style="grid-template-columns:auto auto;gap:var(--nx-space-1) var(--nx-space-6);text-align:right;">
                  <dt class="nx-kv__k">Subtotal</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ invoice()!.amount | number:'1.2-2' }}</dd>
                  <dt class="nx-kv__k">IVA (18%)</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ (invoice()!.amountIncludingVat - invoice()!.amount) | number:'1.2-2' }}</dd>
                  <dt class="nx-kv__k" style="font-weight:var(--nx-weight-bold);color:var(--nx-text-strong);">Total</dt>
                  <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-lg);font-weight:var(--nx-weight-bold);">
                    {{ invoice()!.currencyCode || 'RD$' }} {{ invoice()!.amountIncludingVat | number:'1.2-2' }}
                  </dd>
                  @if (invoice()!.remainingAmount > 0) {
                    <dt class="nx-kv__k nx-amount--negative">Saldo Pendiente</dt>
                    <dd class="nx-kv__v nx-kv__v--mono nx-amount--negative">{{ invoice()!.remainingAmount | number:'1.2-2' }}</dd>
                  }
                </dl>
              </div>
            }
          </div>

        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Factura</div>
            <div class="nx-factbox__title">{{ invoice()!.no }}</div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Importes</div>
            <dl class="nx-kv">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Subtotal</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ invoice()!.amount | number:'1.2-2' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Total c/IVA</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);font-weight:var(--nx-weight-bold);">{{ invoice()!.amountIncludingVat | number:'1.2-2' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Saldo</dt>
              <dd class="nx-kv__v nx-kv__v--mono nx-amount" style="font-size:var(--nx-text-sm);" [class.nx-amount--negative]="invoice()!.remainingAmount > 0" [class.nx-amount--zero]="invoice()!.remainingAmount === 0">
                {{ invoice()!.remainingAmount | number:'1.2-2' }}
              </dd>
            </dl>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Acciones</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block">Ver Movimientos</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block">Nota de Crédito</button>
              <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block">Exportar PDF</button>
            </div>
          </div>
        </div>

      </div>
    } @else {
      <div class="nx-empty">
        <div class="nx-empty__icon"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg></div>
        <p class="nx-empty__title">Factura no encontrada</p>
        <a routerLink="/invoices" class="nx-btn nx-btn--secondary nx-btn--sm">Volver a Facturas</a>
      </div>
    }
  `,
  styles: [':host { display: block; }']
})
export class InvoiceCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(InvoiceService);

  invoice = signal<Invoice | undefined>(undefined);
  lines = signal<InvoiceLine[]>([]);

  ngOnInit(): void {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.invoice.set(this.svc.getByNo(no));
    this.lines.set(this.svc.getLinesForInvoice(no));
  }

  statusClass(s: string): string {
    return { open: 'nx-badge--info', posted: 'nx-badge--success', overdue: 'nx-badge--danger', paid: 'nx-badge--outline' }[s] ?? '';
  }
  statusLabel(s: string): string {
    return { open: 'Abierta', posted: 'Publicada', overdue: 'Vencida', paid: 'Pagada' }[s] ?? s;
  }
}
