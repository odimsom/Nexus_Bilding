import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';
import { JournalLine } from '../domain/journal-line.model';

@Injectable({ providedIn: 'root' })
export class JournalService {
  private readonly api = inject(ApiService);

  readonly lines = signal<JournalLine[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  async load(template: string = 'GENERAL', batch: string = 'DEFAULT') {
    this.loading.set(true);
    this.error.set(null);
    try {
      const res = await firstValueFrom(this.api.get<{data: JournalLine[]}>(`finance/journal?templateName=${template}&batchName=${batch}`));
      this.lines.set(res.data || []);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'No se pudo cargar el diario.');
    } finally {
      this.loading.set(false);
    }
  }

  async createLine(payload: any) {
    this.loading.set(true);
    this.error.set(null);
    try {
      await firstValueFrom(this.api.post('finance/journal', payload));
      await this.load(payload.journalTemplateName, payload.journalBatchName);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'No se pudo crear la línea.');
    } finally {
      this.loading.set(false);
    }
  }

  async deleteLine(id: string) {
    this.loading.set(true);
    this.error.set(null);
    try {
      await firstValueFrom(this.api.delete(`finance/journal/${id}`));
      // We assume it's still GENERAL/DEFAULT for now
      await this.load();
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al eliminar línea.');
    } finally {
      this.loading.set(false);
    }
  }

  async postBatch(template: string = 'GENERAL', batch: string = 'DEFAULT') {
    this.loading.set(true);
    this.error.set(null);
    try {
      const res = await firstValueFrom(this.api.post<{data: {postedEntries: number}}>(`finance/journal/post-batch?templateName=${template}&batchName=${batch}`, {}));
      await this.load(template, batch);
      return res.data?.postedEntries || 0;
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al registrar el diario.');
      this.loading.set(false);
      throw e;
    }
  }
}
