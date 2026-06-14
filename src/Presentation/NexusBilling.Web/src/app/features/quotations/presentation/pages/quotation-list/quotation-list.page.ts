import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { QuotationService } from '../../../data/quotation.service';
import { QuotationListItem } from '../../../domain/quotation.model';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-quotation-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './quotation-list.page.html',
  styleUrl: './quotation-list.page.css'
})
export class QuotationListPage implements OnInit {
  readonly svc = inject(QuotationService);
  private readonly excel = inject(ExcelExportService);

  search = '';
  statusFilter = '';
  expiryFilter = '';
  filtered = signal<QuotationListItem[]>([]);

  async ngOnInit() {
    await this.svc.load({ pageSize: 200 });
    this.applyFilter();
  }

  applyFilter(): void {
    let list = this.svc.items();
    const q = this.search.toLowerCase().trim();
    if (q) list = list.filter(x => x.no.toLowerCase().includes(q) || x.sellToCustomerName.toLowerCase().includes(q) || x.sellToCustomerNo.toLowerCase().includes(q));
    if (this.statusFilter) list = list.filter(x => x.status === this.statusFilter);
    if (this.expiryFilter === 'active') list = list.filter(x => !x.isExpired);
    if (this.expiryFilter === 'expired') list = list.filter(x => x.isExpired);
    this.filtered.set(list);
  }

  statusBadge(q: QuotationListItem): string {
    if (q.status === 'Closed') return 'nx-badge--outline';
    if (q.isExpired) return 'nx-badge--warning';
    return 'nx-badge--info';
  }

  statusLabel(q: QuotationListItem): string {
    if (q.status === 'Closed') return 'Cerrada';
    if (q.isExpired) return 'Vencida';
    return 'Abierta';
  }

  async exportCSV(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Cotizaciones',
      title: 'Cotizaciones',
      subtitle: 'Nexus Billing - Módulo de Ventas',
      sheetName: 'Cotizaciones',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Cliente', key: 'cliente', width: 35 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Válida Hasta', key: 'validez', width: 14 },
        { header: 'Cotizó', key: 'cotizo', width: 25 },
        { header: 'Estado', key: 'estado', width: 14 },
        { header: 'Subtotal', key: 'subtotal', width: 16, numFmt: '#,##0.00' },
        { header: 'Total c/ITBIS', key: 'total', width: 16, numFmt: '#,##0.00' },
      ],
      data: this.filtered().map(q => ({
        no: q.no,
        cliente: q.sellToCustomerName,
        fecha: q.postingDate,
        validez: q.validUntilDate ?? '',
        cotizo: q.quotedBy ?? '',
        estado: this.statusLabel(q),
        subtotal: q.amount,
        total: q.amountIncludingVat,
      })),
    });
  }
}
