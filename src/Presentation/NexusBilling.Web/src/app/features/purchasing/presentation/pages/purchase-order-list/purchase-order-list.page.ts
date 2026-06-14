import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseOrderService } from '../../../data/purchase.service';
import { sortBy } from '../../../../../shared/utils/sort.utils';
import { CreatePurchaseOrderModalComponent } from '../../components/create-purchase-order-modal/create-purchase-order-modal.component';

@Component({
  selector: 'app-purchase-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, CreatePurchaseOrderModalComponent],
  templateUrl: './purchase-order-list.page.html',
  styleUrl: './purchase-order-list.page.css'
})
export class PurchaseOrderListPage implements OnInit {
  readonly svc = inject(PurchaseOrderService);
  private readonly router = inject(Router);

  sortField = 'postingDate';
  sortAsc = false;
  searchText = '';
  filteredItems = signal<any[]>([]);
  showNewOrderModal = signal(false);

  async ngOnInit() {
    await this.svc.load();
    this.applyFilter();
  }

  applyFilter(): void {
    const q = this.searchText.toLowerCase().trim();
    const all = this.svc.items();
    if (!q) {
      this.filteredItems.set(all);
    } else {
      this.filteredItems.set(all.filter(o =>
        o.no.toLowerCase().includes(q) ||
        (o.vendorName ?? '').toLowerCase().includes(q) ||
        (o.vendorNo ?? '').toLowerCase().includes(q) ||
        (o.status ?? '').toLowerCase().includes(q)
      ));
    }
  }

  sorted(): any[] {
    return sortBy(this.filteredItems(), this.sortField, this.sortAsc);
  }

  setSort(f: string): void {
    if (this.sortField === f) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortField = f;
      this.sortAsc = true;
    }
  }

  si(f: string): string {
    if (this.sortField !== f) return '';
    return this.sortAsc ? '↑' : '↓';
  }

  createNew() {
    this.showNewOrderModal.set(true);
  }

  async onOrderCreated(): Promise<void> {
    this.showNewOrderModal.set(false);
    await this.svc.load();
    this.applyFilter();
  }
}
