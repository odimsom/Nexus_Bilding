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
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <a routerLink="/gl">Finanzas</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Diario General</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Diario General</h1>
        <p class="nx-page-subtitle">Plantilla: GENERAL | Lote: DEFAULT</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary" (click)="exportExcel()" [disabled]="svc.lines().length === 0">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg> Excel
        </button>
        <button class="nx-btn nx-btn--secondary" (click)="reload()" [disabled]="svc.loading()">Actualizar</button>
        <button class="nx-btn nx-btn--primary" (click)="postBatch()" [disabled]="svc.loading() || svc.lines().length === 0 || balance() !== 0">
          <i data-lucide="send" style="width:16px;height:16px;"></i> Registrar
        </button>
      </div>
    </div>

    @if (svc.error() || successMsg) {
      <div [class]="successMsg ? 'nx-callout nx-callout--success' : 'nx-callout nx-callout--danger'" style="margin-bottom:var(--nx-space-4);">
        {{ svc.error() || successMsg }}
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="svc.error.set(null); successMsg=''" style="margin-left:auto;">Cerrar</button>
      </div>
    }

    <!-- Nueva Línea -->
    <div class="nx-card" style="margin-bottom:var(--nx-space-4);padding:var(--nx-space-4);">
      <h3 style="font-size:var(--nx-text-base);font-weight:var(--nx-weight-semibold);margin-bottom:var(--nx-space-3);">Agregar Línea</h3>
      <form (ngSubmit)="addLine()" #f="ngForm" style="display:flex;gap:var(--nx-space-3);align-items:flex-end;flex-wrap:wrap;">
        <div class="nx-form-group" style="flex:1;min-width:140px;">
          <label class="nx-label">Fecha</label>
          <input type="date" class="nx-input" name="postingDate" [(ngModel)]="newLine.postingDate" required />
        </div>
        <div class="nx-form-group" style="flex:1;min-width:140px;">
          <label class="nx-label">No. Documento</label>
          <input type="text" class="nx-input" name="documentNo" [(ngModel)]="newLine.documentNo" required />
        </div>
        <div class="nx-form-group" style="flex:2;min-width:200px;">
          <label class="nx-label">No. Cuenta</label>
          <select class="nx-input" name="accountNo" [(ngModel)]="newLine.accountNo" required>
            <option value="">Seleccione cuenta...</option>
            @for (acc of glSvc.accounts(); track acc.no) {
              <option [value]="acc.no">{{ acc.no }} - {{ acc.name }}</option>
            }
          </select>
        </div>
        <div class="nx-form-group" style="flex:2;min-width:200px;">
          <label class="nx-label">Descripción</label>
          <input type="text" class="nx-input" name="description" [(ngModel)]="newLine.description" required />
        </div>
        <div class="nx-form-group" style="flex:1;min-width:140px;">
          <label class="nx-label">Monto (Débito + / Crédito -)</label>
          <input type="number" step="0.01" class="nx-input" name="amount" [(ngModel)]="newLine.amount" required />
        </div>
        <div class="nx-form-group" style="flex:2;min-width:200px;">
          <label class="nx-label">Cta. Contrapartida (Opcional)</label>
          <select class="nx-input" name="balAccountNo" [(ngModel)]="newLine.balAccountNo">
            <option value="">Ninguna...</option>
            @for (acc of glSvc.accounts(); track acc.no) {
              <option [value]="acc.no">{{ acc.no }} - {{ acc.name }}</option>
            }
          </select>
        </div>
        <button type="submit" class="nx-btn nx-btn--primary" [disabled]="f.invalid || svc.loading()">Agregar</button>
      </form>
    </div>

    <!-- Lista de Líneas -->
    <div class="nx-card" style="overflow:hidden;">
      @if (svc.loading() && svc.lines().length === 0) {
        <div class="nx-empty">
          <div class="nx-spinner"></div>
          <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando diario…</p>
        </div>
      } @else if (svc.lines().length === 0) {
        <div class="nx-empty">
          <p class="nx-empty__title">Diario vacío</p>
          <p class="nx-empty__text">Agregue líneas usando el formulario superior.</p>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Líneas del Diario">
            <thead>
              <tr>
                <th class="sortable" style="width:100px;" (click)="setSort('postingDate')">Fecha {{ si('postingDate') }}</th>
                <th class="sortable" (click)="setSort('documentNo')">No. Doc. {{ si('documentNo') }}</th>
                <th class="sortable" (click)="setSort('accountNo')">Cta. No. {{ si('accountNo') }}</th>
                <th class="sortable" (click)="setSort('description')">Descripción {{ si('description') }}</th>
                <th class="sortable" (click)="setSort('balAccountNo')">Cta. Contrapartida {{ si('balAccountNo') }}</th>
                <th class="num sortable" (click)="setSort('amount')">Monto {{ si('amount') }}</th>
                <th style="width:60px;text-align:right;"></th>
              </tr>
            </thead>
            <tbody>
              @for (line of sortedLines(); track line.id) {
                <tr>
                  <td style="white-space:nowrap;color:var(--nx-text-strong);">{{ line.postingDate | date:'dd/MM/yyyy' }}</td>
                  <td style="font-weight:var(--nx-weight-medium);">{{ line.documentNo }}</td>
                  <td>{{ line.accountNo }}</td>
                  <td>{{ line.description }}</td>
                  <td>{{ line.balAccountNo || '-' }}</td>
                  <td class="num nx-num">
                    <span [style.color]="line.amount < 0 ? 'var(--nx-money-negative)' : (line.amount > 0 ? 'var(--nx-money-positive)' : '')">
                      {{ line.amount | currency:'DOP':'':'1.2-2' }}
                    </span>
                  </td>
                  <td style="text-align:right;">
                    <button class="nx-iconbtn nx-iconbtn--sm" (click)="deleteLine(line.id)" title="Eliminar línea">
                      <i data-lucide="trash-2" style="width:16px;height:16px;color:var(--nx-money-negative);"></i>
                    </button>
                  </td>
                </tr>
              }
            </tbody>
            <tfoot style="border-top:2px solid var(--nx-border);background:var(--nx-bg-subtle);">
              <tr>
                <td colspan="5" style="text-align:right;font-weight:var(--nx-weight-semibold);padding:var(--nx-space-3) var(--nx-space-4);">Descuadre / Balance Total:</td>
                <td class="num nx-num" style="font-weight:var(--nx-weight-bold);padding:var(--nx-space-3) var(--nx-space-4);">
                  <span [style.color]="balance() === 0 ? 'var(--nx-money-positive)' : 'var(--nx-money-negative)'">
                    {{ balance() | currency:'DOP':'':'1.2-2' }}
                  </span>
                </td>
                <td></td>
              </tr>
            </tfoot>
          </table>
        </div>
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
