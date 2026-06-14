import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ItemService, ItemFormData, LedgerEntry } from '../../../data/item.service';
import { Item } from '../../../domain/item.model';

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './item-card.page.html',
  styleUrl: './item-card.page.css'
})
export class ItemCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(ItemService);

  item = signal<Item | null>(null);
  loading = signal(true);
  actionLoading = signal(false);
  showEdit = signal(false);
  saving = signal(false);
  editError = signal<string | null>(null);
  form: ItemFormData = {} as ItemFormData;

  // Ajuste de inventario
  showAdjust   = signal(false);
  adjustSaving = signal(false);
  adjustError  = signal('');
  adjustForm   = { quantity: 0, documentNo: '', description: '' };

  // Diario de producto
  showLedger    = signal(false);
  ledgerLoading = signal(false);
  ledgerEntries = signal<LedgerEntry[]>([]);
  ledgerTotal   = signal(0);

  margen(): number {
    const i = this.item();
    if (!i || i.unitPrice <= 0) return 0;
    return (i.unitPrice - i.unitCost) / i.unitPrice * 100;
  }

  typeLabel(t: string): string {
    return { Inventory: 'Inventario', Service: 'Servicio', 'Non-Inventory': 'No Inventario' }[t] ?? t ?? '—';
  }

  typeBadge(t: string): string {
    if (t === 'Service') return 'nx-badge nx-badge--info';
    if (t === 'Non-Inventory') return 'nx-badge nx-badge--warn';
    return 'nx-badge nx-badge--outline';
  }

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const item = await this.svc.getByNo(no);
    this.item.set(item);
    this.loading.set(false);
  }

  openEdit(): void {
    const i = this.item()!;
    this.form = {
      no: i.no, description: i.description, description2: i.description2 ?? '',
      baseUnitOfMeasure: i.baseUnitOfMeasure, unitPrice: i.unitPrice,
      unitCost: i.unitCost, standardCost: i.standardCost ?? 0,
      type: i.type ?? 'Inventory', itemCategoryCode: i.itemCategoryCode ?? '',
      inventoryPostingGroup: i.inventoryPostingGroup ?? '',
      genProdPostingGroup: i.genProdPostingGroup ?? '',
      vatProdPostingGroup: i.vatProdPostingGroup ?? '',
      vendorNo: i.vendorNo ?? '', vendorItemNo: i.vendorItemNo ?? ''
    };
    this.editError.set(null);
    this.showEdit.set(true);
  }

  closeEdit(): void { this.showEdit.set(false); }

  async toggleBlock(): Promise<void> {
    const i = this.item();
    if (!i) return;
    this.actionLoading.set(true);
    try {
      if (i.blocked) await this.svc.unblock(i.no);
      else await this.svc.block(i.no);
      const updated = await this.svc.getByNo(i.no);
      this.item.set(updated);
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al cambiar el estado del producto.');
    } finally {
      this.actionLoading.set(false);
    }
  }

  openAdjust(): void {
    this.adjustForm = { quantity: 0, documentNo: '', description: '' };
    this.adjustError.set('');
    this.showAdjust.set(true);
  }

  closeAdjust(): void { this.showAdjust.set(false); }

  async saveAdjust(): Promise<void> {
    if (!this.adjustForm.quantity) { this.adjustError.set('La cantidad es obligatoria y no puede ser cero.'); return; }
    this.adjustSaving.set(true);
    this.adjustError.set('');
    try {
      await this.svc.adjustInventory(
        this.item()!.no,
        this.adjustForm.quantity,
        this.adjustForm.documentNo,
        this.adjustForm.description || 'Ajuste manual'
      );
      const updated = await this.svc.getByNo(this.item()!.no);
      this.item.set(updated);
      this.showAdjust.set(false);
    } catch (e: any) {
      this.adjustError.set(e?.error?.error?.message ?? 'Error al registrar el ajuste.');
    } finally {
      this.adjustSaving.set(false);
    }
  }

  async openLedger(): Promise<void> {
    this.showLedger.set(true);
    this.ledgerLoading.set(true);
    try {
      const result = await this.svc.getLedger(this.item()!.no);
      this.ledgerEntries.set(result.items);
      this.ledgerTotal.set(result.totalCount);
    } catch {
      this.ledgerEntries.set([]);
    } finally {
      this.ledgerLoading.set(false);
    }
  }

  closeLedger(): void { this.showLedger.set(false); }

  async saveEdit(): Promise<void> {
    if (!this.form.description?.trim()) {
      this.editError.set('La descripción es obligatoria.');
      return;
    }
    this.saving.set(true);
    this.editError.set(null);
    try {
      await this.svc.update(this.item()!.no, this.form);
      const updated = await this.svc.getByNo(this.item()!.no);
      this.item.set(updated);
      this.closeEdit();
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.saving.set(false);
    }
  }
}
