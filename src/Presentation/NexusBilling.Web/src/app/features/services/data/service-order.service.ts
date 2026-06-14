import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { ServiceOrderDetail, ServiceOrderListItem } from '../domain/service-order.model';

export interface ServiceOrderLineInput {
  no: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType: number;
}

export interface CreateServiceOrderData {
  documentType: number;
  customerNo: string;
  customerName: string;
  description: string;
  orderDate: string;
  startingDate?: string | null;
  finishingDate?: string | null;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  currencyCode: string;
  contractNo: string;
  seriesCode?: string | null;
  manualNo?: string | null;
  lines: ServiceOrderLineInput[];
}

@Injectable({ providedIn: 'root' })
export class ServiceOrderService {
  private readonly api = inject(ApiService);

  readonly orders = signal<ServiceOrderListItem[]>([]);
  readonly loading = signal(false);

  async load(params?: { docType?: string; status?: string; search?: string; page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    try {
      const p: Record<string, string | number | undefined> = {
        page: params?.page ?? 1,
        pageSize: params?.pageSize ?? 100,
      };
      if (params?.docType) p['documentType'] = params.docType;
      if (params?.status) p['status'] = params.status;
      if (params?.search) p['search'] = params.search;
      const result = await firstValueFrom(this.api.get<PagedData<ServiceOrderListItem>>('service/orders', p));
      this.orders.set(result.items as ServiceOrderListItem[]);
    } catch {
      this.orders.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async create(data: CreateServiceOrderData): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ no: string }>('service/orders', data));
    return res.no;
  }

  async getDetail(no: string): Promise<ServiceOrderDetail> {
    return firstValueFrom(this.api.get<ServiceOrderDetail>(`service/orders/${encodeURIComponent(no)}`));
  }

  async updateStatus(no: string, status: number): Promise<void> {
    await firstValueFrom(this.api.patch<void>(`service/orders/${encodeURIComponent(no)}/status`, { status }));
  }

  async invoiceServiceOrder(no: string): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ invoiceNo: string }>(`service/orders/${encodeURIComponent(no)}/invoice`, {}));
    return res.invoiceNo;
  }
}
