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
