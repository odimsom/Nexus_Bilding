import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { PurchaseOrderService } from '../../../data/purchase.service';

@Component({
  selector: 'app-purchase-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Pedidos de Compra</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-5);">
      <div>
        <h1 class="nx-page-title">Pedidos de Compra</h1>
        <p class="nx-page-subtitle">Gestiona tus órdenes de abastecimiento y entradas de almacén</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--primary" (click)="createNew()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Pedido
        </button>
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (svc.error()) {
      <div class="nx-callout nx-callout--danger">{{ svc.error() }}</div>
    } @else {
      <div class="nx-card">
        <div class="nx-card__head" style="gap:var(--nx-space-4);flex-wrap:wrap;">
          <div class="nx-input-icon" style="max-width:320px;flex:1;">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
            <input class="nx-input" placeholder="Buscar pedidos..." />
          </div>
          <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
            {{ svc.totalItems() }} pedidos en total
          </div>
        </div>

        @if (svc.items().length === 0) {
          <div class="nx-empty" style="padding:var(--nx-space-8);">
            <div class="nx-empty__icon">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z"/><polyline points="7.5 4.21 12 6.81 16.5 4.21"/><polyline points="7.5 19.79 12 17.19 16.5 19.79"/><polyline points="3.27 6.96 12 12.01 20.73 6.96"/><line x1="12" y1="22.08" x2="12" y2="12"/></svg>
            </div>
            <p class="nx-empty__title">No hay pedidos registrados</p>
            <p class="nx-empty__text">Comienza creando una orden de compra hacia tus suplidores.</p>
            <button class="nx-btn nx-btn--primary nx-btn--sm" style="margin-top:var(--nx-space-4);" (click)="createNew()">Crear Pedido</button>
          </div>
        } @else {
          <div style="overflow-x:auto;">
            <table class="nx-table">
              <thead>
                <tr>
                  <th style="width:120px;">No.</th>
                  <th>Proveedor</th>
                  <th>Fecha</th>
                  <th style="text-align:right;">Monto Total</th>
                  <th>Estado</th>
                </tr>
              </thead>
              <tbody>
                @for (o of svc.items(); track o.no) {
                  <tr style="cursor:pointer;" [routerLink]="['/purchases', o.no]">
                    <td class="nx-td--doc"><a class="nx-link">{{ o.no }}</a></td>
                    <td style="font-weight:var(--nx-weight-medium);">
                      <div>{{ o.vendorName }}</div>
                      <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ o.vendorNo }}</div>
                    </td>
                    <td>{{ o.postingDate | date:'shortDate' }}</td>
                    <td style="text-align:right;font-variant-numeric:tabular-nums;">
                      {{ o.amountIncludingVat | currency:o.currencyCode || 'USD' }}
                    </td>
                    <td>
                      @if (o.status === 'Open') {
                        <span class="nx-badge nx-badge--warn">Abierta</span>
                      } @else if (o.status === 'Released') {
                        <span class="nx-badge nx-badge--success">Lanzada</span>
                      } @else {
                        <span class="nx-badge nx-badge--info">{{ o.status }}</span>
                      }
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        }
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
  `]
})
export class PurchaseOrderListPage implements OnInit {
  readonly svc = inject(PurchaseOrderService);
  private readonly router = inject(Router);

  async ngOnInit() {
    await this.svc.load();
  }

  createNew() {
    this.router.navigate(['/purchases', 'new']);
  }
}
