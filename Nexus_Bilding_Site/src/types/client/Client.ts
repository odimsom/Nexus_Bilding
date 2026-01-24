import { ClientStatus } from '../shared/ClientStatus';

export interface Client {
  id: string;

  name: string;
  email?: string;
  phone?: string;

  taxId: string; // RNC o Cédula
  isCompany: boolean;

  address?: string;
  city?: string;

  status: ClientStatus;

  // Validación DGII
  verifiedAt?: string; // fecha en que se validó contra DGII

  notes?: string;

  createdAt: string;
}
