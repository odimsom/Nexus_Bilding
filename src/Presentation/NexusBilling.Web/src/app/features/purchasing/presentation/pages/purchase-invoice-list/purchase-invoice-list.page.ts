import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { RouterLink, ActivatedRoute, Router } from '@angular/router';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';
import { sortBy } from '../../../../../shared/utils/sort.utils';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-purchase-invoice-list',
  standalone: true,
  imports: [RouterLink, DatePipe, CurrencyPipe, FormsModule],
  templateUrl: './purchase-invoice-list.page.html',
  styleUrl: './purchase-invoice-list.page.css'
})
export class PurchaseInvoiceListPage implements OnInit {
  readonly svc = inject(PurchaseInvoiceService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly excel = inject(ExcelExportService);

  sortField = 'postingDate';
  sortAsc = false;
  searchText = '';
  filteredItems = signal<any[]>([]);
  filterVendorNo = '';

  readonly totalAmount = computed(() => this.filteredItems().reduce((s: number, i: any) => s + (i.amountIncludingVat ?? 0), 0));

  async ngOnInit() {
    this.filterVendorNo = this.route.snapshot.queryParamMap.get('vendorNo') ?? '';
    if (this.filterVendorNo) this.searchText = this.filterVendorNo;
    await this.svc.load({ pageSize: 500 });
    this.applyFilter();
  }

  applyFilter(): void {
    const q = this.searchText.toLowerCase().trim();
    const all = this.svc.items();
    if (!q) {
      this.filteredItems.set(all);
    } else {
      this.filteredItems.set(all.filter(inv =>
        (inv.no ?? '').toLowerCase().includes(q) ||
        (inv.buyFromVendorNo ?? '').toLowerCase().includes(q) ||
        (inv.payToName ?? '').toLowerCase().includes(q)
      ));
    }
  }

  sorted(): any[] {
    return sortBy(this.filteredItems(), this.sortField, this.sortAsc);
  }

  setSort(f: string): void {
    if (this.sortField === f) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortField = f;
      this.sortAsc = true;
    }
  }

  si(f: string): string {
    if (this.sortField !== f) return '';
    return this.sortAsc ? '↑' : '↓';
  }

  async exportExcel(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Facturas_Compra',
      title: 'Facturas de Compra',
      subtitle: 'Nexus Billing - Módulo de Compras',
      sheetName: 'Facturas Compra',
      columns: [
        { header: 'No. Factura', key: 'no', width: 15 },
        { header: 'Proveedor No.', key: 'vendorNo', width: 15 },
        { header: 'Nombre Proveedor', key: 'vendorName', width: 35 },
        { header: 'Ref. Externa', key: 'ref', width: 18 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Subtotal', key: 'subtotal', width: 16, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
        { header: 'Total c/IVA', key: 'total', width: 16, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
      ],
      data: this.sorted().map((inv: any) => ({
        no: inv.no,
        vendorNo: inv.buyFromVendorNo ?? '',
        vendorName: inv.payToName ?? '',
        ref: inv.externalDocumentNo ?? '',
        fecha: inv.postingDate,
        subtotal: inv.amount ?? 0,
        total: inv.amountIncludingVat ?? 0,
      })),
    });
  }
}
