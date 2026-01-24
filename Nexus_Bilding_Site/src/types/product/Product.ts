import { ProductStatus } from '../shared/ProductStatus';
import { ITBISRate } from '../shared/ITBISRate';

export interface Product {
  id: string;

  name: string;
  description?: string;

  sku?: string;
  category?: string;

  price: number;
  cost?: number;

  itbisRate: ITBISRate;

  exemptionReason?: string; // obligatorio si itbisRate === 0

  stock?: number;
  lowStockThreshold?: number;

  status: ProductStatus;

  createdAt: string;
}
