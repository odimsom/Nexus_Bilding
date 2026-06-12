import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { Invoice, InvoiceFilter, InvoiceLine, InvoiceSortField, SalesOrder } from '../domain/invoice.model';

export interface SalesOrderLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
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
  readonly loading = signal(false);

  async loadOrders(params?: { documentType?: string; status?: string; search?: string; page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
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

  // Legacy mock methods kept for invoice list (posted invoices not yet in DB)
  getAll(): Invoice[] { return MOCK_INVOICES; }
  getByNo(no: string): Invoice | undefined { return MOCK_INVOICES.find(i => i.no === no); }
  getLinesForInvoice(no: string): InvoiceLine[] { return MOCK_LINES[no] ?? []; }

  filter(f: InvoiceFilter, sortField: InvoiceSortField = 'postingDate', sortAsc = false): Invoice[] {
    let result = MOCK_INVOICES;
    if (f.search) { const q = f.search.toLowerCase(); result = result.filter(i => i.no.toLowerCase().includes(q) || i.sellToCustomerName.toLowerCase().includes(q) || i.externalDocumentNo?.toLowerCase().includes(q)); }
    if (f.status) result = result.filter(i => i.status === f.status);
    if (f.customerId) result = result.filter(i => i.sellToCustomerNo === f.customerId);
    if (f.dateFrom) result = result.filter(i => i.postingDate >= f.dateFrom!);
    if (f.dateTo) result = result.filter(i => i.postingDate <= f.dateTo!);
    return [...result].sort((a, b) => {
      let av: string | number = '', bv: string | number = '';
      switch (sortField) {
        case 'no': av = a.no; bv = b.no; break;
        case 'postingDate': av = a.postingDate; bv = b.postingDate; break;
        case 'dueDate': av = a.dueDate ?? ''; bv = b.dueDate ?? ''; break;
        case 'sellToCustomerName': av = a.sellToCustomerName; bv = b.sellToCustomerName; break;
        case 'amountIncludingVat': av = a.amountIncludingVat; bv = b.amountIncludingVat; break;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }

  countForTab(status: string | 'all'): number {
    return status === 'all' ? MOCK_INVOICES.length : MOCK_INVOICES.filter(i => i.status === status).length;
  }

  getSalesOrders(): SalesOrder[] { return this.orders(); }
}

const MOCK_INVOICES: Invoice[] = [
  { no: 'INV-2025-0041', sellToCustomerNo: 'C-00001', sellToCustomerName: 'Altagracia Comercial S.R.L.', billToName: 'Altagracia Comercial S.R.L.', postingDate: '2025-06-11', dueDate: '2025-07-11', documentDate: '2025-06-11', externalDocumentNo: 'PO-4521', paymentTermsCode: '30 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'RMT', currencyCode: '', amount: 124790.76, amountIncludingVat: 148500, remainingAmount: 0, status: 'posted' },
  { no: 'INV-2025-0040', sellToCustomerNo: 'C-00002', sellToCustomerName: 'Distribuidora Los Alcarrizos', billToName: 'Distribuidora Los Alcarrizos', postingDate: '2025-06-10', dueDate: '2025-06-25', documentDate: '2025-06-10', externalDocumentNo: 'REQ-0234', paymentTermsCode: '15 DIAS', paymentMethodCode: 'CHEQUE', salespersonCode: 'RMT', currencyCode: '', amount: 63109.24, amountIncludingVat: 75200, remainingAmount: 75200, status: 'open' },
  { no: 'INV-2025-0039', sellToCustomerNo: 'C-00003', sellToCustomerName: 'Ferretería El Progreso', billToName: 'Ferretería El Progreso', postingDate: '2025-06-09', dueDate: '2025-06-09', documentDate: '2025-06-09', paymentTermsCode: 'CONTADO', paymentMethodCode: 'EFECTIVO', salespersonCode: 'MAV', currencyCode: '', amount: 19159.66, amountIncludingVat: 22800, remainingAmount: 22800, status: 'overdue' },
  { no: 'INV-2025-0038', sellToCustomerNo: 'C-00004', sellToCustomerName: 'Supermercado Bravo', billToName: 'Supermercado Bravo', postingDate: '2025-06-08', dueDate: '2025-07-08', documentDate: '2025-06-08', externalDocumentNo: 'OC-2025-089', paymentTermsCode: '30 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'RMT', currencyCode: '', amount: 260714.29, amountIncludingVat: 310750, remainingAmount: 100000, status: 'open' },
  { no: 'INV-2025-0037', sellToCustomerNo: 'C-00005', sellToCustomerName: 'Grupo Estrella', billToName: 'Grupo Estrella', postingDate: '2025-06-07', dueDate: '2025-08-07', documentDate: '2025-06-07', externalDocumentNo: 'GE-20250607', paymentTermsCode: '60 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'MAV', currencyCode: '', amount: 747899.16, amountIncludingVat: 890000, remainingAmount: 0, status: 'posted' },
];

const MOCK_LINES: Record<string, InvoiceLine[]> = {
  'INV-2025-0041': [
    { lineNo: 10000, type: 'Item', no: 'ART-0012', description: 'Cemento Portland 42.5 kg', quantity: 200, unitOfMeasureCode: 'SAC', unitPrice: 420, lineDiscountPct: 5, amount: 79800, amountIncludingVat: 95000 },
    { lineNo: 20000, type: 'Item', no: 'ART-0045', description: 'Varilla de Hierro 3/8"', quantity: 50, unitOfMeasureCode: 'QQ', unitPrice: 875, lineDiscountPct: 0, amount: 43750, amountIncludingVat: 52062.5 },
  ],
};
