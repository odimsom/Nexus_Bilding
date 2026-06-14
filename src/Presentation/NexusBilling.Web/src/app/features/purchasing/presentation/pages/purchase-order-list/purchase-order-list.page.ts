import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseOrderService } from '../../../data/purchase.service';
import { sortBy } from '../../../../../shared/utils/sort.utils';
import { CreatePurchaseOrderModalComponent } from '../../components/create-purchase-order-modal/create-purchase-order-modal.component';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-purchase-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, CreatePurchaseOrderModalComponent],
  templateUrl: './purchase-order-list.page.html',
  styleUrl: './purchase-order-list.page.css'
})
export class PurchaseOrderListPage implements OnInit {
  readonly svc = inject(PurchaseOrderService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly excel = inject(ExcelExportService);

  sortField = 'postingDate';
  sortAsc = false;
  searchText = '';
  filteredItems = signal<any[]>([]);
  showNewOrderModal = signal(false);
  filterVendorNo = '';

  readonly totalAmount = computed(() => this.filteredItems().reduce((s: number, o: any) => s + (o.amountIncludingVat ?? 0), 0));

  async ngOnInit() {
    this.filterVendorNo = this.route.snapshot.queryParamMap.get('vendorNo') ?? '';
    if (this.filterVendorNo) this.searchText = this.filterVendorNo;
    await this.svc.load();
    this.applyFilter();
  }

  applyFilter(): void {
    const q = this.searchText.toLowerCase().trim();
    const all = this.svc.items();
    if (!q) {
      this.filteredItems.set(all);
    } else {
      this.filteredItems.set(all.filter(o =>
        o.no.toLowerCase().includes(q) ||
        (o.vendorName ?? '').toLowerCase().includes(q) ||
        (o.vendorNo ?? '').toLowerCase().includes(q) ||
        (o.status ?? '').toLowerCase().includes(q)
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

  createNew() {
    this.showNewOrderModal.set(true);
  }

  async onOrderCreated(): Promise<void> {
    this.showNewOrderModal.set(false);
    await this.svc.load();
    this.applyFilter();
  }

  async exportExcel(): Promise<void> {
    const statusLabel = (s: string) => ({ Open: 'Abierta', Released: 'Liberada', Posted: 'Publicada' }[s] ?? s);
    await this.excel.exportAsExcel({
      filename: 'Ordenes_Compra',
      title: 'Órdenes de Compra',
      subtitle: 'Nexus Billing - Módulo de Compras',
      sheetName: 'Órdenes Compra',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Proveedor No.', key: 'vendorNo', width: 15 },
        { header: 'Nombre Proveedor', key: 'vendorName', width: 35 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Estado', key: 'estado', width: 14 },
        { header: 'Total c/IVA', key: 'total', width: 16, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
      ],
      data: this.sorted().map((o: any) => ({
        no: o.no,
        vendorNo: o.vendorNo ?? '',
        vendorName: o.vendorName ?? '',
        fecha: o.postingDate,
        estado: statusLabel(o.status ?? ''),
        total: o.amountIncludingVat ?? 0,
      })),
    });
  }
}
