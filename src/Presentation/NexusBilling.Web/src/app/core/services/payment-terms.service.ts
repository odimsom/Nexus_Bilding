import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from './api.service';

export interface PaymentTerm {
  code: string;
  description: string;
}

const FALLBACK: PaymentTerm[] = [
  { code: 'CM',     description: 'Contado' },
  { code: '15D',    description: '15 Días' },
  { code: '30D',    description: '30 Días' },
  { code: '60D',    description: '60 Días' },
  { code: '90D',    description: '90 Días' },
  { code: 'CUOTAS', description: 'Cuotas' },
];

@Injectable({ providedIn: 'root' })
export class PaymentTermsService {
  private readonly api = inject(ApiService);

  terms = signal<PaymentTerm[]>([]);

  async load(): Promise<void> {
    if (this.terms().length > 0) return;
    try {
      const result = await firstValueFrom(this.api.get<PaymentTerm[]>('administration/payment-terms'));
      this.terms.set(result ?? FALLBACK);
    } catch {
      this.terms.set(FALLBACK);
    }
  }
}
