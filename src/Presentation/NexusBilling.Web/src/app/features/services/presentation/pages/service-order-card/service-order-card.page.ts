import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ServiceOrderService } from '../../../data/service-order.service';
import { ServiceOrderDetail } from '../../../domain/service-order.model';

@Component({
  selector: 'app-service-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando orden…</p>
      </div>
    } @else if (order()) {

      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/services">Órdenes de Servicio</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ order()!.no }}</span>
      </nav>

      <div class="nx-page-header">
        <div style="flex:1;min-width:0;">
          <div style="display:flex;align-items:center;gap:var(--nx-space-2);margin-bottom:var(--nx-space-1);">
            <span class="nx-badge nx-badge--outline">{{ order()!.documentTypeLabel }}</span>
          </div>
          <h1 class="nx-page-title" style="font-family:var(--nx-font-mono);letter-spacing:0;">{{ order()!.no }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);">
            <span class="nx-badge" [class]="statusClass(order()!.status)">
              <span class="nx-badge__dot"></span>{{ order()!.statusLabel }}
            </span>
            @if (order()!.contractNo) {
              <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
                Contrato: <span style="font-family:var(--nx-font-mono);">{{ order()!.contractNo }}</span>
              </span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm" disabled title="Próximamente">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><polyline points="6 9 6 2 18 2 18 9"/><path d="M6 18H4a2 2 0 0 1-2-2v-5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-2"/><rect x="6" y="14" width="12" height="8"/></svg>
            Imprimir
          </button>
          @if (order()!.status === 0) {
            <button class="nx-btn nx-btn--subtle nx-btn--sm" disabled title="Próximamente">
              Iniciar Servicio
            </button>
          }
          @if (order()!.status === 1) {
            <button class="nx-btn nx-btn--primary nx-btn--sm" disabled title="Próximamente">
              Finalizar
            </button>
          }
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">

        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Información general</div></div>
            <div class="nx-card__body">
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:0 var(--nx-space-6);">
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">No.</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.no }}</dd>
                  <dt class="nx-kv__k">Tipo</dt>
                  <dd class="nx-kv__v">{{ order()!.documentTypeLabel }}</dd>
                  <dt class="nx-kv__k">Cliente</dt>
                  <dd class="nx-kv__v">
                    <a [routerLink]="['/customers', order()!.customerNo]" class="nx-link">
                      {{ order()!.customerName }}
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                    </a>
                  </dd>
                  <dt class="nx-kv__k">Descripción</dt>
                  <dd class="nx-kv__v">{{ order()!.description || '—' }}</dd>
                  <dt class="nx-kv__k">Contrato</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.contractNo || '—' }}</dd>
                </dl>
                <dl class="nx-kv nx-kv--ruled">
                  <dt class="nx-kv__k">Fecha de la orden</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.orderDate || '—' }}</dd>
                  <dt class="nx-kv__k">Fecha de inicio</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.startingDate || '—' }}</dd>
                  <dt class="nx-kv__k">Fecha de finalización</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ order()!.finishingDate || '—' }}</dd>
                  <dt class="nx-kv__k">Condición de pago</dt>
                  <dd class="nx-kv__v">{{ order()!.paymentTermsCode || '—' }}</dd>
                  <dt class="nx-kv__k">Método de pago</dt>
                  <dd class="nx-kv__v">{{ order()!.paymentMethodCode || '—' }}</dd>
                </dl>
              </div>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Líneas del documento</div>
              <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ order()!.lines.length }} línea{{ order()!.lines.length !== 1 ? 's' : '' }}</span>
            </div>
            @if (order()!.lines.length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-8);">
                <p class="nx-empty__text">Sin líneas de detalle.</p>
              </div>
            } @else {
              <div style="overflow-x:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th style="width:70px;">No. línea</th>
                      <th style="width:90px;">Tipo</th>
                      <th style="width:120px;">No.</th>
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
                        <td>
                          <span class="nx-badge nx-badge--outline" style="font-size:10px;">{{ line.typeLabel }}</span>
                        </td>
                        <td class="nx-td--doc">{{ line.no || '—' }}</td>
                        <td>{{ line.description }}</td>
                        <td class="nx-td--num">{{ line.quantity | number:'1.0-4' }}</td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ line.unitOfMeasure }}</td>
                        <td class="nx-td--num">{{ line.unitPrice | number:'1.2-2' }}</td>
                        <td class="nx-td--num">
                          @if (line.lineDiscount > 0) {
                            <span style="color:var(--nx-text-muted);">{{ line.lineDiscount | number:'1.1-2' }}%</span>
                          } @else { <span style="color:var(--nx-text-faint);">—</span> }
                        </td>
                        <td class="nx-td--num">{{ line.amount | number:'1.2-2' }}</td>
                        <td class="nx-td--num" style="font-weight:var(--nx-weight-semibold);">{{ line.amountIncludingVat | number:'1.2-2' }}</td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
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

        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Orden de Servicio</div>
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
            <div class="nx-factbox__sectionlabel">Fechas</div>
            <dl class="nx-kv">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Orden</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ order()!.orderDate || '—' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Inicio</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ order()!.startingDate || '—' }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">Fin</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ order()!.finishingDate || '—' }}</dd>
            </dl>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Relacionado</div>
            <ul class="nx-linklist">
              <li>
                <a [routerLink]="['/customers', order()!.customerNo]" class="nx-link">
                  Ver ficha del cliente
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m7 17 10-10"/><path d="M7 7h10v10"/></svg>
                </a>
              </li>
              <li>
                <a routerLink="/services" class="nx-link">
                  Todas las órdenes de servicio
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
        <a routerLink="/services" class="nx-btn nx-btn--secondary nx-btn--sm" style="margin-top:var(--nx-space-3);">Volver a órdenes de servicio</a>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:2rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
  `]
})
export class ServiceOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(ServiceOrderService);

  order = signal<ServiceOrderDetail | null>(null);
  loading = signal(true);

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    try {
      const detail = await this.svc.getDetail(no);
      this.order.set(detail);
    } catch {
      this.order.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  statusClass(status: number): string {
    return ({ 0: 'nx-badge--outline', 1: 'nx-badge--info', 2: 'nx-badge--success', 3: 'nx-badge--warn' } as Record<number, string>)[status] ?? 'nx-badge--outline';
  }
}
