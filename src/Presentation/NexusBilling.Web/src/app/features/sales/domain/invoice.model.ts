/**
 * SalesInvoiceHeader — frontend model
 * Maps: NexusBilling.Core.Domain.Sales.Entities.SalesInvoiceHeader
 *
 * Key fields: No, SellToCustomerNo, SellToCustomerName, PostingDate,
 * DueDate, CurrencyCode, Amount, AmountIncludingVat, Paid/Open status
 */
export interface Invoice {
  no: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  billToName: string;
  postingDate: string;          // ISO date
  dueDate: string | null;
  documentDate: string;
  externalDocumentNo?: string;
  paymentTermsCode?: string;
  paymentMethodCode?: string;
  salespersonCode?: string;
  currencyCode: string;         // '' = LCY (local)
  amount: number;               // before VAT
  amountIncludingVat: number;   // after VAT
  remainingAmount: number;      // for open entries
  status: InvoiceStatus;
  locationCode?: string;
  orderNo?: string;
}

export type InvoiceStatus = 'open' | 'posted' | 'overdue' | 'paid';

export interface InvoiceLine {
  lineNo: number;
  type: string;                 // Item, G/L Account, etc.
  no: string;                  // Item No or GL Account No
  description: string;
  quantity: number;
  unitOfMeasureCode: string;
  unitPrice: number;
  lineDiscountPct: number;
  amount: number;
  amountIncludingVat: number;
}

export interface InvoiceFilter {
  search?: string;
  status?: InvoiceStatus | null;
  customerId?: string;
  dateFrom?: string;
  dateTo?: string;
  currencyCode?: string;
}

export type InvoiceSortField = 'no' | 'postingDate' | 'dueDate' | 'sellToCustomerName' | 'amountIncludingVat';

export interface SalesOrder {
  no: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  documentType: 'Order' | 'Quote' | 'Blanket Order' | 'Return Order';
  postingDate: string;
  dueDate: string | null;
  status: SalesOrderStatus;
  currencyCode: string;
  amount: number;
  amountIncludingVat: number;
  externalDocumentNo?: string;
}

export type SalesOrderStatus = 'open' | 'released' | 'pending_approval' | 'pending_prepayment';

export interface SalesOrderDetailLine {
  lineNo: number;
  type: string;
  no: string;
  description: string;
  unitOfMeasure: string;
  quantity: number;
  unitPrice: number;
  lineDiscount: number;
  lineDiscountAmount: number;
  amount: number;
  amountIncludingVat: number;
  vat: number;
}

export interface SalesOrderDetail {
  no: string;
  documentType: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  postingDate: string;
  dueDate: string | null;
  amount: number;
  amountIncludingVat: number;
  currencyCode: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  externalDocumentNo: string;
  status: string;
  lines: SalesOrderDetailLine[];
}

