import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { CreateQuotationData, QuotationDetail, QuotationListItem } from '../domain/quotation.model';

@Injectable({ providedIn: 'root' })
export class QuotationService {
  private readonly api = inject(ApiService);

  readonly items = signal<QuotationListItem[]>([]);
  readonly loading = signal(false);
  readonly error = signal('');

  async load(params?: { customerNo?: string; search?: string; status?: string; page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const p: Record<string, string | number | undefined> = {
        page: params?.page ?? 1,
        pageSize: params?.pageSize ?? 100,
      };
      if (params?.customerNo) p['customerNo'] = params.customerNo;
      if (params?.search) p['search'] = params.search;
      if (params?.status) p['status'] = params.status;

      const result = await firstValueFrom(this.api.get<PagedData<QuotationListItem>>('sales/quotations', p));
      this.items.set(result.items);
    } catch {
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getByNo(no: string): Promise<QuotationDetail> {
    return firstValueFrom(this.api.get<QuotationDetail>(`sales/quotations/${encodeURIComponent(no)}`));
  }

  async create(data: CreateQuotationData): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ no: string }>('sales/quotations', data));
    return res.no;
  }

  async convertToOrder(no: string, seriesCode = 'ORD'): Promise<string> {
    const res = await firstValueFrom(
      this.api.post<{ orderNo: string }>(`sales/quotations/${encodeURIComponent(no)}/convert-to-order`, { seriesCode })
    );
    return res.orderNo;
  }

  async updateHeader(no: string, data: {
    validUntilDate: string | null;
    quotedBy: string | null;
    observations: string | null;
    currencyCode: string;
    paymentTermsCode: string;
    paymentMethodCode: string;
    externalDocumentNo: string | null;
  }): Promise<void> {
    await firstValueFrom(this.api.patch<void>(`sales/quotations/${encodeURIComponent(no)}`, data));
  }

  async updateLines(no: string, lines: Array<{
    lineType: string;
    itemNo: string;
    description: string;
    quantity: number;
    unitPrice: number;
    lineDiscountPct: number;
    unitOfMeasure: string;
    vatPct: number;
  }>): Promise<{ amount: number; amountIncludingVat: number }> {
    return firstValueFrom(
      this.api.put<{ amount: number; amountIncludingVat: number }>(
        `sales/quotations/${encodeURIComponent(no)}/lines`, lines)
    );
  }

  async duplicate(no: string, newPostingDate: string, newValidUntilDate: string | null, seriesCode = 'COT'): Promise<string> {
    const res = await firstValueFrom(
      this.api.post<{ no: string }>(`sales/quotations/${encodeURIComponent(no)}/duplicate`, {
        newPostingDate,
        newValidUntilDate,
        seriesCode
      })
    );
    return res.no;
  }
}
