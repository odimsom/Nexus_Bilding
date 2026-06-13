import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseOrderService } from '../../../data/purchase.service';
import { PurchaseOrder, CreatePurchaseOrderDto } from '../../../domain/purchase.model';

@Component({
  selector: 'app-purchase-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando pedido...</p>
      </div>
    } @else if (isNew()) {
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/purchases">Pedidos de Compra</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">Nuevo Pedido</span>
      </nav>

      <div class="nx-page-header">
        <div>
          <h1 class="nx-page-title">Nuevo Pedido de Compra</h1>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--ghost" routerLink="/purchases">Cancelar</button>
          <button class="nx-btn nx-btn--primary" [disabled]="saving() || !newForm.buyFromVendorNo" (click)="saveNew()">
            @if (saving()) { Guardando... } @else { Guardar Pedido }
          </button>
        </div>
      </div>

      <div class="nx-card" style="padding:var(--nx-space-6);">
        @if (errorMsg()) {
          <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">{{ errorMsg() }}</div>
        }
        <div class="form-grid">
          <div class="nx-field">
            <label class="nx-label">No. Proveedor *</label>
            <input class="nx-input" [(ngModel)]="newForm.buyFromVendorNo" placeholder="Ej. VEND-0001" autofocus />
          </div>
          <div class="nx-field">
            <label class="nx-label">Nombre del Proveedor</label>
            <input class="nx-input" [(ngModel)]="newForm.payToName" placeholder="Opcional" />
          </div>
          <div class="nx-field">
            <label class="nx-label">Fecha de Contabilización</label>
            <input class="nx-input" type="date" [(ngModel)]="newForm.postingDate" />
          </div>
          <div class="nx-field">
            <label class="nx-label">No. Documento Externo</label>
            <input class="nx-input" [(ngModel)]="newForm.externalDocumentNo" placeholder="Factura/Recibo externo" />
          </div>
        </div>
        <div class="nx-callout nx-callout--info" style="margin-top:var(--nx-space-4);">
          Las líneas del pedido se agregarán después de crear el encabezado del documento.
        </div>
      </div>
    } @else if (order()) {
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/purchases">Pedidos de Compra</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ order()!.no }}</span>
      </nav>

      <div class="nx-page-header">
        <div style="flex:1;min-width:0;">
          <h1 class="nx-page-title">Pedido: {{ order()!.no }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);flex-wrap:wrap;">
            <span class="nx-eyebrow">{{ order()!.vendorName || order()!.vendorNo }}</span>
            @if (order()!.status === 'Open') {
              <span class="nx-badge nx-badge--warning"><span class="nx-badge__dot"></span>Abierta</span>
            } @else if (order()!.status === 'Released') {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Lanzada</span>
            } @else {
              <span class="nx-badge nx-badge--info"><span class="nx-badge__dot"></span>{{ order()!.status }}</span>
            }
            <span style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">
              Fecha: {{ order()!.postingDate | date:'shortDate' }}
            </span>
          </div>
        </div>
        <div class="nx-page-actions">
          @if (order()!.status === 'Open') {
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="postOrder()">
              @if (saving()) { Procesando... } @else { Postear Pedido }
            </button>
          }
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 320px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Líneas del Pedido</div></div>
            @if (order()!.lines && order()!.lines.length > 0) {
              <div style="overflow-x:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th>Tipo</th>
                      <th>No.</th>
                      <th>Descripción</th>
                      <th>Cant.</th>
                      <th>UM</th>
                      <th style="text-align:right;">Precio</th>
                      <th style="text-align:right;">Total</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (line of order()!.lines; track line.lineNo) {
                      <tr>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-xs);">Item</td>
                        <td class="nx-td--mono">{{ line.itemNo }}</td>
                        <td>{{ line.description }}</td>
                        <td>{{ line.quantity }}</td>
                        <td>{{ line.unitOfMeasure }}</td>
                        <td style="text-align:right;">{{ line.unitPrice | number:'1.2-2' }}</td>
                        <td style="text-align:right;font-weight:var(--nx-weight-medium);">{{ line.amountIncludingVat | number:'1.2-2' }}</td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
            } @else {
              <div class="nx-empty" style="padding:var(--nx-space-5);">
                <p class="nx-empty__text">No hay líneas registradas en este pedido.</p>
              </div>
            }
          </div>
        </div>

        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Resumen</div>
            <div class="nx-factbox__title">Totales</div>
          </div>
          <div class="nx-factbox__section">
            <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-2);">
              <span style="color:var(--nx-text-muted);">Subtotal</span>
              <span class="nx-td--mono">{{ order()!.amount | currency:order()!.currencyCode || 'USD' }}</span>
            </div>
            <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-3);">
              <span style="color:var(--nx-text-muted);">ITBIS (18%)</span>
              <span class="nx-td--mono">{{ order()!.amountIncludingVat - order()!.amount | currency:order()!.currencyCode || 'USD' }}</span>
            </div>
            <div style="display:flex;justify-content:space-between;padding-top:var(--nx-space-3);border-top:1px solid var(--nx-border);font-weight:var(--nx-weight-bold);font-size:var(--nx-text-lg);">
              <span>Total</span>
              <span>{{ order()!.amountIncludingVat | currency:order()!.currencyCode || 'USD' }}</span>
            </div>
          </div>
        </div>
      </div>
    } @else {
      <div class="nx-empty">
        <p class="nx-empty__title">Pedido no encontrado</p>
        <a routerLink="/purchases" class="nx-btn nx-btn--secondary nx-btn--sm">Volver</a>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
})
export class PurchaseOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(PurchaseOrderService);

  isNew = signal(false);
  loading = signal(true);
  saving = signal(false);
  errorMsg = signal<string | null>(null);
  
  order = signal<PurchaseOrder | null>(null);
  
  newForm: CreatePurchaseOrderDto = {
    buyFromVendorNo: '',
    payToName: '',
    postingDate: new Date().toISOString().split('T')[0],
    externalDocumentNo: ''
  };

  async ngOnInit() {
    const no = this.route.snapshot.paramMap.get('no');
    if (no === 'new') {
      this.isNew.set(true);
      this.loading.set(false);
    } else if (no) {
      this.loading.set(true);
      const res = await this.svc.getByNo(no);
      this.order.set(res);
      this.loading.set(false);
    }
  }

  async saveNew() {
    if (!this.newForm.buyFromVendorNo) {
      this.errorMsg.set('El número de proveedor es requerido.');
      return;
    }
    this.saving.set(true);
    this.errorMsg.set(null);
    try {
      // Formatear fecha a ISO string si es necesario, pero el backend lo acepta YYYY-MM-DD si se manda asi.
      const no = await this.svc.create({
        ...this.newForm,
        lines: [
          {
            lineType: 'Item',
            itemNo: 'ITEM-TEST',
            description: 'Item de prueba de creación',
            unitOfMeasure: 'UN',
            quantity: 1,
            unitPrice: 100,
            lineDiscountPct: 0
          }
        ]
      });
      this.router.navigate(['/purchases', no], { replaceUrl: true });
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Ocurrió un error al crear el pedido.');
    } finally {
      this.saving.set(false);
    }
  }

  async postOrder() {
    if (!this.order()) return;
    this.saving.set(true);
    try {
      await this.svc.post(this.order()!.no);
      const res = await this.svc.getByNo(this.order()!.no);
      this.order.set(res);
      alert('¡Pedido contabilizado con éxito!');
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Ocurrió un error al contabilizar el pedido.');
    } finally {
      this.saving.set(false);
    }
  }
}
