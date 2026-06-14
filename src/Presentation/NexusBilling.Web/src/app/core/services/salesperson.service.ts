import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from './api.service';

export interface SalespersonOption { code: string; name: string; email?: string; phone?: string; jobTitle?: string; }

@Injectable({ providedIn: 'root' })
export class SalespersonService {
  private readonly api = inject(ApiService);

  readonly salespersons = signal<SalespersonOption[]>([]);
  private loaded = false;

  async load(): Promise<void> {
    if (this.loaded) return;
    try {
      const data = await firstValueFrom(this.api.get<SalespersonOption[]>('sales/salespersons'));
      this.salespersons.set(data);
      this.loaded = true;
    } catch {
      this.salespersons.set([]);
    }
  }

  private async reload(): Promise<void> {
    this.loaded = false;
    await this.load();
  }

  async create(code: string, name: string, email?: string, phone?: string, jobTitle?: string): Promise<void> {
    await firstValueFrom(this.api.post<SalespersonOption>('sales/salespersons', { code, name, email, phone, jobTitle }));
    await this.reload();
  }

  async update(code: string, name: string, email?: string, phone?: string, jobTitle?: string): Promise<void> {
    await firstValueFrom(this.api.put<SalespersonOption>(`sales/salespersons/${code}`, { code, name, email, phone, jobTitle }));
    await this.reload();
  }

  async delete(code: string): Promise<void> {
    await firstValueFrom(this.api.delete(`sales/salespersons/${code}`));
    await this.reload();
  }
}
