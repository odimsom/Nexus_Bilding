import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from './api.service';

export interface CurrencyOption { code: string; description: string; symbol: string; }

@Injectable({ providedIn: 'root' })
export class CurrencyService {
  private readonly api = inject(ApiService);

  readonly currencies = signal<CurrencyOption[]>([]);
  private loaded = false;

  async load(): Promise<void> {
    if (this.loaded) return;
    try {
      const data = await firstValueFrom(this.api.get<CurrencyOption[]>('finance/currencies'));
      this.currencies.set(data);
      this.loaded = true;
    } catch {
      this.currencies.set([
        { code: 'DOP', description: 'Peso Dominicano', symbol: 'RD$' },
        { code: 'USD', description: 'Dólar Americano', symbol: '$' },
        { code: 'EUR', description: 'Euro',            symbol: '€' },
      ]);
    }
  }

  private async reload(): Promise<void> {
    this.loaded = false;
    await this.load();
  }

  async create(code: string, description: string, symbol: string): Promise<void> {
    await firstValueFrom(this.api.post<CurrencyOption>('finance/currencies', { code, description, symbol }));
    await this.reload();
  }

  async update(code: string, description: string, symbol: string): Promise<void> {
    await firstValueFrom(this.api.put<CurrencyOption>(`finance/currencies/${code}`, { code, description, symbol }));
    await this.reload();
  }

  async delete(code: string): Promise<void> {
    await firstValueFrom(this.api.delete(`finance/currencies/${code}`));
    await this.reload();
  }
}
