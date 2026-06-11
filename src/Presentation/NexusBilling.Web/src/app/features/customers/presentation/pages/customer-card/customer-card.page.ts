import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CustomerService } from '../../data/customer.service';
import { InvoiceService } from '../../../sales/data/invoice.service';
import { Customer } from '../../domain/customer.model';
import { Invoice } from '../../../sales/domain/invoice.model';

@Component({
  selector: 'app-customer-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    @if (customer()) {
      <!-- Breadcrumb -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/customers">Clientes</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ customer()!.no }}</span>
      </nav>

      <!-- Page header -->
      <div class="nx-page-header">
        <div class="nx-avatar nx-avatar--lg" style="background:var(--nx-emerald-100);color:var(--nx-emerald-800);">
          {{ customer()!.name.charAt(0) }}
        </div>
        <div>
          <h1 class="nx-page-title">{{ customer()!.name }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);">
            <span class="nx-eyebrow">{{ customer()!.no }}</span>
            @if (customer()!.blocked) {
              <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
            } @else {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
            }
            @if (customer()!.currencyCode) {
              <span class="nx-badge nx-badge--outline">{{ customer()!.currencyCode }}</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm">Editar</button>
          <button class="nx-btn nx-btn--primary nx-btn--sm">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
            Nueva Factura
          </button>
        </div>
      </div>

      <!-- KPI tiles -->
      <div style="display:grid;grid-template-columns:repeat(auto-fit,minmax(160px,1fr));gap:var(--nx-space-4);margin-bottom:var(--nx-space-6);">
        <div class="nx-stat">
          <span class="nx-stat__label">Saldo Total</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">
            RD$ {{ (customer()!.balance ?? 0) | number:'1.0-0' }}
          </span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Saldo Vencido</span>
          <span class="nx-stat__value nx-num" [class.nx-amount--negative]="(customer()!.balanceDue ?? 0) > 0" style="font-size:var(--nx-text-2xl);">
            RD$ {{ (customer()!.balanceDue ?? 0) | number:'1.0-0' }}
          </span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Límite de Crédito</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">
            RD$ {{ (customer()!.creditLimit ?? 0) | number:'1.0-0' }}
          </span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Facturas Abiertas</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">
            {{ openInvoices().length }}
          </span>
        </div>
      </div>

      <!-- Two-column detail layout -->
      <div style="display:grid;grid-template-columns:1fr 340px;gap:var(--nx-space-4);align-items:start;">

        <!-- General info -->
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- General section -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Información General</div>
            </div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled">
                <dt class="nx-kv__k">No. Cliente</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.no }}</dd>

                <dt class="nx-kv__k">Dirección</dt>
                <dd class="nx-kv__v">{{ customer()!.address }}</dd>

                <dt class="nx-kv__k">Ciudad</dt>
                <dd class="nx-kv__v">{{ customer()!.city }}</dd>

                <dt class="nx-kv__k">País</dt>
                <dd class="nx-kv__v">{{ customer()!.countryRegionCode || '—' }}</dd>

                <dt class="nx-kv__k">Contacto</dt>
                <dd class="nx-kv__v">{{ customer()!.contact || '—' }}</dd>

                <dt class="nx-kv__k">Teléfono</dt>
                <dd class="nx-kv__v">{{ customer()!.phoneNo || '—' }}</dd>

                <dt class="nx-kv__k">Email</dt>
                <dd class="nx-kv__v">
                  @if (customer()!.email) {
                    <a [href]="'mailto:' + customer()!.email" class="nx-link">{{ customer()!.email }}</a>
                  } @else { — }
                </dd>

                <dt class="nx-kv__k">RNC / RNC VAT</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.vatRegistrationNo || '—' }}</dd>
              </dl>
            </div>
          </div>

          <!-- Payment / Terms -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Facturación y Cobro</div>
            </div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled">
                <dt class="nx-kv__k">Grupo de Contabilización</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.customerPostingGroup || '—' }}</dd>

                <dt class="nx-kv__k">Condición de Pago</dt>
                <dd class="nx-kv__v">{{ customer()!.paymentTermsCode || '—' }}</dd>

                <dt class="nx-kv__k">Moneda</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ customer()!.currencyCode || 'DOP (local)' }}</dd>

                <dt class="nx-kv__k">Vendedor</dt>
                <dd class="nx-kv__v">{{ customer()!.salespersonCode || '—' }}</dd>
              </dl>
            </div>
          </div>

          <!-- Invoices -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Historial de Facturas</div>
              <div class="nx-card__actions">
                <a routerLink="/invoices" [queryParams]="{customer: customer()!.no}" class="nx-btn nx-btn--ghost nx-btn--sm">Ver todas</a>
              </div>
            </div>
            @if (customerInvoices().length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-8) var(--nx-space-6);">
                <p class="nx-empty__text">No hay facturas para este cliente.</p>
              </div>
            } @else {
              <div style="overflow-x:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th>No.</th>
                      <th>Fecha</th>
                      <th>Vencimiento</th>
                      <th>Estado</th>
                      <th class="nx-th--num">Importe</th>
                      <th class="nx-th--num">Saldo</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (inv of customerInvoices(); track inv.no) {
                      <tr>
                        <td class="nx-td--doc">
                          <a [routerLink]="['/invoices', inv.no]" class="nx-link">{{ inv.no }}</a>
                        </td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.postingDate }}</td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.dueDate || '—' }}</td>
                        <td>
                          <span class="nx-badge" [class]="statusClass(inv.status)">
                            <span class="nx-badge__dot"></span>{{ statusLabel(inv.status) }}
                          </span>
                        </td>
                        <td class="nx-td--num nx-num">{{ inv.amountIncludingVat | number:'1.2-2' }}</td>
                        <td class="nx-td--num">
                          <span class="nx-amount" [class.nx-amount--negative]="inv.remainingAmount > 0" [class.nx-amount--zero]="inv.remainingAmount === 0">
                            {{ inv.remainingAmount | number:'1.2-2' }}
                          </span>
                        </td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
            }
          </div>
        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Información Relacionada</div>
            <div class="nx-factbox__title">Resumen del Cliente</div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Ventas</div>
            <dl class="nx-kv" style="grid-template-columns:1fr 1fr;">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Total facturas</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ customerInvoices().length }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Abiertas</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ openInvoices().length }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Vencidas</dt>
              <dd class="nx-kv__v nx-kv__v--mono nx-amount--negative" style="font-size:var(--nx-text-sm);">{{ overdueInvoices().length }}</dd>
            </dl>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Saldos</div>
            <dl class="nx-kv" style="grid-template-columns:1fr 1fr;">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Saldo</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">RD$ {{ (customer()!.balance ?? 0) | number:'1.0-0' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Vencido</dt>
              <dd class="nx-kv__v nx-kv__v--mono nx-amount--negative" style="font-size:var(--nx-text-sm);">RD$ {{ (customer()!.balanceDue ?? 0) | number:'1.0-0' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Límite</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">RD$ {{ (customer()!.creditLimit ?? 0) | number:'1.0-0' }}</dd>
            </dl>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Acciones Rápidas</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block">Ver Libro Mayor</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block">Crear Orden de Venta</button>
              @if (customer()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block">Desbloquear</button>
              } @else {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" style="color:var(--nx-red-500);">Bloquear</button>
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
  `,
  styles: [':host { display: block; }']
})
export class CustomerCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly customerSvc = inject(CustomerService);
  private readonly invoiceSvc = inject(InvoiceService);

  customer = signal<Customer | undefined>(undefined);
  customerInvoices = signal<Invoice[]>([]);
  openInvoices = signal<Invoice[]>([]);
  overdueInvoices = signal<Invoice[]>([]);

  ngOnInit(): void {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    const c = this.customerSvc.getByNo(no);
    this.customer.set(c);

    if (c) {
      const invs = this.invoiceSvc.filter({ customerId: no });
      this.customerInvoices.set(invs);
      this.openInvoices.set(invs.filter(i => i.status === 'open'));
      this.overdueInvoices.set(invs.filter(i => i.status === 'overdue'));
    }
  }

  statusClass(s: string): string {
    return { open: 'nx-badge--info', posted: 'nx-badge--success', overdue: 'nx-badge--danger', paid: 'nx-badge--outline' }[s] ?? '';
  }
  statusLabel(s: string): string {
    return { open: 'Abierta', posted: 'Publicada', overdue: 'Vencida', paid: 'Pagada' }[s] ?? s;
  }
}
