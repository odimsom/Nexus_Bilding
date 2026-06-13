export interface PurchaseInvoice {
  id: string;
  no: string;
  buyFromVendorNo: string;
  payToName: string;
  postingDate: string;
  amountIncludingVat: number;
}

export interface PurchaseInvoiceLine {
  description: string;
  quantity: number;
  unitCost: number;
  amount: number;
  amountIncludingVat: number;
  unitOfMeasureCode: string;
  vat: number;
}

export interface PurchaseInvoiceDetail extends PurchaseInvoice {
  amount: number;
  currencyCode: string;
  paymentTermsCode: string;
  lines: PurchaseInvoiceLine[];
}

export interface CreateInvoiceLineForm {
  description: string;
  quantity: number;
  unitCost: number;
  unitOfMeasureCode: string;
  vatPct: number;
}

export interface CreatePurchaseInvoiceDto {
  buyFromVendorNo: string;
  payToName: string;
  postingDate: string;
  externalDocumentNo: string;
  paymentTermsCode: string;
  currencyCode: string;
  lines: CreateInvoiceLineForm[];
}
