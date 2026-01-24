export interface StockTransaction {
  id: string;

  productId: string;
  type: 'sale' | 'purchase' | 'adjustment';

  quantity: number;
  balanceAfter: number;

  reference?: string;

  createdAt: string;
}
