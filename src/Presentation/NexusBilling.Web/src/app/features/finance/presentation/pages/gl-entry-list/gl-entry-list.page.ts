import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GLEntryService } from '../../../data/gl-entry.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-gl-entry-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './gl-entry-list.page.html',
  styleUrl: './gl-entry-list.page.css'
})
export class GLEntryListPage implements OnInit {
  readonly svc = inject(GLEntryService);
  private readonly excel = inject(ExcelExportService);

  sortField = 'entryNo';
  sortAsc = false;

  sortedEntries() {
    return [...this.svc.entries()].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (this.sortField) {
        case 'postingDate':  av = a.postingDate;   bv = b.postingDate;   break;
        case 'documentType': av = a.documentType || ''; bv = b.documentType || ''; break;
        case 'documentNo':   av = a.documentNo;    bv = b.documentNo;    break;
        case 'glAccountNo':  av = a.glAccountNo;   bv = b.glAccountNo;   break;
        case 'glAccountName':av = a.glAccountName; bv = b.glAccountName; break;
        case 'description':  av = a.description;   bv = b.description;   break;
        case 'amount':       av = a.amount;        bv = b.amount;        break;
        case 'entryNo':      av = a.entryNo;       bv = b.entryNo;       break;
        default:             av = a.entryNo;       bv = b.entryNo;
      }
      if (typeof av === 'string') return this.sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return this.sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }

  setSort(f: string): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: string): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  ngOnInit(): void { this.reload(); }

  reload(): void {
    this.svc.load();
  }

  async exportExcel(): Promise<void> {
    const data = this.svc.entries().map(e => ({
      entryNo: e.entryNo,
      date: new Intl.DateTimeFormat('es-DO').format(new Date(e.postingDate || '')),
      docType: e.documentType || '',
      docNo: e.documentNo,
      accNo: e.glAccountNo,
      accName: e.glAccountName,
      desc: e.description,
      amount: e.amount
    }));

    await this.excel.exportAsExcel({
      filename: 'MovimientosContabilidad_Nexus',
      title: 'Movimientos de Contabilidad',
      subtitle: 'Nexus Billing - Mayor General',
      sheetName: 'Movimientos',
      columns: [
        { header: 'No. Mov.', key: 'entryNo', width: 10 },
        { header: 'Fecha', key: 'date', width: 12 },
        { header: 'Tipo Doc.', key: 'docType', width: 12 },
        { header: 'No. Documento', key: 'docNo', width: 15 },
        { header: 'Cta. No.', key: 'accNo', width: 15 },
        { header: 'Nombre Cta.', key: 'accName', width: 35 },
        { header: 'Descripción', key: 'desc', width: 40 },
        { header: 'Monto', key: 'amount', width: 15, numFmt: '#,##0.00', alignment: { horizontal: 'right' } }
      ],
      data
    });
  }

  goToPage(page: number): void {
    this.svc.load({ page });
  }
}
