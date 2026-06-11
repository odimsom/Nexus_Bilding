import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../data/invoice.service';
import { SalesOrder } from '../../../domain/invoice.model';

interface TabDef { id: 'orders' | 'quotes'; label: string; }

@Component({
  selector: 'app-sales-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <!-- Breadcrumb -->
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Órdenes de Venta</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Órdenes de Venta</h1>
        <p class="nx-page-subtitle">{{ orders().length }} documentos activos</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">Liberar</button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nueva Orden
        </button>
      </div>
    </div>

    <!-- Tabs -->
    <div class="nx-tabs" style="margin-bottom:var(--nx-space-4);">
      <button class="nx-tab" [attr.aria-selected]="activeTab === 'orders'" (click)="activeTab = 'orders'">
        Órdenes <span class="nx-tab__count">{{ orders().filter(o => o.documentType === 'Order').length }}</span>
      </button>
      <button class="nx-tab" [attr.aria-selected]="activeTab === 'quotes'" (click)="activeTab = 'quotes'">
        Cotizaciones <span class="nx-tab__count">{{ orders().filter(o => o.documentType === 'Quote').length }}</span>
      </button>
    </div>

    <!-- Filter -->
    <div style="display:flex;gap:var(--nx-space-3);margin-bottom:var(--nx-space-4);flex-wrap:wrap;">
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:360px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input id="order-search" type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar No., cliente…" [(ngModel)]="searchText" />
      </div>
    </div>

    <!-- Table -->
    <div class="nx-card" style="overflow:hidden;">
      <div style="overflow-x:auto;">
        <table class="nx-table" aria-label="Lista de órdenes de venta">
          <thead>
            <tr>
              <th>No.</th>
              <th>Tipo</th>
              <th>Cliente</th>
              <th>Fecha</th>
              <th>Vencimiento</th>
              <th>Estado</th>
              <th>Moneda</th>
              <th class="nx-th--num">Importe c/IVA</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            @for (order of filteredOrders(); track order.no) {
              <tr>
                <td class="nx-td--doc">
                  <span class="nx-link">{{ order.no }}</span>
                </td>
                <td>
                  <span class="nx-badge nx-badge--outline">{{ order.documentType }}</span>
                </td>
                <td style="font-weight:var(--nx-weight-medium);">
                  <a [routerLink]="['/customers', order.sellToCustomerNo]" class="nx-link">
                    {{ order.sellToCustomerName }}
                  </a>
                </td>
                <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ order.postingDate }}</td>
                <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ order.dueDate || '—' }}</td>
                <td>
                  <span class="nx-badge" [class]="orderStatusClass(order.status)">
                    <span class="nx-badge__dot"></span>{{ orderStatusLabel(order.status) }}
                  </span>
                </td>
                <td>
                  @if (order.currencyCode) {
                    <span class="nx-badge nx-badge--outline">{{ order.currencyCode }}</span>
                  } @else {
                    <span style="color:var(--nx-text-faint);font-size:var(--nx-text-sm);">DOP</span>
                  }
                </td>
                <td class="nx-td--num nx-num">{{ order.amountIncludingVat | number:'1.2-2' }}</td>
                <td>
                  <button class="nx-iconbtn nx-iconbtn--sm" aria-label="Abrir orden">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 18l6-6-6-6"/></svg>
                  </button>
                </td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    </div>
  `,
  styles: [':host { display: block; }']
})
export class SalesOrderListPage implements OnInit {
  private readonly svc = inject(InvoiceService);

  activeTab: 'orders' | 'quotes' = 'orders';
  searchText = '';
  orders = signal<SalesOrder[]>([]);

  filteredOrders = signal<SalesOrder[]>([]);

  ngOnInit(): void {
    this.orders.set(this.svc.getSalesOrders());
    this.filteredOrders.set(this.orders());
  }

  orderStatusClass(s: string): string {
    return { open: 'nx-badge--info', released: 'nx-badge--success', pending_approval: 'nx-badge--warn', pending_prepayment: 'nx-badge--warn' }[s] ?? '';
  }
  orderStatusLabel(s: string): string {
    return { open: 'Abierta', released: 'Liberada', pending_approval: 'Aprobación Pend.', pending_prepayment: 'Prepago Pend.' }[s] ?? s;
  }
}
