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
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <a routerLink="/gl">Mayor General</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Movimientos de Contabilidad</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Movimientos de Contabilidad (GL Entries)</h1>
        <p class="nx-page-subtitle">
          @if (svc.loading()) { Cargando… }
          @else { Mostrando página {{ svc.currentPage() }} de {{ svc.totalPages() }} }
        </p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="exportExcel()" [disabled]="svc.entries().length === 0">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg> Excel
        </button>
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="reload()">Actualizar</button>
      </div>
    </div>

    @if (svc.error()) {
      <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
        {{ svc.error() }}
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="reload()" style="margin-left:auto;">Reintentar</button>
      </div>
    }

    <div class="nx-card" style="overflow:hidden;">
      @if (svc.loading()) {
        <div class="nx-empty">
          <div class="nx-spinner"></div>
          <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando movimientos…</p>
        </div>
      } @else if (svc.entries().length === 0) {
        <div class="nx-empty">
          <p class="nx-empty__title">No hay movimientos registrados</p>
          <p class="nx-empty__text">Los asientos contabilizados aparecerán aquí.</p>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Movimientos de Contabilidad">
            <thead>
              <tr>
                <th class="sortable" style="width:100px;" (click)="setSort('postingDate')">Fecha Registro {{ si('postingDate') }}</th>
                <th class="sortable" (click)="setSort('documentType')">Tipo Doc. {{ si('documentType') }}</th>
                <th class="sortable" (click)="setSort('documentNo')">No. Documento {{ si('documentNo') }}</th>
                <th class="sortable" (click)="setSort('glAccountNo')">Cta. No. {{ si('glAccountNo') }}</th>
                <th class="sortable" (click)="setSort('glAccountName')">Nombre Cta. {{ si('glAccountName') }}</th>
                <th class="sortable" (click)="setSort('description')">Descripción {{ si('description') }}</th>
                <th class="num sortable" (click)="setSort('amount')">Monto {{ si('amount') }}</th>
                <th class="num sortable" style="width:80px;" (click)="setSort('entryNo')">No. Mov. {{ si('entryNo') }}</th>
              </tr>
            </thead>
            <tbody>
              @for (entry of sortedEntries(); track entry.entryNo) {
                <tr>
                  <td style="white-space:nowrap;color:var(--nx-text-strong);">{{ entry.postingDate | date:'dd/MM/yyyy' }}</td>
                  <td>{{ entry.documentType || '-' }}</td>
                  <td style="font-weight:var(--nx-weight-medium);">{{ entry.documentNo }}</td>
                  <td><a [routerLink]="['/finance/gl-accounts', entry.glAccountNo]" class="nx-link">{{ entry.glAccountNo }}</a></td>
                  <td>{{ entry.glAccountName }}</td>
                  <td style="max-width:200px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;" [title]="entry.description">{{ entry.description }}</td>
                  <td class="num nx-num">
                    <span [style.color]="entry.amount < 0 ? 'var(--nx-money-negative)' : (entry.amount > 0 ? 'var(--nx-money-positive)' : '')">
                      {{ entry.amount | currency:'DOP':'':'1.2-2' }}
                    </span>
                  </td>
                  <td class="num nx-num" style="color:var(--nx-text-muted);">{{ entry.entryNo }}</td>
                </tr>
              }
            </tbody>
          </table>
        </div>

        @if (svc.totalPages() > 1) {
          <div style="display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-3) var(--nx-space-4);border-top:1px solid var(--nx-border);">
            <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
              Total: {{ svc.totalItems() }} movimientos
            </div>
            <div style="display:flex;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm" [disabled]="svc.currentPage() <= 1" (click)="goToPage(svc.currentPage() - 1)">Anterior</button>
              <button class="nx-btn nx-btn--secondary nx-btn--sm" [disabled]="svc.currentPage() >= svc.totalPages()" (click)="goToPage(svc.currentPage() + 1)">Siguiente</button>
            </div>
          </div>
        }
      }
    </div>
  `,
  styles: [`
    :host { display: block; }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
    .num { text-align:right; font-variant-numeric:tabular-nums; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite; }
    @keyframes spin { to { transform:rotate(360deg); } }
  `]
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
