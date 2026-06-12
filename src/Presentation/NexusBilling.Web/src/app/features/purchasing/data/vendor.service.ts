import { Injectable, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { Vendor, CreateVendorFormData } from '../domain/vendor.model';

@Injectable({ providedIn: 'root' })
export class VendorService {
  private readonly api = inject(ApiService);

  readonly items = signal<Vendor[]>([]);
  readonly loading = signal(false);
  readonly totalItems = signal(0);
  readonly error = signal('');

  async load(options?: { page?: number; pageSize?: number }): Promise<void> {
    this.loading.set(true);
    this.error.set('');
    try {
      const page = options?.page ?? 1;
      const size = options?.pageSize ?? 50;
      const res = await firstValueFrom(
        this.api.get<PagedData<Vendor>>(`purchasing/vendors?page=${page}&pageSize=${size}`)
      );
      this.items.set(res.items);
      this.totalItems.set(res.pagination.totalItems);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar proveedores.');
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async create(data: CreateVendorFormData): Promise<string> {
    const res = await firstValueFrom(
      this.api.post<{ data: { no: string } }>('purchasing/vendors', data)
    );
    return res.data.no;
  }
}
