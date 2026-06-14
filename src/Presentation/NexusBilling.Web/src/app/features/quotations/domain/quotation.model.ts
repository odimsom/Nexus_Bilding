export interface QuotationListItem {
  no: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  postingDate: string;
  validUntilDate: string | null;
  quotedBy: string | null;
  status: string;
  amount: number;
  amountIncludingVat: number;
  isExpired: boolean;
}

export interface QuotationLine {
  lineNo: number;
  lineType: 'Item' | 'Service';
  no: string;
  description: string;
  quantity: number;
  unitOfMeasure: string;
  unitPrice: number;
  lineDiscountPct: number;
  amount: number;
  amountIncludingVat: number;
  vatPct: number;
  // Service fields
  serviceBillingType: number | null;  // 0=TiempoEstandar 1=ValorUnico 2=TiempoEstimado
  serviceStartDate: string | null;
  serviceEndDate: string | null;
  serviceHours: number | null;
  hourlyRate: number | null;
  resourceNo: string | null;
}

export interface QuotationDetail {
  no: string;
  sellToCustomerNo: string;
  sellToCustomerName: string;
  postingDate: string;
  validUntilDate: string | null;
  quotedBy: string | null;
  observations: string | null;
  status: string;
  currencyCode: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  externalDocumentNo: string;
  amount: number;
  amountIncludingVat: number;
  isExpired: boolean;
  lines: QuotationLine[];
}

export interface CreateQuotationLineData {
  lineType: 'Item' | 'Service';
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  vatPct: number;
  serviceBillingType: number | null;
  serviceStartDate: string | null;
  serviceEndDate: string | null;
  serviceHours: number | null;
  hourlyRate: number | null;
  resourceNo: string | null;
}

export interface CreateQuotationData {
  sellToCustomerNo: string;
  sellToCustomerName: string;
  postingDate: string;
  validUntilDate: string | null;
  quotedBy: string | null;
  observations: string | null;
  currencyCode: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  externalDocumentNo: string | null;
  seriesCode: string;
  lines: CreateQuotationLineData[];
}
