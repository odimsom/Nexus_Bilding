import { inject, Injectable, signal } from '@angular/core';
import { ApiService, PagedData } from '../../../core/services/api.service';
import { Item, ItemSortField } from '../domain/item.model';
import { firstValueFrom } from 'rxjs';

export interface LedgerEntry {
  entryNo: number;
  postingDate: string;
  entryTypeLabel: string;
  entryType: number;
  documentNo: string;
  description: string;
  quantity: number;
  remainingQuantity: number;
  unitOfMeasureCode: string;
  positive: boolean;
}

export interface LedgerResult {
  items: LedgerEntry[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export interface ItemListItem {
  no: string;
  description: string;
  baseUnitOfMeasure: string;
  unitPrice: number;
  unitCost: number;
  blocked: boolean;
  inventory: number;
  type: string;
  itemCategoryCode: string;
}

export interface ItemSearchParams {
  search?: string;
  blocked?: boolean | null;
  page?: number;
  pageSize?: number;
}

export interface ItemFormData {
  no: string;
  description: string;
  description2: string;
  baseUnitOfMeasure: string;
  unitPrice: number;
  unitCost: number;
  standardCost: number;
  type: string;
  itemCategoryCode: string;
  inventoryPostingGroup: string;
  genProdPostingGroup: string;
  vatProdPostingGroup: string;
  vendorNo: string;
  vendorItemNo: string;
}

@Injectable({ providedIn: 'root' })
export class ItemService {
  private readonly api = inject(ApiService);

  readonly items = signal<ItemListItem[]>([]);
  readonly loading = signal(false);
  readonly error = signal<string | null>(null);
  readonly totalItems = signal(0);
  readonly totalPages = signal(0);
  readonly currentPage = signal(1);

  async load(params: ItemSearchParams = {}): Promise<void> {
    this.loading.set(true);
    this.error.set(null);
    try {
      const p: Record<string, string | number | boolean | undefined> = {
        page: params.page ?? 1,
        pageSize: params.pageSize ?? 200,
      };
      if (params.search) p['search'] = params.search;
      if (params.blocked !== undefined && params.blocked !== null) p['blocked'] = params.blocked;

      const result = await firstValueFrom(
        this.api.get<PagedData<ItemListItem>>('inventory/items', p)
      );
      this.items.set(result.items);
      this.totalItems.set(result.pagination.totalItems);
      this.totalPages.set(result.pagination.totalPages);
      this.currentPage.set(result.pagination.page);
    } catch (e: any) {
      this.error.set(e?.error?.error?.message ?? 'Error al cargar productos.');
      this.items.set([]);
    } finally {
      this.loading.set(false);
    }
  }

  async getByNo(no: string): Promise<Item | null> {
    try {
      return await firstValueFrom(this.api.get<Item>(`inventory/items/${encodeURIComponent(no)}`));
    } catch {
      return null;
    }
  }

  async create(data: ItemFormData): Promise<string> {
    const res = await firstValueFrom(this.api.post<{ no: string }>('inventory/items', data));
    return res.no;
  }

  async update(no: string, data: ItemFormData): Promise<void> {
    await firstValueFrom(this.api.put<{ no: string }>(`inventory/items/${encodeURIComponent(no)}`, data));
  }

  async block(no: string): Promise<void> {
    await firstValueFrom(this.api.patch<{ blocked: boolean }>(`inventory/items/${encodeURIComponent(no)}/block`));
  }

  async unblock(no: string): Promise<void> {
    await firstValueFrom(this.api.patch<{ blocked: boolean }>(`inventory/items/${encodeURIComponent(no)}/unblock`));
  }

  async adjustInventory(no: string, quantity: number, documentNo: string, description: string): Promise<number> {
    const res = await firstValueFrom(this.api.post<{ entryNo: number }>(
      `inventory/items/${encodeURIComponent(no)}/adjust`,
      { quantity, documentNo, description }
    ));
    return res.entryNo;
  }

  async getLedger(no: string, page = 1, pageSize = 50): Promise<LedgerResult> {
    return firstValueFrom(this.api.get<LedgerResult>(`inventory/items/${encodeURIComponent(no)}/ledger`, { page, pageSize }));
  }

  sorted(sortField: ItemSortField = 'no', sortAsc = true): ItemListItem[] {
    return [...this.items()].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (sortField) {
        case 'no':          av = a.no;          bv = b.no;          break;
        case 'description': av = a.description; bv = b.description; break;
        case 'unitPrice':   av = a.unitPrice;   bv = b.unitPrice;   break;
        case 'unitCost':    av = a.unitCost;    bv = b.unitCost;    break;
        case 'inventory':   av = a.inventory;   bv = b.inventory;   break;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }
}
