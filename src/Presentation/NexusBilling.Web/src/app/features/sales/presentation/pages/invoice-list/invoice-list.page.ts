import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../data/invoice.service';
import { Invoice, InvoiceFilter, InvoiceSortField, InvoiceStatus } from '../../../domain/invoice.model';

interface TabDef { id: InvoiceStatus | 'all'; label: string; }

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <!-- Breadcrumb -->
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Facturas</span>
    </nav>

    <!-- Page header -->
    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Facturas de Venta</h1>
        <p class="nx-page-subtitle">{{ filtered().length }} documentos</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>
          Exportar
        </button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nueva Factura
        </button>
      </div>
    </div>

    <!-- Tabs -->
    <div class="nx-tabs" style="margin-bottom:var(--nx-space-4);">
      @for (tab of tabs; track tab.id) {
        <button
          class="nx-tab"
          [attr.aria-selected]="activeTab === tab.id"
          (click)="setTab(tab.id)"
        >
          {{ tab.label }}
          <span class="nx-tab__count">{{ countForTab(tab.id) }}</span>
        </button>
      }
    </div>

    <!-- Filter bar -->
    <div class="filter-bar">
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:360px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input id="invoice-search" type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar No., cliente, referencia…" [(ngModel)]="searchText" (ngModelChange)="applyFilter()" />
      </div>

      <input id="invoice-date-from" type="date" class="nx-input" style="width:160px;" [(ngModel)]="dateFrom" (ngModelChange)="applyFilter()" />
      <input id="invoice-date-to" type="date" class="nx-input" style="width:160px;" [(ngModel)]="dateTo" (ngModelChange)="applyFilter()" />

      @if (searchText || dateFrom || dateTo) {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">Limpiar</button>
      }
    </div>

    <!-- KPI summary row -->
    <div style="display:grid;grid-template-columns:repeat(4,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-4);">
      <div class="nx-stat">
        <span class="nx-stat__label">Total Período</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">RD$ {{ totalAmount() | number:'1.0-0' }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Por Cobrar</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-positive);">RD$ {{ totalOpen() | number:'1.0-0' }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Vencido</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);color:var(--nx-money-negative);">RD$ {{ totalOverdue() | number:'1.0-0' }}</span>
      </div>
      <div class="nx-stat">
        <span class="nx-stat__label">Cobrado</span>
        <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">RD$ {{ totalPaid() | number:'1.0-0' }}</span>
      </div>
    </div>

    <!-- Table -->
    <div class="nx-card" style="overflow:hidden;">
      @if (filtered().length === 0) {
        <div class="nx-empty">
          <div class="nx-empty__icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
          </div>
          <p class="nx-empty__title">No hay facturas</p>
          <p class="nx-empty__text">Prueba con otros filtros o crea una nueva factura.</p>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Lista de facturas">
            <thead>
              <tr>
                <th class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                <th class="sortable" (click)="setSort('sellToCustomerName')">Cliente {{ si('sellToCustomerName') }}</th>
                <th class="sortable" (click)="setSort('postingDate')">Fecha Registro {{ si('postingDate') }}</th>
                <th class="sortable" (click)="setSort('dueDate')">Vencimiento {{ si('dueDate') }}</th>
                <th>Pago</th>
                <th>Moneda</th>
                <th>Estado</th>
                <th class="nx-th--num sortable" (click)="setSort('amountIncludingVat')">Importe {{ si('amountIncludingVat') }}</th>
                <th class="nx-th--num">Saldo</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              @for (inv of filtered(); track inv.no) {
                <tr>
                  <td class="nx-td--doc">
                    <a [routerLink]="['/invoices', inv.no]" class="nx-link">{{ inv.no }}</a>
                  </td>
                  <td style="font-weight:var(--nx-weight-medium);color:var(--nx-text-strong);">
                    <a [routerLink]="['/customers', inv.sellToCustomerNo]" class="nx-link" style="font-weight:var(--nx-weight-medium);">
                      {{ inv.sellToCustomerName }}
                    </a>
                  </td>
                  <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.postingDate }}</td>
                  <td style="font-size:var(--nx-text-sm);" [style.color]="inv.status === 'overdue' ? 'var(--nx-red-500)' : 'var(--nx-text-muted)'">
                    {{ inv.dueDate || '—' }}
                  </td>
                  <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.paymentMethodCode }}</td>
                  <td>
                    @if (inv.currencyCode) {
                      <span class="nx-badge nx-badge--outline">{{ inv.currencyCode }}</span>
                    } @else {
                      <span style="color:var(--nx-text-faint);font-size:var(--nx-text-sm);">DOP</span>
                    }
                  </td>
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
                  <td>
                    <a [routerLink]="['/invoices', inv.no]" class="nx-iconbtn nx-iconbtn--sm" aria-label="Abrir factura">
                      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M9 18l6-6-6-6"/></svg>
                    </a>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>
  `,
  styles: [`
    :host { display: block; }
    .filter-bar { display:flex; align-items:center; gap:var(--nx-space-3); flex-wrap:wrap; margin-bottom:var(--nx-space-4); }
    .sortable { cursor:pointer; user-select:none; white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
  `]
})
export class InvoiceListPage implements OnInit {
  private readonly svc = inject(InvoiceService);

  tabs: TabDef[] = [
    { id: 'all', label: 'Todas' },
    { id: 'open', label: 'Abiertas' },
    { id: 'overdue', label: 'Vencidas' },
    { id: 'posted', label: 'Publicadas' },
    { id: 'paid', label: 'Pagadas' },
  ];

  activeTab: InvoiceStatus | 'all' = 'all';
  searchText = '';
  dateFrom = '';
  dateTo = '';
  sortField: InvoiceSortField = 'postingDate';
  sortAsc = false;

  all = signal<Invoice[]>([]);
  filtered = signal<Invoice[]>([]);

  totalAmount = signal(0);
  totalOpen = signal(0);
  totalOverdue = signal(0);
  totalPaid = signal(0);

  ngOnInit(): void {
    this.all.set(this.svc.getAll());
    this.applyFilter();
  }

  setTab(id: InvoiceStatus | 'all'): void {
    this.activeTab = id;
    this.applyFilter();
  }

  setSort(f: InvoiceSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = false; }
    this.applyFilter();
  }

  si(f: InvoiceSortField): string {
    if (this.sortField !== f) return '';
    return this.sortAsc ? '↑' : '↓';
  }

  clearFilters(): void {
    this.searchText = '';
    this.dateFrom = '';
    this.dateTo = '';
    this.applyFilter();
  }

  applyFilter(): void {
    const f: InvoiceFilter = {
      search: this.searchText || undefined,
      status: this.activeTab === 'all' ? undefined : this.activeTab,
      dateFrom: this.dateFrom || undefined,
      dateTo: this.dateTo || undefined,
    };
    const result = this.svc.filter(f, this.sortField, this.sortAsc);
    this.filtered.set(result);

    const all = this.all();
    this.totalAmount.set(all.reduce((s, i) => s + i.amountIncludingVat, 0));
    this.totalOpen.set(all.filter(i => i.status === 'open').reduce((s, i) => s + i.remainingAmount, 0));
    this.totalOverdue.set(all.filter(i => i.status === 'overdue').reduce((s, i) => s + i.remainingAmount, 0));
    this.totalPaid.set(all.filter(i => i.status === 'paid').reduce((s, i) => s + i.amountIncludingVat, 0));
  }

  countForTab(id: InvoiceStatus | 'all'): number {
    const all = this.all();
    return id === 'all' ? all.length : all.filter(i => i.status === id).length;
  }

  statusClass(s: string): string {
    return { open: 'nx-badge--info', posted: 'nx-badge--success', overdue: 'nx-badge--danger', paid: 'nx-badge--outline' }[s] ?? '';
  }
  statusLabel(s: string): string {
    return { open: 'Abierta', posted: 'Publicada', overdue: 'Vencida', paid: 'Pagada' }[s] ?? s;
  }
}
