import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { PurchaseOrder, CreatePurchaseOrderDto } from '../domain/purchase.model';

@Injectable({ providedIn: 'root' })
export class PurchaseOrderService {
  private readonly api = inject(ApiService);

  readonly items = signal<PurchaseOrder[]>([]);
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
        this.api.get<PagedData<PurchaseOrder>>(`purchasing/orders?page=${page}&pageSize=${size}`)
      );
      this.items.set(res.items);
      this.totalItems.set(res.pagination.totalItems);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar órdenes de compra.');
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getByNo(no: string): Promise<PurchaseOrder | null> {
    try {
      return await firstValueFrom(this.api.get<PurchaseOrder>(`purchasing/orders/${no}`));
    } catch {
      return null;
    }
  }

  async create(data: CreatePurchaseOrderDto): Promise<string> {
    const res = await firstValueFrom(
      this.api.post<{ no: string }>('purchasing/orders', data)
    );
    return res.no;
  }

  async updateLines(no: string, lines: {
    itemNo: string; description: string; quantity: number;
    unitPrice: number; lineDiscountPct: number; unitOfMeasure: string; lineType?: string;
  }[]): Promise<{ amount: number; amountIncludingVat: number }> {
    return firstValueFrom(
      this.api.put<{ amount: number; amountIncludingVat: number }>(
        `purchasing/orders/${encodeURIComponent(no)}/lines`, lines)
    );
  }

  async post(no: string): Promise<{ invoiceNo: string }> {
    return firstValueFrom(
      this.api.post<{ invoiceNo: string }>(`purchasing/orders/${encodeURIComponent(no)}/post`, {})
    );
  }

  async updateHeader(no: string, data: {
    dueDate: string | null;
    currencyCode: string;
    paymentTermsCode: string;
    externalDocumentNo: string | null;
  }): Promise<void> {
    await firstValueFrom(this.api.patch<void>(`purchasing/orders/${encodeURIComponent(no)}`, data));
  }
}
