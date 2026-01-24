import { FiscalDocumentType } from '../shared/FiscalDocumentType';
import { FiscalDocumentStatus } from '../shared/FiscalDocumentStatus';
import { FiscalDocumentItem } from './FiscalDocumentItem';

export interface FiscalDocument {
  id: string;

  documentType: FiscalDocumentType;
  eCFNumber?: string; // asignado tras firma

  clientId?: string; // opcional para E41
  clientName: string;
  clientTaxId?: string;

  issueDate: string;
  dueDate?: string;

  items: FiscalDocumentItem[];

  subtotal: number;
  totalITBIS: number;
  totalDiscount: number;
  totalAmount: number;

  status: FiscalDocumentStatus;

  // Cumplimiento Ley 126-22
  securityCode?: string; // 64 caracteres
  xmlUrl?: string;       // XML firmado (legal)
  qrCodeContent?: string;
  signedAt?: string;

  notes?: string;

  createdAt: string;
}
