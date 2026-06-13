import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../data/invoice.service';
import { Invoice } from '../../../domain/invoice.model';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Facturas de Venta</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Facturas de Venta</h1>
        <p class="nx-page-subtitle">Facturas publicadas de clientes</p>
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (svc.error()) {
      <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">{{ svc.error() }}</div>
    } @else {

      <!-- KPI strip -->
      <div style="display:grid;grid-template-columns:repeat(3,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-5);">
        <div class="nx-stat">
          <span class="nx-stat__label">Total período</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">RD$ {{ totalAmount() | number:'1.0-0' }}</span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Facturas</span>
          <span class="nx-stat__value" style="font-size:var(--nx-text-xl);">{{ svc.invoices().length }}</span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">ITBIS total</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">RD$ {{ totalItbis() | number:'1.0-0' }}</span>
        </div>
      </div>

      <div class="nx-card">
        <div class="nx-card__head" style="gap:var(--nx-space-4);flex-wrap:wrap;">
          <div class="nx-input-icon" style="max-width:320px;flex:1;">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
            <input class="nx-input" placeholder="Buscar No., cliente, referencia…" [(ngModel)]="searchText" (ngModelChange)="applyFilter()" />
          </div>
          <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
            {{ filtered().length }} documentos
          </div>
        </div>

        @if (filtered().length === 0) {
          <div class="nx-empty" style="padding:var(--nx-space-8);">
            <div class="nx-empty__icon">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
            </div>
            <p class="nx-empty__title">No hay facturas</p>
            <p class="nx-empty__text">Las facturas se crean publicando una orden de venta.</p>
          </div>
        } @else {
          <div style="overflow-x:auto;">
            <table class="nx-table" aria-label="Facturas de venta">
              <thead>
                <tr>
                  <th style="width:140px;" class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                  <th class="sortable" (click)="setSort('sellToCustomerName')">Cliente {{ si('sellToCustomerName') }}</th>
                  <th class="sortable" (click)="setSort('postingDate')">Fecha {{ si('postingDate') }}</th>
                  <th>Vencimiento</th>
                  <th>Vendedor</th>
                  <th>Moneda</th>
                  <th class="nx-th--num sortable" (click)="setSort('amountIncludingVat')">Total c/IVA {{ si('amountIncludingVat') }}</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                @for (inv of filtered(); track inv.no) {
                  <tr style="cursor:pointer;" [routerLink]="['/invoices', inv.no]">
                    <td class="nx-td--doc"><a class="nx-link">{{ inv.no }}</a></td>
                    <td style="font-weight:var(--nx-weight-medium);">{{ inv.sellToCustomerName || inv.billToName }}</td>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">
                      {{ inv.postingDate | date:'dd/MM/yyyy' }}
                    </td>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">
                      {{ inv.dueDate ? (inv.dueDate | date:'dd/MM/yyyy') : '—' }}
                    </td>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inv.salespersonCode || '—' }}</td>
                    <td>
                      @if (inv.currencyCode) {
                        <span class="nx-badge nx-badge--outline">{{ inv.currencyCode }}</span>
                      } @else {
                        <span style="color:var(--nx-text-faint);font-size:var(--nx-text-sm);">DOP</span>
                      }
                    </td>
                    <td class="nx-td--num" style="font-weight:var(--nx-weight-semibold);">
                      {{ inv.amountIncludingVat | number:'1.2-2' }}
                    </td>
                    <td>
                      <a [routerLink]="['/invoices', inv.no]" class="nx-iconbtn nx-iconbtn--sm" (click)="$event.stopPropagation()">
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
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
  `]
})
export class InvoiceListPage implements OnInit {
  readonly svc = inject(InvoiceService);

  searchText = '';
  sortField = 'postingDate';
  sortAsc = false;
  filtered = signal<Invoice[]>([]);

  totalAmount = computed(() => this.svc.invoices().reduce((s, i) => s + i.amountIncludingVat, 0));
  totalItbis = computed(() => this.svc.invoices().reduce((s, i) => s + (i.amountIncludingVat - i.amount), 0));

  async ngOnInit(): Promise<void> {
    await this.svc.loadInvoices();
    this.applyFilter();
  }

  setSort(f: string): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = false; }
    this.applyFilter();
  }

  si(f: string): string { return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : ''; }

  applyFilter(): void {
    let list = [...this.svc.invoices()];
    if (this.searchText) {
      const q = this.searchText.toLowerCase();
      list = list.filter(i =>
        i.no.toLowerCase().includes(q) ||
        (i.sellToCustomerName || '').toLowerCase().includes(q) ||
        (i.externalDocumentNo || '').toLowerCase().includes(q)
      );
    }
    list.sort((a, b) => {
      let av: string | number = '', bv: string | number = '';
      switch (this.sortField) {
        case 'no': av = a.no; bv = b.no; break;
        case 'postingDate': av = a.postingDate; bv = b.postingDate; break;
        case 'sellToCustomerName': av = a.sellToCustomerName; bv = b.sellToCustomerName; break;
        case 'amountIncludingVat': av = a.amountIncludingVat; bv = b.amountIncludingVat; break;
        default: av = a.postingDate; bv = b.postingDate;
      }
      if (typeof av === 'string') return this.sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return this.sortAsc ? av - (bv as number) : (bv as number) - av;
    });
    this.filtered.set(list);
  }
}
