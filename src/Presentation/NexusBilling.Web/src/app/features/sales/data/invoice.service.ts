import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { Invoice, SalesOrder, SalesOrderDetail } from '../domain/invoice.model';

export interface SalesOrderLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType?: string;
}

export interface CreateSalesOrderData {
  documentType: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  externalDocumentNo: string;
  currencyCode: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  postingDate: string;
  dueDate?: string | null;
  seriesCode?: string | null;
  manualNo?: string | null;
  lines: SalesOrderLine[];
}

@Injectable({ providedIn: 'root' })
export class InvoiceService {
  private readonly api = inject(ApiService);

  readonly orders = signal<SalesOrder[]>([]);
  readonly invoices = signal<Invoice[]>([]);
  readonly loading = signal(false);
  readonly error = signal('');

  /* ── Sales Orders ──────────────────────────────────────────── */

  async loadOrders(params?: { documentType?: string; status?: string; search?: string; page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const p: Record<string, string | number | undefined> = {
        page: params?.page ?? 1,
        pageSize: params?.pageSize ?? 100,
      };
      if (params?.documentType) p['documentType'] = params.documentType;
      if (params?.status) p['status'] = params.status;
      if (params?.search) p['search'] = params.search;

      const result = await firstValueFrom(this.api.get<PagedData<SalesOrder>>('sales/orders', p));
      this.orders.set(result.items as SalesOrder[]);
    } catch {
      this.orders.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async createOrder(data: CreateSalesOrderData): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ no: string }>('sales/orders', data));
    return res.no;
  }

  async getOrderDetail(no: string): Promise<SalesOrderDetail> {
    return firstValueFrom(this.api.get<SalesOrderDetail>(`sales/orders/${encodeURIComponent(no)}`));
  }

  /* ── Sales Invoices (posted) ───────────────────────────────── */

  async loadInvoices(params?: { search?: string; page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const p: Record<string, string | number | undefined> = {
        page: params?.page ?? 1,
        pageSize: params?.pageSize ?? 100,
      };
      if (params?.search) p['search'] = params.search;

      const result = await firstValueFrom(this.api.get<PagedData<Invoice>>('sales/invoices', p));
      this.invoices.set(result.items);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar facturas de venta.');
      this.invoices.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getInvoiceDetail(no: string): Promise<any> {
    return firstValueFrom(this.api.get<any>(`sales/invoices/${encodeURIComponent(no)}`));
  }

  getSalesOrders(): SalesOrder[] { return this.orders(); }
}
