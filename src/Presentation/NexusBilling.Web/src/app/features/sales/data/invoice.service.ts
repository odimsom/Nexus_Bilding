import { Injectable, signal } from '@angular/core';
import { Invoice, InvoiceFilter, InvoiceLine, InvoiceSortField, SalesOrder } from '../domain/invoice.model';

/** Mock service — replace with API calls once backend endpoints are ready */
@Injectable({ providedIn: 'root' })
export class InvoiceService {

  private readonly _invoices = signal<Invoice[]>(MOCK_INVOICES);

  getAll(): Invoice[] {
    return this._invoices();
  }

  getByNo(no: string): Invoice | undefined {
    return this._invoices().find(i => i.no === no);
  }

  getLinesForInvoice(no: string): InvoiceLine[] {
    return MOCK_LINES[no] ?? [];
  }

  filter(f: InvoiceFilter, sortField: InvoiceSortField = 'postingDate', sortAsc = false): Invoice[] {
    let result = this._invoices();

    if (f.search) {
      const q = f.search.toLowerCase();
      result = result.filter(i =>
        i.no.toLowerCase().includes(q) ||
        i.sellToCustomerName.toLowerCase().includes(q) ||
        i.externalDocumentNo?.toLowerCase().includes(q)
      );
    }
    if (f.status) result = result.filter(i => i.status === f.status);
    if (f.customerId) result = result.filter(i => i.sellToCustomerNo === f.customerId);
    if (f.dateFrom) result = result.filter(i => i.postingDate >= f.dateFrom!);
    if (f.dateTo)   result = result.filter(i => i.postingDate <= f.dateTo!);
    if (f.currencyCode) result = result.filter(i => i.currencyCode === f.currencyCode);

    return [...result].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (sortField) {
        case 'no':                  av = a.no; bv = b.no; break;
        case 'postingDate':         av = a.postingDate; bv = b.postingDate; break;
        case 'dueDate':             av = a.dueDate ?? ''; bv = b.dueDate ?? ''; break;
        case 'sellToCustomerName':  av = a.sellToCustomerName; bv = b.sellToCustomerName; break;
        case 'amountIncludingVat':  av = a.amountIncludingVat; bv = b.amountIncludingVat; break;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }

  getSalesOrders(): SalesOrder[] {
    return MOCK_ORDERS;
  }
}

/* ─── MOCK DATA ──────────────────────────────────────────────────────── */
const MOCK_INVOICES: Invoice[] = [
  { no: 'INV-2025-0041', sellToCustomerNo: 'C-00001', sellToCustomerName: 'Altagracia Comercial S.R.L.', billToName: 'Altagracia Comercial S.R.L.', postingDate: '2025-06-11', dueDate: '2025-07-11', documentDate: '2025-06-11', externalDocumentNo: 'PO-4521', paymentTermsCode: '30 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'RMT', currencyCode: '', amount: 124790.76, amountIncludingVat: 148500, remainingAmount: 0, status: 'posted' },
  { no: 'INV-2025-0040', sellToCustomerNo: 'C-00002', sellToCustomerName: 'Distribuidora Los Alcarrizos', billToName: 'Distribuidora Los Alcarrizos', postingDate: '2025-06-10', dueDate: '2025-06-25', documentDate: '2025-06-10', externalDocumentNo: 'REQ-0234', paymentTermsCode: '15 DIAS', paymentMethodCode: 'CHEQUE', salespersonCode: 'RMT', currencyCode: '', amount: 63109.24, amountIncludingVat: 75200, remainingAmount: 75200, status: 'open' },
  { no: 'INV-2025-0039', sellToCustomerNo: 'C-00003', sellToCustomerName: 'Ferretería El Progreso', billToName: 'Ferretería El Progreso', postingDate: '2025-06-09', dueDate: '2025-06-09', documentDate: '2025-06-09', paymentTermsCode: 'CONTADO', paymentMethodCode: 'EFECTIVO', salespersonCode: 'MAV', currencyCode: '', amount: 19159.66, amountIncludingVat: 22800, remainingAmount: 22800, status: 'overdue' },
  { no: 'INV-2025-0038', sellToCustomerNo: 'C-00004', sellToCustomerName: 'Supermercado Bravo', billToName: 'Supermercado Bravo', postingDate: '2025-06-08', dueDate: '2025-07-08', documentDate: '2025-06-08', externalDocumentNo: 'OC-2025-089', paymentTermsCode: '30 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'RMT', currencyCode: '', amount: 260714.29, amountIncludingVat: 310750, remainingAmount: 100000, status: 'open' },
  { no: 'INV-2025-0037', sellToCustomerNo: 'C-00005', sellToCustomerName: 'Grupo Estrella', billToName: 'Grupo Estrella', postingDate: '2025-06-07', dueDate: '2025-08-07', documentDate: '2025-06-07', externalDocumentNo: 'GE-20250607', paymentTermsCode: '60 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'MAV', currencyCode: '', amount: 747899.16, amountIncludingVat: 890000, remainingAmount: 0, status: 'posted' },
  { no: 'INV-2025-0036', sellToCustomerNo: 'C-00006', sellToCustomerName: 'Importadora Caribbean Tech', billToName: 'Importadora Caribbean Tech', postingDate: '2025-06-05', dueDate: '2025-07-20', documentDate: '2025-06-05', externalDocumentNo: 'CT-451', paymentTermsCode: 'NET 45', paymentMethodCode: 'WIRE', salespersonCode: 'MAV', currencyCode: 'USD', amount: 3500, amountIncludingVat: 4165, remainingAmount: 0, status: 'posted' },
  { no: 'INV-2025-0035', sellToCustomerNo: 'C-00008', sellToCustomerName: 'Constructora NovaBuild S.A.', billToName: 'Constructora NovaBuild S.A.', postingDate: '2025-06-03', dueDate: '2025-09-01', documentDate: '2025-06-03', externalDocumentNo: 'NB-2025-03', paymentTermsCode: '90 DIAS', paymentMethodCode: 'TRANSFERENCIA', salespersonCode: 'MAV', currencyCode: '', amount: 1050420.17, amountIncludingVat: 1250000, remainingAmount: 250000, status: 'open' },
  { no: 'INV-2025-0034', sellToCustomerNo: 'C-00009', sellToCustomerName: 'Gasolinera San Miguel', billToName: 'Gasolinera San Miguel', postingDate: '2025-06-02', dueDate: null, documentDate: '2025-06-02', paymentTermsCode: 'CONTADO', paymentMethodCode: 'EFECTIVO', salespersonCode: 'RMT', currencyCode: '', amount: 10504.20, amountIncludingVat: 12500, remainingAmount: 0, status: 'paid' },
  { no: 'INV-2025-0033', sellToCustomerNo: 'C-00010', sellToCustomerName: 'Hotel Bahía Samana', billToName: 'Hotel Bahía Samana', postingDate: '2025-06-01', dueDate: '2025-07-01', documentDate: '2025-06-01', externalDocumentNo: 'HBS-JUNE-01', paymentTermsCode: 'NET 30', paymentMethodCode: 'WIRE', salespersonCode: 'MAV', currencyCode: 'USD', amount: 4789, amountIncludingVat: 5699, remainingAmount: 0, status: 'paid' },
  { no: 'INV-2025-0032', sellToCustomerNo: 'C-00007', sellToCustomerName: 'Farmacia El Alivio', billToName: 'Farmacia El Alivio', postingDate: '2025-05-28', dueDate: '2025-06-27', documentDate: '2025-05-28', paymentTermsCode: '30 DIAS', paymentMethodCode: 'CHEQUE', salespersonCode: 'RMT', currencyCode: '', amount: 15294.12, amountIncludingVat: 18200, remainingAmount: 18200, status: 'overdue' },
];

const MOCK_LINES: Record<string, InvoiceLine[]> = {
  'INV-2025-0041': [
    { lineNo: 10000, type: 'Item', no: 'ART-0012', description: 'Cemento Portland 42.5 kg', quantity: 200, unitOfMeasureCode: 'SAC', unitPrice: 420, lineDiscountPct: 5, amount: 79800, amountIncludingVat: 95000 },
    { lineNo: 20000, type: 'Item', no: 'ART-0045', description: 'Varilla de Hierro 3/8"', quantity: 50, unitOfMeasureCode: 'QQ', unitPrice: 875, lineDiscountPct: 0, amount: 43750, amountIncludingVat: 52062.5 },
  ],
  'INV-2025-0040': [
    { lineNo: 10000, type: 'Item', no: 'ART-0089', description: 'Aceite Motor 20W50 1L', quantity: 120, unitOfMeasureCode: 'UND', unitPrice: 385, lineDiscountPct: 0, amount: 46200, amountIncludingVat: 54978 },
    { lineNo: 20000, type: 'Item', no: 'ART-0090', description: 'Filtro Aceite Universal', quantity: 60, unitOfMeasureCode: 'UND', unitPrice: 280, lineDiscountPct: 2, amount: 16464, amountIncludingVat: 19592.16 },
  ],
};

const MOCK_ORDERS: SalesOrder[] = [
  { no: 'SO-2025-0081', sellToCustomerNo: 'C-00001', sellToCustomerName: 'Altagracia Comercial S.R.L.', documentType: 'Order', postingDate: '2025-06-11', dueDate: '2025-06-25', status: 'released', currencyCode: '', amount: 84000, amountIncludingVat: 100000, externalDocumentNo: 'PO-4600' },
  { no: 'SO-2025-0080', sellToCustomerNo: 'C-00004', sellToCustomerName: 'Supermercado Bravo', documentType: 'Order', postingDate: '2025-06-10', dueDate: '2025-06-30', status: 'open', currencyCode: '', amount: 210000, amountIncludingVat: 250000, externalDocumentNo: 'OC-2025-092' },
  { no: 'SQ-2025-0015', sellToCustomerNo: 'C-00008', sellToCustomerName: 'Constructora NovaBuild S.A.', documentType: 'Quote', postingDate: '2025-06-09', dueDate: null, status: 'open', currencyCode: '', amount: 504000, amountIncludingVat: 600000 },
];
