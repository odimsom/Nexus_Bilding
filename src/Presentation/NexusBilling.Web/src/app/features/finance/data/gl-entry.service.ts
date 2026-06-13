import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { GLEntry } from '../domain/gl-entry.model';

@Injectable({ providedIn: 'root' })
export class GLEntryService {
  private readonly api = inject(ApiService);

  readonly entries = signal<GLEntry[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  readonly totalItems = signal(0);
  readonly totalPages = signal(0);
  readonly currentPage = signal(1);

  async load(params?: { glAccountNo?: string; page?: number; pageSize?: number }) {
    this.loading.set(true);
    this.error.set(null);
    try {
      const q = new URLSearchParams();
      if (params?.glAccountNo) q.set('glAccountNo', params.glAccountNo);
      if (params?.page) q.set('page', String(params.page));
      if (params?.pageSize) q.set('pageSize', String(params.pageSize));

      const res = await firstValueFrom(this.api.get<{entries: GLEntry[], pagination: any}>(`finance/gl-entries?${q}`));
      this.entries.set(res.entries || []);
      this.totalItems.set(res.pagination?.totalItems || 0);
      this.totalPages.set(res.pagination?.totalPages || 1);
      this.currentPage.set(res.pagination?.page || 1);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'No se pudieron cargar los movimientos.');
    } finally {
      this.loading.set(false);
    }
  }
}
