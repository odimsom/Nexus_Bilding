import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ItemService, ItemListItem, ItemFormData } from '../../../data/item.service';
import { ItemSortField } from '../../../domain/item.model';
import { ApiService } from '../../../../../core/services/api.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-item-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './item-list.page.html',
  styleUrl: './item-list.page.css'
})
export class ItemListPage implements OnInit {
  readonly svc    = inject(ItemService);
  private readonly excel = inject(ExcelExportService);
  readonly noSeriesMissing = signal(false);
  private readonly router = inject(Router);
  private readonly api = inject(ApiService);

  loadingNextNo = signal(false);
  previewNo = signal('');

  searchText = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'active';
  typeFilter = '';
  sortField: ItemSortField = 'no';
  sortAsc = true;

  showModal = signal(false);
  saving = signal(false);
  modalError = signal<string | null>(null);

  form: ItemFormData = this.emptyForm();

  private _searchTimeout?: ReturnType<typeof setTimeout>;

  visibleItems(): ItemListItem[] {
    const sorted = this.svc.sorted(this.sortField, this.sortAsc);
    if (!this.typeFilter) return sorted;
    return sorted.filter(i => i.type === this.typeFilter);
  }

  sorted(): ItemListItem[] { return this.svc.sorted(this.sortField, this.sortAsc); }

  ngOnInit(): void { this.reload(); }

  reload(): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked });
  }

  onSearchChange(): void {
    clearTimeout(this._searchTimeout);
    this._searchTimeout = setTimeout(() => this.reload(), 350);
  }

  onFilterChange(): void { this.reload(); }
  applyTypeFilter(): void { /* client-side filter via visibleItems() */ }

  setSort(f: ItemSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: ItemSortField): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.showBlocked = 'active';
    this.typeFilter = '';
    this.reload();
  }

  goToPage(page: number): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked, page });
  }

  typeLabel(t: string): string {
    return { Inventory: 'Inventario', Service: 'Servicio', 'Non-Inventory': 'No Inv.' }[t] ?? t ?? '—';
  }

  typeBadge(t: string): string {
    if (t === 'Service') return 'nx-badge nx-badge--info';
    if (t === 'Non-Inventory') return 'nx-badge nx-badge--warn';
    return 'nx-badge nx-badge--outline';
  }

  openModal(): void {
    this.form = this.emptyForm();
    this.modalError.set(null);
    this.noSeriesMissing.set(false);
    this.previewNo.set('');
    this.showModal.set(true);
    this.fetchNextNo();
  }

  async fetchNextNo(): Promise<void> {
    this.loadingNextNo.set(true);
    try {
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>('administration/no-series/ITEM/next'));
      this.previewNo.set(res.nextNo);
      this.form.no = res.nextNo;
    } catch {
      this.previewNo.set('');
      this.form.no = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  goConfigureSeries(): void {
    this.closeModal();
    this.router.navigate(['/settings'], { queryParams: { tab: 'sequences', returnTo: '/inventory' } });
  }

  closeModal(): void { this.showModal.set(false); }

  async saveItem(): Promise<void> {
    if (!this.form.description?.trim()) {
      this.modalError.set('La descripción del producto es obligatoria.');
      return;
    }
    this.saving.set(true);
    this.modalError.set(null);
    this.noSeriesMissing.set(false);
    try {
      const no = await this.svc.create(this.form);
      this.closeModal();
      this.router.navigate(['/inventory', no]);
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? e?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('series')) {
        this.noSeriesMissing.set(true);
        this.modalError.set('No hay una secuencia de numeración configurada para productos.');
      } else if (msg.toLowerCase().includes('already') || msg.toLowerCase().includes('ya existe') || msg.toLowerCase().includes('duplicate')) {
        this.modalError.set('El número de producto ya está en uso. Haz clic en actualizar para obtener uno disponible.');
        this.fetchNextNo();
      } else if (msg) {
        this.modalError.set(msg);
      } else {
        this.modalError.set('Ocurrió un error al guardar el producto. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }

  async exportExcel(): Promise<void> {
    const items = this.svc.sorted(this.sortField, this.sortAsc);
    await this.excel.exportAsExcel({
      filename: 'Inventario',
      title: 'Lista de Productos',
      subtitle: 'Nexus Billing - Módulo de Inventario',
      sheetName: 'Productos',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Descripción', key: 'desc', width: 40 },
        { header: 'Tipo', key: 'tipo', width: 14 },
        { header: 'U/M', key: 'um', width: 10 },
        { header: 'Precio', key: 'precio', width: 14, numFmt: '#,##0.00' },
        { header: 'Costo', key: 'costo', width: 14, numFmt: '#,##0.00' },
        { header: 'Inventario', key: 'inv', width: 12, numFmt: '#,##0.00' },
        { header: 'Estado', key: 'estado', width: 12 },
      ],
      data: items.map(i => ({
        no: i.no,
        desc: i.description,
        tipo: i.type,
        um: i.baseUnitOfMeasure,
        precio: i.unitPrice,
        costo: i.unitCost,
        inv: i.inventory,
        estado: i.blocked ? 'Bloqueado' : 'Activo',
      })),
    });
  }

  private emptyForm(): ItemFormData {
    return {
      no: '', description: '', description2: '', baseUnitOfMeasure: 'UND',
      unitPrice: 0, unitCost: 0, standardCost: 0, type: 'Inventory',
      itemCategoryCode: '', inventoryPostingGroup: 'FINISHED',
      genProdPostingGroup: 'RETAIL', vatProdPostingGroup: '',
      vendorNo: '', vendorItemNo: ''
    };
  }
}
