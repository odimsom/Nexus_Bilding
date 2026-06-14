import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { GLAccountService, GLAccountSortField } from '../../../data/gl-account.service';
import { GLAccount } from '../../../domain/gl-account.model';

@Component({
  selector: 'app-gl-account-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './gl-account-list.page.html',
  styleUrl: './gl-account-list.page.css'
})
export class GLAccountListPage implements OnInit {
  readonly svc = inject(GLAccountService);

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
}
