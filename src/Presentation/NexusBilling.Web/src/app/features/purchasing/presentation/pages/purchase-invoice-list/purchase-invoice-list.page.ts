import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';

@Component({
  selector: 'app-purchase-invoice-list',
  standalone: true,
  imports: [RouterLink, DatePipe, CurrencyPipe],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Facturas de Compra</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-5);">
      <div>
        <h1 class="nx-page-title">Facturas de Compra</h1>
        <p class="nx-page-subtitle">Historial de facturas recibidas de proveedores</p>
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
            <input class="nx-input" placeholder="Buscar facturas…" />
          </div>
          <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
            {{ svc.totalItems() }} facturas en total
          </div>
        </div>

        @if (svc.items().length === 0) {
          <div class="nx-empty" style="padding:var(--nx-space-8);">
            <div class="nx-empty__icon">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/><line x1="16" y1="13" x2="8" y2="13"/><line x1="16" y1="17" x2="8" y2="17"/><polyline points="10 9 9 9 8 9"/></svg>
            </div>
            <p class="nx-empty__title">No hay facturas registradas</p>
            <p class="nx-empty__text">Las facturas de compra contabilizadas aparecerán aquí.</p>
          </div>
        } @else {
          <div style="overflow-x:auto;">
            <table class="nx-table">
              <thead>
                <tr>
                  <th style="width:140px;">No. Factura</th>
                  <th style="width:120px;">Proveedor</th>
                  <th>Nombre Proveedor</th>
                  <th>Fecha</th>
                  <th>Estado</th>
                  <th style="text-align:right;">Importe Total</th>
                </tr>
              </thead>
              <tbody>
                @for (inv of svc.items(); track inv.no) {
                  <tr style="cursor:pointer;" [routerLink]="['/purchase-invoices', inv.no]">
                    <td class="nx-td--doc"><a class="nx-link">{{ inv.no }}</a></td>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.buyFromVendorNo }}</td>
                    <td style="font-weight:var(--nx-weight-medium);">{{ inv.payToName }}</td>
                    <td>{{ inv.postingDate | date:'dd/MM/yyyy' }}</td>
                    <td><span class="nx-badge nx-badge--info">Registrada</span></td>
                    <td class="nx-td--num">{{ inv.amountIncludingVat | currency:'DOP':'symbol':'1.2-2' }}</td>
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
export class PurchaseInvoiceListPage implements OnInit {
  readonly svc = inject(PurchaseInvoiceService);

  async ngOnInit() {
    await this.svc.load();
  }
}
