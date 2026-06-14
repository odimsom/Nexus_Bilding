import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { InvoiceService } from '../../../data/invoice.service';
import { Invoice } from '../../../domain/invoice.model';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './invoice-list.page.html',
  styleUrl: './invoice-list.page.css'
})
export class InvoiceListPage implements OnInit {
  readonly svc = inject(InvoiceService);
  private readonly excel = inject(ExcelExportService);
  private readonly route = inject(ActivatedRoute);

  searchText = '';
  filterCustomerNo = '';
  showOverdueOnly = false;
  sortField = 'postingDate';
  sortAsc = false;
  filtered = signal<Invoice[]>([]);

  totalAmount = computed(() => this.filtered().reduce((s, i) => s + i.amountIncludingVat, 0));
  totalItbis = computed(() => this.filtered().reduce((s, i) => s + (i.amountIncludingVat - i.amount), 0));
  overdueCount = computed(() => this.filtered().filter(i => this.isOverdue(i)).length);

  async ngOnInit(): Promise<void> {
    this.filterCustomerNo = this.route.snapshot.queryParamMap.get('customerNo') ?? '';
    if (this.filterCustomerNo) this.searchText = this.filterCustomerNo;
    await this.svc.loadInvoices({ pageSize: 500 });
    this.applyFilter();
  }

  setSort(f: string): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = false; }
    this.applyFilter();
  }

  si(f: string): string { return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : ''; }

  async exportExcel(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Facturas_Venta',
      title: 'Facturas de Venta',
      subtitle: 'Nexus Billing - Módulo de Ventas',
      sheetName: 'Facturas',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Cliente', key: 'cliente', width: 35 },
        { header: 'Ref. Externa', key: 'ref', width: 18 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Subtotal', key: 'subtotal', width: 16, numFmt: '#,##0.00' },
        { header: 'Total c/IVA', key: 'total', width: 16, numFmt: '#,##0.00' },
      ],
      data: this.filtered().map(i => ({
        no: i.no,
        cliente: i.sellToCustomerName,
        ref: i.externalDocumentNo ?? '',
        fecha: i.postingDate,
        subtotal: i.amount,
        total: i.amountIncludingVat,
      })),
    });
  }

  applyFilter(): void {
    let list = [...this.svc.invoices()];
    if (this.searchText) {
      const q = this.searchText.toLowerCase();
      list = list.filter(i =>
        i.no.toLowerCase().includes(q) ||
        (i.sellToCustomerNo || '').toLowerCase().includes(q) ||
        (i.sellToCustomerName || '').toLowerCase().includes(q) ||
        (i.externalDocumentNo || '').toLowerCase().includes(q)
      );
    }
    if (this.showOverdueOnly) {
      list = list.filter(i => this.isOverdue(i));
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

  isOverdue(inv: { dueDate?: string | null }): boolean {
    if (!inv.dueDate) return false;
    return new Date(inv.dueDate) < new Date(new Date().toDateString());
  }
}
