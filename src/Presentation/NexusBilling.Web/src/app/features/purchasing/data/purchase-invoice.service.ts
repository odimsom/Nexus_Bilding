import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { PurchaseInvoice, PurchaseInvoiceDetail } from '../domain/purchase-invoice.model';

@Injectable({ providedIn: 'root' })
export class PurchaseInvoiceService {
  private readonly api = inject(ApiService);

  readonly items = signal<PurchaseInvoice[]>([]);
  readonly loading = signal(false);
  readonly totalItems = signal(0);
  readonly error = signal('');

  async load(options?: { page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const page = options?.page ?? 1;
      const size = options?.pageSize ?? 50;
      const res = await firstValueFrom(
        this.api.get<PagedData<PurchaseInvoice>>(`purchasing/invoices?page=${page}&pageSize=${size}`)
      );
      this.items.set(res.items);
      this.totalItems.set(res.pagination.totalItems);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar facturas de compra.');
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getByNo(no: string): Promise<PurchaseInvoiceDetail | null> {
    try {
      return await firstValueFrom(this.api.get<PurchaseInvoiceDetail>(`purchasing/invoices/${no}`));
    } catch {
      return null;
    }
  }
}
