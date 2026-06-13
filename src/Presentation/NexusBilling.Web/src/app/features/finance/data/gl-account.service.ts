import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { GLAccount } from '../domain/gl-account.model';

export type GLAccountSortField = 'no' | 'name' | 'accountType' | 'balance';

@Injectable({ providedIn: 'root' })
export class GLAccountService {
  private readonly api = inject(ApiService);

  readonly accounts = signal<GLAccount[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  readonly totalItems = signal(0);
  readonly totalPages = signal(0);
  readonly currentPage = signal(1);

  async load(params?: { search?: string; blocked?: boolean; page?: number; pageSize?: number }) {
    this.loading.set(true);
    this.error.set(null);
    try {
      const q = new URLSearchParams();
      if (params?.search) q.set('search', params.search);
      if (params?.blocked !== undefined) q.set('blocked', String(params.blocked));
      if (params?.page) q.set('page', String(params.page));
      if (params?.pageSize) q.set('pageSize', String(params.pageSize));

      const res = await firstValueFrom(this.api.get<{accounts: GLAccount[], pagination: any}>(`finance/gl-accounts?${q}`));
      this.accounts.set(res.accounts || []);
      this.totalItems.set(res.pagination?.totalItems || 0);
      this.totalPages.set(res.pagination?.totalPages || 1);
      this.currentPage.set(res.pagination?.page || 1);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'No se pudo cargar el plan de cuentas.');
    } finally {
      this.loading.set(false);
    }
  }

  sorted(field: GLAccountSortField, asc: boolean): GLAccount[] {
    return [...this.accounts()].sort((a, b) => {
      let v1 = a[field], v2 = b[field];
      if (typeof v1 === 'string' && typeof v2 === 'string') {
        return asc ? v1.localeCompare(v2) : v2.localeCompare(v1);
      }
      return asc ? (v1 as number) - (v2 as number) : (v2 as number) - (v1 as number);
    });
  }
}
