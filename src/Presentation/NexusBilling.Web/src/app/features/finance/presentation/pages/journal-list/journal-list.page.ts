import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { JournalService } from '../../../data/journal.service';
import { GLAccountService } from '../../../data/gl-account.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-journal-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './journal-list.page.html',
  styleUrl: './journal-list.page.css'
})
export class JournalListPage implements OnInit {
  readonly svc = inject(JournalService);
  readonly glSvc = inject(GLAccountService);
  private readonly excel = inject(ExcelExportService);

  successMsg = '';
  
  newLine = {
    postingDate: new Date().toISOString().substring(0, 10),
    documentNo: '',
    accountNo: '',
    description: '',
    amount: 0,
    balAccountNo: ''
  };

  readonly balance = computed(() => {
    return this.svc.lines().reduce((acc, l) => acc + l.amount, 0);
  });

  sortField = 'postingDate';
  sortAsc = true;

  sortedLines() {
    return [...this.svc.lines()].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (this.sortField) {
        case 'postingDate': av = a.postingDate || ''; bv = b.postingDate || ''; break;
        case 'documentNo':  av = a.documentNo;  bv = b.documentNo;  break;
        case 'accountNo':   av = a.accountNo;   bv = b.accountNo;   break;
        case 'description': av = a.description; bv = b.description; break;
        case 'balAccountNo':av = a.balAccountNo || ''; bv = b.balAccountNo || ''; break;
        case 'amount':      av = a.amount;      bv = b.amount;      break;
        default:            av = a.postingDate || ''; bv = b.postingDate || '';
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

  ngOnInit(): void {
    this.reload();
    this.glSvc.load(); // Load GL Accounts for the dropdown
  }

  reload(): void {
    this.successMsg = '';
    this.svc.load();
  }

  async exportExcel(): Promise<void> {
    const data = this.svc.lines().map(l => ({
      date: new Intl.DateTimeFormat('es-DO').format(new Date(l.postingDate || '')),
      docNo: l.documentNo,
      accNo: l.accountNo,
      desc: l.description,
      balAcc: l.balAccountNo || '',
      amount: l.amount
    }));

    await this.excel.exportAsExcel({
      filename: 'DiarioGeneral_Nexus',
      title: 'Borrador de Diario General',
      subtitle: 'Nexus Billing - Módulo de Finanzas (Plantilla: GENERAL | Lote: DEFAULT)',
      sheetName: 'Diario General',
      columns: [
        { header: 'Fecha', key: 'date', width: 12 },
        { header: 'No. Doc.', key: 'docNo', width: 15 },
        { header: 'Cta. No.', key: 'accNo', width: 15 },
        { header: 'Descripción', key: 'desc', width: 40 },
        { header: 'Cta. Contrapartida', key: 'balAcc', width: 18 },
        { header: 'Monto', key: 'amount', width: 15, numFmt: '#,##0.00', alignment: { horizontal: 'right' } }
      ],
      data
    });
  }

  async addLine() {
    await this.svc.createLine({
      journalTemplateName: 'GENERAL',
      journalBatchName: 'DEFAULT',
      ...this.newLine
    });
    
    if (!this.svc.error()) {
      // Clear specific fields only
      this.newLine.description = '';
      this.newLine.amount = 0;
      this.newLine.balAccountNo = '';
    }
  }

  async deleteLine(id: string) {
    if (confirm('¿Eliminar esta línea del diario?')) {
      await this.svc.deleteLine(id);
    }
  }

  async postBatch() {
    if (this.balance() !== 0) {
      alert('El diario está descuadrado. Revise los montos.');
      return;
    }
    
    if (confirm('¿Desea registrar el diario? Las líneas se contabilizarán y desaparecerán de esta vista.')) {
      try {
        const qty = await this.svc.postBatch();
        this.successMsg = `Diario registrado correctamente. Se generaron ${qty} movimientos de contabilidad.`;
        setTimeout(() => { if ((window as any).lucide) (window as any).lucide.createIcons(); }, 0);
      } catch (e) {
        // error handled by service
      }
    }
  }
}
