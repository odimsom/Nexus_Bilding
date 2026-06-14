import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../data/invoice.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';
import { CreateSalesOrderModalComponent } from '../../components/create-sales-order-modal/create-sales-order-modal.component';

@Component({
  selector: 'app-sales-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, CreateSalesOrderModalComponent],
  templateUrl: './sales-order-list.page.html',
  styleUrl: './sales-order-list.page.css'
})
export class SalesOrderListPage implements OnInit {
  readonly svc = inject(InvoiceService);
  readonly router = inject(Router);
  showNewOrderModal = signal(false);
  private readonly excel = inject(ExcelExportService);

  readonly tabs = [
    { id: 'all',   label: 'Todas' },
    { id: 'Order', label: 'Pedidos' },
    { id: 'Quote', label: 'Cotizaciones' },
  ];

  activeTab = 'all';
  searchText = '';
  filtered = signal<any[]>([]);

  async ngOnInit(): Promise<void> { await this.reload(); }

  async reload(): Promise<void> {
    await this.svc.loadOrders();
    this.applyFilter();
  }

  setTab(id: string): void { this.activeTab = id; this.applyFilter(); }

  applyFilter(): void {
    let list = this.svc.orders();
    if (this.activeTab !== 'all') list = list.filter(o => o.documentType === this.activeTab);
    if (this.searchText) {
      const q = this.searchText.toLowerCase();
      list = list.filter(o => o.no.toLowerCase().includes(q) || (o.sellToCustomerName || '').toLowerCase().includes(q));
    }
    this.filtered.set(list);
  }

  countForTab(id: string): number {
    const all = this.svc.orders();
    return id === 'all' ? all.length : all.filter(o => o.documentType === id).length;
  }

  docTypeLabel(t: string): string {
    return { Order: 'Pedido', Quote: 'Cotización', Invoice: 'Factura' }[t] ?? t;
  }

  statusClass(s: string): string {
    return { Open: 'nx-badge--info', Released: 'nx-badge--success', Closed: 'nx-badge--outline' }[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return { Open: 'Abierta', Released: 'Liberada', Closed: 'Cerrada' }[s] ?? s;
  }

  async exportCSV(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Ordenes_Venta',
      title: 'Órdenes de Venta',
      subtitle: 'Nexus Billing - Módulo de Ventas',
      sheetName: 'Órdenes',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Tipo', key: 'tipo', width: 14 },
        { header: 'Cliente', key: 'cliente', width: 35 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Estado', key: 'estado', width: 14 },
        { header: 'Moneda', key: 'moneda', width: 10 },
        { header: 'Subtotal', key: 'subtotal', width: 16, numFmt: '#,##0.00' },
        { header: 'Total c/IVA', key: 'total', width: 16, numFmt: '#,##0.00' },
      ],
      data: this.filtered().map(o => ({
        no: o.no,
        tipo: this.docTypeLabel(o.documentType),
        cliente: o.sellToCustomerName,
        fecha: o.postingDate,
        estado: this.statusLabel(o.status),
        moneda: o.currencyCode || 'DOP',
        subtotal: o.amount,
        total: o.amountIncludingVat,
      })),
    });
  }

  async onOrderCreated(): Promise<void> {
    this.showNewOrderModal.set(false);
    await this.reload();
  }
}
