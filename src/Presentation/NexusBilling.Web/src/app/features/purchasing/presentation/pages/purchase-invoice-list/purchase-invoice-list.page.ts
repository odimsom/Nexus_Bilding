import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';
import { sortBy } from '../../../../../shared/utils/sort.utils';

@Component({
  selector: 'app-purchase-invoice-list',
  standalone: true,
  imports: [RouterLink, DatePipe, CurrencyPipe, FormsModule],
  templateUrl: './purchase-invoice-list.page.html',
  styleUrl: './purchase-invoice-list.page.css'
})
export class PurchaseInvoiceListPage implements OnInit {
  readonly svc = inject(PurchaseInvoiceService);

  sortField = 'postingDate';
  sortAsc = false;
  searchText = '';
  filteredItems = signal<any[]>([]);

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
      this.filteredItems.set(all.filter(inv =>
        (inv.no ?? '').toLowerCase().includes(q) ||
        (inv.buyFromVendorNo ?? '').toLowerCase().includes(q) ||
        (inv.payToName ?? '').toLowerCase().includes(q)
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
}
