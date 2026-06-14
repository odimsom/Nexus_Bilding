import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from './api.service';

export interface PaymentMethodOption { code: string; description: string; }

@Injectable({ providedIn: 'root' })
export class PaymentMethodService {
  private readonly api = inject(ApiService);

  readonly methods = signal<PaymentMethodOption[]>([]);
  private loaded = false;

  async load(): Promise<void> {
    if (this.loaded) return;
    try {
      const data = await firstValueFrom(this.api.get<PaymentMethodOption[]>('sales/payment-methods'));
      this.methods.set(data);
      this.loaded = true;
    } catch {
      this.methods.set([
        { code: 'EFE',  description: 'Efectivo'               },
        { code: 'TRF',  description: 'Transferencia Bancaria' },
        { code: 'CHQ',  description: 'Cheque'                 },
        { code: 'TC',   description: 'Tarjeta de Crédito'     },
        { code: 'TD',   description: 'Tarjeta de Débito'      },
        { code: 'CRED', description: 'Crédito'                },
      ]);
    }
  }

  private async reload(): Promise<void> {
    this.loaded = false;
    await this.load();
  }

  async create(code: string, description: string): Promise<void> {
    await firstValueFrom(this.api.post<PaymentMethodOption>('sales/payment-methods', { code, description }));
    await this.reload();
  }

  async update(code: string, description: string): Promise<void> {
    await firstValueFrom(this.api.put<PaymentMethodOption>(`sales/payment-methods/${code}`, { code, description }));
    await this.reload();
  }

  async delete(code: string): Promise<void> {
    await firstValueFrom(this.api.delete(`sales/payment-methods/${code}`));
    await this.reload();
  }
}
