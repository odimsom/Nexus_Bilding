import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

export interface NoSeriesItem {
  code: string;
  description: string;
  defaultNos: boolean;
  manualNos: boolean;
  startingNo: string | null;
  endingNo: string | null;
  lastNoUsed: string | null;
  incrementByNo: number;
  open: boolean;
}

@Injectable({ providedIn: 'root' })
export class NoSeriesService {
  private readonly api = inject(ApiService);

  readonly items = signal<NoSeriesItem[]>([]);
  readonly loading = signal(false);

  async load(): Promise<void> {
    this.loading.set(true);
    try {
      const result = await firstValueFrom(this.api.get<NoSeriesItem[]>('administration/no-series'));
      this.items.set(result);
    } catch { this.items.set([]); }
    finally { this.loading.set(false); }
  }

  async save(item: NoSeriesItem): Promise<void> {
    await firstValueFrom(this.api.post<{ code: string }>('administration/no-series', {
      code: item.code, description: item.description, defaultNos: item.defaultNos, manualNos: item.manualNos
    }));
    if (item.startingNo) {
      await firstValueFrom(this.api.post<{ seriesCode: string }>('administration/no-series/lines', {
        seriesCode: item.code,
        startingNo: item.startingNo,
        endingNo: item.endingNo ?? '',
        incrementByNo: item.incrementByNo
      }));
    }
    await this.load();
  }

  async delete(code: string): Promise<void> {
    await firstValueFrom(this.api.delete<{ code: string }>(`administration/no-series/${encodeURIComponent(code)}`));
    await this.load();
  }
}
