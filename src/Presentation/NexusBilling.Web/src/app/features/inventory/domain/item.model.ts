export interface Item {
  no: string;
  description: string;
  description2: string;
  baseUnitOfMeasure: string;
  unitPrice: number;
  unitCost: number;
  blocked: boolean;
  inventory: number;
  type: string;
  itemCategoryCode: string;
  inventoryPostingGroup: string;
  genProdPostingGroup: string;
  vatProdPostingGroup: string;
  vendorNo: string;
  vendorItemNo: string;
  standardCost: number;
  lastDirectCost: number;
}

export interface ItemFilter {
  search?: string;
  blocked?: boolean | null;
  type?: string;
  itemCategoryCode?: string;
}

export type ItemSortField = 'no' | 'description' | 'unitPrice' | 'unitCost' | 'inventory';

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
