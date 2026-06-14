import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GLAccountService, GLAccountSortField } from '../../../data/gl-account.service';
import { GLAccount } from '../../../domain/gl-account.model';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-gl-account-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './gl-account-list.page.html',
  styleUrl: './gl-account-list.page.css'
})
export class GLAccountListPage implements OnInit {
  readonly svc = inject(GLAccountService);
  private readonly router = inject(Router);
  private readonly excel = inject(ExcelExportService);

  searchText = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'active';
  sortField: GLAccountSortField = 'no';
  sortAsc = true;

  private _searchTimeout?: ReturnType<typeof setTimeout>;

  visibleItems(): GLAccount[] { return this.svc.sorted(this.sortField, this.sortAsc); }

  ngOnInit(): void { this.reload(); }

  reload(): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked });
  }

  onSearchChange(): void {
    clearTimeout(this._searchTimeout);
    this._searchTimeout = setTimeout(() => this.reload(), 350);
  }

  onFilterChange(): void { this.reload(); }

  setSort(f: GLAccountSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: GLAccountSortField): string { return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : ''; }

  clearFilters(): void {
    this.searchText = '';
    this.showBlocked = 'active';
    this.reload();
  }

  goToPage(page: number): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked, page });
  }

  typeLabel(t: number): string {
    const types = ['Registro', 'Encabezado', 'Total', 'Inicio-Total', 'Fin-Total'];
    return types[t] ?? 'Desconocido';
  }

  viewEntries(accountNo: string): void {
    this.router.navigate(['/gl-entries'], { queryParams: { accountNo } });
  }

  goToJournal(): void {
    this.router.navigate(['/journal']);
  }

  async exportExcel(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Plan_de_Cuentas',
      title: 'Plan de Cuentas',
      subtitle: 'Nexus Billing - Módulo de Finanzas',
      sheetName: 'Cuentas GL',
      columns: [
        { header: 'No. Cuenta', key: 'no', width: 14 },
        { header: 'Nombre', key: 'name', width: 40 },
        { header: 'Tipo', key: 'tipo', width: 15 },
        { header: 'Saldo', key: 'balance', width: 18, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
        { header: 'Bloqueada', key: 'blocked', width: 12 },
      ],
      data: this.visibleItems().map(a => ({
        no: a.no,
        name: a.name,
        tipo: this.typeLabel(a.accountType),
        balance: a.balance ?? 0,
        blocked: a.blocked ? 'Sí' : 'No',
      })),
    });
  }
}
