export interface Payment {
  id: string;

  fiscalDocumentId: string;

  amount: number;
  method: 'cash' | 'transfer' | 'card' | 'check';

  paidAt: string;
}
