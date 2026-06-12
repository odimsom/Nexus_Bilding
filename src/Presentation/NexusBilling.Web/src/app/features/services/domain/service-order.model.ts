export interface ServiceOrderListItem {
  no: string;
  documentType: string;
  customerNo: string;
  customerName: string;
  description: string;
  startingDate: string | null;
  finishingDate: string | null;
  orderDate: string | null;
  status: number;
  statusLabel: string;
  contractNo: string;
}

export interface ServiceLine {
  lineNo: number;
  typeCode: number;
  typeLabel: string;
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

export interface ServiceOrderDetail {
  no: string;
  documentType: number;
  documentTypeLabel: string;
  customerNo: string;
  customerName: string;
  description: string;
  orderDate: string | null;
  startingDate: string | null;
  finishingDate: string | null;
  dueDate: string | null;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  currencyCode: string;
  contractNo: string;
  status: number;
  statusLabel: string;
  amount: number;
  amountIncludingVat: number;
  lines: ServiceLine[];
}
