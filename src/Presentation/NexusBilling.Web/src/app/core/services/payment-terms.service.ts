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
  private loaded = false;

  async load(): Promise<void> {
    if (this.loaded) return;
    try {
      const result = await firstValueFrom(this.api.get<PaymentTerm[]>('administration/payment-terms'));
      this.terms.set(result ?? FALLBACK);
      this.loaded = true;
    } catch {
      this.terms.set(FALLBACK);
    }
  }

  private async reload(): Promise<void> {
    this.loaded = false;
    await this.load();
  }

  async create(code: string, description: string): Promise<void> {
    await firstValueFrom(this.api.post<PaymentTerm>('administration/payment-terms', { code, description }));
    await this.reload();
  }

  async update(code: string, description: string): Promise<void> {
    await firstValueFrom(this.api.put<PaymentTerm>(`administration/payment-terms/${code}`, { code, description }));
    await this.reload();
  }

  async delete(code: string): Promise<void> {
    await firstValueFrom(this.api.delete(`administration/payment-terms/${code}`));
    await this.reload();
  }
}
