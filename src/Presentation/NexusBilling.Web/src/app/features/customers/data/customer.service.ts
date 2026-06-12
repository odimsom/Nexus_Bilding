import { inject, Injectable, signal } from '@angular/core';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { Customer, CustomerSortField } from '../domain/customer.model';
import { firstValueFrom } from 'rxjs';

export interface CustomerListItem {
  no: string;
  name: string;
  city: string;
  contact: string;
  blocked: boolean;
  salespersonCode: string;
  paymentTermsCode: string;
  balance: number;
  balanceDue: number;
}

export interface CustomerSearchParams {
  search?: string;
  blocked?: boolean | null;
  page?: number;
  pageSize?: number;
}

export interface CustomerFormData {
  no: string;
  name: string;
  address: string;
  city: string;
  contact: string;
  phoneNo: string;
  email: string;
  creditLimit: number;
  vatRegistrationNo: string;
  paymentTermsCode: string;
  paymentMethodCode: string;
  salespersonCode: string;
  currencyCode: string;
  customerPostingGroup: string;
  countryRegionCode: string;
}

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private readonly api = inject(ApiService);

  readonly items = signal<CustomerListItem[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);
  readonly totalItems = signal(0);
  readonly totalPages = signal(0);
  readonly currentPage = signal(1);

  async load(params: CustomerSearchParams = {}): Promise<void> {
    this.loading.set(true);
    this.error.set(null);
    try {
      const p: Record<string, string | number | boolean | undefined> = {
        page: params.page ?? 1,
        pageSize: params.pageSize ?? 100,
      };
      if (params.search) p['search'] = params.search;
      if (params.blocked !== undefined && params.blocked !== null) p['blocked'] = params.blocked;

      const result = await firstValueFrom(
        this.api.get<PagedData<CustomerListItem>>('sales/customers', p)
      );
      this.items.set(result.items);
      this.totalItems.set(result.pagination.totalItems);
      this.totalPages.set(result.pagination.totalPages);
      this.currentPage.set(result.pagination.page);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar clientes.');
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getByNo(no: string): Promise<Customer | null> {
    try {
      return await firstValueFrom(this.api.get<Customer>(`sales/customers/${encodeURIComponent(no)}`));
    } catch {
      return null;
    }
  }

  async create(data: CustomerFormData): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ no: string }>('sales/customers', data));
    return res.no;
  }

  async update(no: string, data: CustomerFormData): Promise<void> {
    await firstValueFrom(this.api.put<{ no: string }>(`sales/customers/${encodeURIComponent(no)}`, data));
  }

  async block(no: string): Promise<void> {
    await firstValueFrom(this.api.patch<{ blocked: boolean }>(`sales/customers/${encodeURIComponent(no)}/block`));
  }

  async unblock(no: string): Promise<void> {
    await firstValueFrom(this.api.patch<{ blocked: boolean }>(`sales/customers/${encodeURIComponent(no)}/unblock`));
  }

  sorted(sortField: CustomerSortField = 'name', sortAsc = true): CustomerListItem[] {
    return [...this.items()].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (sortField) {
        case 'no':        av = a.no;        bv = b.no;        break;
        case 'name':      av = a.name;      bv = b.name;      break;
        case 'city':      av = a.city;      bv = b.city;      break;
        case 'balance':   av = a.balance;   bv = b.balance;   break;
        case 'balanceDue':av = a.balanceDue;bv = b.balanceDue;break;
        default:          av = a.name;      bv = b.name;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }

  cities(): string[] {
    return [...new Set(this.items().map(c => c.city).filter(Boolean))].sort();
  }
}
