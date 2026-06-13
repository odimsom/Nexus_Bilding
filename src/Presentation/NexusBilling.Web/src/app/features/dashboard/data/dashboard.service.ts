import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

export interface MonthlyTotal { month: string; total: number; }

export interface DashboardStats {
  totalCustomers: number;
  totalItems: number;
  openOrders: number;
  totalSalesThisMonth: number;
  totalSalesAllTime: number;
  totalVendors: number;
  openPurchaseOrders: number;
  totalPurchasesThisMonth: number;
  totalPurchasesAllTime: number;
  recentOrders: RecentOrder[];
  monthlySales: MonthlyTotal[];
}

export interface RecentOrder {
  no: string;
  documentType: string;
  customerName: string;
  postingDate: string;
  status: string;
  amountIncludingVat: number;
}

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private readonly api = inject(ApiService);

  readonly stats = signal<DashboardStats | null>(null);
  readonly loading = signal(false);

  async load(): Promise<void> {
    this.loading.set(true);
    try {
      const data = await firstValueFrom(this.api.get<DashboardStats>('dashboard/stats'));
      this.stats.set(data);
    } catch {
      this.stats.set(null);
    } finally {
      this.loading.set(false);
    }
  }
}
