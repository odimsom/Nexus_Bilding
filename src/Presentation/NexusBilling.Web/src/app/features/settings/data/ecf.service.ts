import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

export interface EcfConfig {
  id?: string;
  rnc: string;
  representativeName: string;
  environment: number;
  p12Path?: string;
  isActive: boolean;
}

export interface NcfSequence {
  id: string;
  ncfType: string;
  currentNumber: number;
  maxNumber: number;
  expirationDate: string;
  available: number;
}

@Injectable({ providedIn: 'root' })
export class EcfService {
  private readonly api = inject(ApiService);

  readonly config = signal<EcfConfig | null>(null);
  readonly ncfSequences = signal<NcfSequence[]>([]);
  readonly loadingConfig = signal(false);
  readonly loadingNcf = signal(false);

  async loadConfig(): Promise<void> {
    this.loadingConfig.set(true);
    try {
      const result = await firstValueFrom(this.api.get<EcfConfig | null>('administration/ecf-config'));
      this.config.set(result);
    } catch { this.config.set(null); }
    finally { this.loadingConfig.set(false); }
  }

  async saveConfig(cfg: EcfConfig): Promise<void> {
    await firstValueFrom(this.api.put<null>('administration/ecf-config', {
      rnc: cfg.rnc,
      representativeName: cfg.representativeName,
      environment: cfg.environment,
      isActive: cfg.isActive
    }));
    await this.loadConfig();
  }

  async loadNcfSequences(): Promise<void> {
    this.loadingNcf.set(true);
    try {
      const result = await firstValueFrom(this.api.get<NcfSequence[]>('administration/ncf-sequences'));
      this.ncfSequences.set(result);
    } catch { this.ncfSequences.set([]); }
    finally { this.loadingNcf.set(false); }
  }

  async createNcfSequence(seq: { ncfType: string; currentNumber: number; maxNumber: number; expirationDate: string }): Promise<void> {
    await firstValueFrom(this.api.post<{ id: string; ncfType: string }>('administration/ncf-sequences', seq));
    await this.loadNcfSequences();
  }

  async updateNcfSequence(id: string, seq: { currentNumber: number; maxNumber: number; expirationDate: string; ncfType: string }): Promise<void> {
    await firstValueFrom(this.api.put<null>(`administration/ncf-sequences/${id}`, seq));
    await this.loadNcfSequences();
  }

  async deleteNcfSequence(id: string): Promise<void> {
    await firstValueFrom(this.api.delete<null>(`administration/ncf-sequences/${id}`));
    await this.loadNcfSequences();
  }
}
