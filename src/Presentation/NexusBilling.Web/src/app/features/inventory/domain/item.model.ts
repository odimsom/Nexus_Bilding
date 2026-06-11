/**
 * Item — frontend model
 * Maps: NexusBilling.Core.Domain.Inventory.Entities.Item
 *
 * Key fields: No, Description, BaseUnitOfMeasure, UnitPrice, UnitCost, Blocked
 * Extended with inventory quantities from ItemLedgerEntry aggregations
 */
export interface Item {
  no: string;
  description: string;
  description2?: string;
  baseUnitOfMeasure: string;
  unitPrice: number;
  unitCost: number;
  blocked: boolean;
  type?: string;               // Inventory | Service | Non-Inventory
  inventoryPostingGroup?: string;
  genProdPostingGroup?: string;
  vatProdPostingGroup?: string;
  itemCategoryCode?: string;
  vendor?: string;
  vendorItemNo?: string;
  inventory?: number;          // current stock quantity (from ItemLedgerEntry sum)
  qtyOnSalesOrder?: number;
  qtyOnPurchOrder?: number;
  standardCost?: number;
  lastDirectCost?: number;
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
