export interface PurchaseOrderLine {
  lineNo: number;
  itemNo: string;
  description: string;
  unitOfMeasure: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  lineDiscountAmount: number;
  amount: number;
  amountIncludingVat: number;
}

export interface PurchaseOrder {
  no: string;
  vendorNo: string;
  vendorName: string;
  postingDate: string;
  dueDate?: string;
  status: string;
  amount: number;
  amountIncludingVat: number;
  currencyCode?: string;
  paymentTermsCode?: string;
  externalDocumentNo?: string;
  lines: PurchaseOrderLine[];
}

export interface CreatePurchaseOrderDto {
  buyFromVendorNo: string;
  payToName?: string;
  postingDate?: string;
  dueDate?: string;
  currencyCode?: string;
  paymentTermsCode?: string;
  externalDocumentNo?: string;
  lines?: {
    lineType: string;
    itemNo?: string;
    description: string;
    unitOfMeasure: string;
    quantity: number;
    unitPrice: number;
    lineDiscountPct: number;
  }[];
}
