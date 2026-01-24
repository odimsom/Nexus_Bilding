import { ITBISRate } from '../shared/ITBISRate';

export interface FiscalDocumentItem {
  productId?: string; // opcional para E41
  description: string;

  quantity: number;
  unitPrice: number;

  itbisRate: ITBISRate;
  itbisAmount: number;

  exemptionReason?: string;

  discount?: number;
  total: number;
}
