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
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Mayor General</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Plan de Cuentas (Mayor General)</h1>
        <p class="nx-page-subtitle">
          @if (svc.loading()) { Cargando… }
          @else { {{ svc.totalItems() }} cuentas registradas }
        </p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">Diario General</button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">Nueva Cuenta</button>
      </div>
    </div>

    <div class="filter-bar">
      <div class="nx-inputgroup" style="flex:1;min-width:220px;max-width:380px;">
        <span class="nx-adorn">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
        </span>
        <input type="search" class="nx-input" style="padding-left:36px;" placeholder="Buscar por cuenta o nombre…"
          [(ngModel)]="searchText" (ngModelChange)="onSearchChange()" />
      </div>

      <select class="nx-select" style="width:160px;" [(ngModel)]="showBlocked" (ngModelChange)="onFilterChange()">
        <option value="active">Solo activas</option>
        <option value="all">Todas</option>
        <option value="blocked">Bloqueadas</option>
      </select>

      @if (searchText || showBlocked !== 'active') {
        <button class="nx-btn nx-btn--ghost nx-btn--sm" (click)="clearFilters()" style="margin-left:auto;">Limpiar</button>
      }
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
          <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando catálogo…</p>
        </div>
      } @else if (visibleItems().length === 0) {
        <div class="nx-empty">
          <p class="nx-empty__title">No se encontraron cuentas</p>
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="clearFilters()">Limpiar filtros</button>
        </div>
      } @else {
        <div style="overflow-x:auto;">
          <table class="nx-table" aria-label="Catálogo de cuentas">
            <thead>
              <tr>
                <th class="sortable" (click)="setSort('no')">No. {{ si('no') }}</th>
                <th class="sortable" (click)="setSort('name')">Nombre {{ si('name') }}</th>
                <th>Tipo</th>
                <th>Estado de Resultados/Balance</th>
                <th class="num sortable" (click)="setSort('balance')">Balance {{ si('balance') }}</th>
              </tr>
            </thead>
            <tbody>
              @for (acc of visibleItems(); track acc.no) {
                <tr [class.is-heading]="acc.accountType === 1 || acc.accountType === 2" [class.is-total]="acc.accountType === 3 || acc.accountType === 4">
                  <td class="nx-td--doc">
                    <a [routerLink]="['/finance/gl-accounts', acc.no]" class="nx-link">{{ acc.no }}</a>
                  </td>
                  <td [style.padding-left]="acc.accountType === 0 ? 'var(--nx-space-5)' : 'var(--nx-space-3)'"
                      [style.font-weight]="acc.accountType === 0 ? 'var(--nx-weight-normal)' : 'var(--nx-weight-semibold)'"
                      style="color:var(--nx-text-strong);">
                    {{ acc.name }}
                  </td>
                  <td>{{ typeLabel(acc.accountType) }}</td>
                  <td style="color:var(--nx-text-muted);">{{ acc.incomeBalance === 0 ? 'Estado de Resultados' : 'Balance General' }}</td>
                  <td class="num nx-num">
                    @if (acc.accountType === 0 || acc.accountType === 3 || acc.accountType === 4) {
                      <span [style.color]="acc.balance < 0 ? 'var(--nx-money-negative)' : ''">
                        {{ acc.balance | currency:'DOP':'':'1.2-2' }}
                      </span>
                    }
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>

        @if (svc.totalPages() > 1) {
          <div style="display:flex;align-items:center;justify-content:center;gap:var(--nx-space-2);padding:var(--nx-space-3) var(--nx-space-4);border-top:1px solid var(--nx-border);">
            <button class="nx-btn nx-btn--ghost nx-btn--sm" [disabled]="svc.currentPage() <= 1" (click)="goToPage(svc.currentPage() - 1)">← Anterior</button>
            <span style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Página {{ svc.currentPage() }} de {{ svc.totalPages() }}</span>
            <button class="nx-btn nx-btn--ghost nx-btn--sm" [disabled]="svc.currentPage() >= svc.totalPages()" (click)="goToPage(svc.currentPage() + 1)">Siguiente →</button>
          </div>
        }
      }
    </div>
  `,
  styles: [`
    :host { display: block; }
    .filter-bar { display:flex;align-items:center;gap:var(--nx-space-3);flex-wrap:wrap;margin-bottom:var(--nx-space-4); }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
    .num { text-align:right; font-variant-numeric:tabular-nums; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite; }
    @keyframes spin { to { transform:rotate(360deg); } }
    tr.is-heading td { background: var(--nx-canvas-alt); }
    tr.is-total td { background: var(--nx-canvas-alt); border-top: 1px solid var(--nx-border); }
  `]
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
