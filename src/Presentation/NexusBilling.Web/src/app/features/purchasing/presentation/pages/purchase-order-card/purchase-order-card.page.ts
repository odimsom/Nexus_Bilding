import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseOrderService } from '../../../data/purchase.service';
import { PurchaseOrder } from '../../../domain/purchase.model';
import { VendorService } from '../../../data/vendor.service';
import { Vendor } from '../../../domain/vendor.model';
import { ItemService, ItemListItem } from '../../../../inventory/data/item.service';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { CurrencyService } from '../../../../../core/services/currency.service';
import { PdfService, PurchaseOrderPdfData } from '../../../../../shared/services/pdf.service';

interface NewLine {
  lineType: string;
  itemNo: string;
  description: string;
  unitOfMeasure: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  itemSugg: ItemListItem[];
  showItemDrop: boolean;
  notInInventory: boolean;
}

@Component({
  selector: 'app-purchase-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './purchase-order-card.page.html',
  styleUrl: './purchase-order-card.page.css'
})
export class PurchaseOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(PurchaseOrderService);
  private readonly vendorSvc = inject(VendorService);
  private readonly itemSvc = inject(ItemService);
  private readonly pdfSvc = inject(PdfService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly currencySvc = inject(CurrencyService);

  loading = signal(true);
  saving = signal(false);
  savingLines = signal(false);
  printing = signal(false);
  errorMsg = signal<string | null>(null);
  editingLines = signal(false);

  showEditModal = signal(false);
  editSaving = signal(false);
  editError = signal('');
  editForm = {
    dueDate: '',
    currencyCode: '',
    paymentTermsCode: '',
    externalDocumentNo: '',
  };

  order = signal<PurchaseOrder | null>(null);

  newLines = signal<NewLine[]>([]);

  subtotal = computed(() => {
    if (this.editingLines()) {
      return this.newLines().reduce((s, l) => s + this.lineTotal(l), 0);
    }
    return this.order()?.amount || 0;
  });
  totalVat = computed(() => {
    if (this.editingLines()) {
      return this.subtotal() * 0.18;
    }
    const o = this.order();
    return o ? o.amountIncludingVat - o.amount : 0;
  });
  totalWithVat = computed(() => {
    if (this.editingLines()) {
      return this.subtotal() * 1.18;
    }
    return this.order()?.amountIncludingVat || 0;
  });

  async ngOnInit() {
    this.paymentTermsSvc.load();
    this.currencySvc.load();
    const no = this.route.snapshot.paramMap.get('no');
    if (no === 'new') {
      this.router.navigate(['/purchases'], { replaceUrl: true });
      return;
    }
    if (no) {
      this.loading.set(true);
      const res = await this.svc.getByNo(no);
      this.order.set(res);
      this.loading.set(false);
    }
  }

  lineTotal(line: NewLine): number {
    const base = line.quantity * line.unitPrice;
    const disc = base * (line.lineDiscountPct / 100);
    return base - disc;
  }

  startEditLines(): void {
    if (this.itemSvc.items().length === 0) {
      this.itemSvc.load({ pageSize: 500 });
    }
    const lines = (this.order()?.lines ?? []).map(l => ({
      lineType: 'Item',
      itemNo: l.itemNo,
      description: l.description,
      unitOfMeasure: l.unitOfMeasure,
      quantity: l.quantity,
      unitPrice: l.unitPrice,
      lineDiscountPct: l.lineDiscountPct,
      itemSugg: [] as ItemListItem[],
      showItemDrop: false,
      notInInventory: false,
    }));
    this.newLines.set(lines);
    this.editingLines.set(true);
  }

  cancelEditLines(): void {
    this.editingLines.set(false);
    this.newLines.set([]);
    this.errorMsg.set(null);
  }

  addLine(): void {
    this.newLines.update(lines => [...lines, {
      lineType: 'Item', itemNo: '', description: '',
      unitOfMeasure: 'UND', quantity: 1, unitPrice: 0, lineDiscountPct: 0,
      itemSugg: [], showItemDrop: false, notInInventory: false
    }]);
  }

  onItemFocus(line: NewLine): void {
    this.filterItems(line);
    line.showItemDrop = true;
  }

  onItemInput(line: NewLine): void {
    this.filterItems(line);
    line.showItemDrop = true;
    line.notInInventory = false;
  }

  onItemBlur(line: NewLine): void {
    setTimeout(() => {
      line.showItemDrop = false;
      if (line.itemNo) {
        const exists = this.itemSvc.items().some(it => it.no.toLowerCase() === line.itemNo.toLowerCase());
        line.notInInventory = !exists;
      } else {
        line.notInInventory = false;
      }
    }, 150);
  }

  private filterItems(line: NewLine): void {
    const q = line.itemNo.toLowerCase().trim();
    const all = this.itemSvc.items();
    line.itemSugg = q
      ? all.filter(it => it.no.toLowerCase().includes(q) || it.description.toLowerCase().includes(q)).slice(0, 8)
      : all.slice(0, 8);
  }

  selectItem(line: NewLine, item: ItemListItem): void {
    line.itemNo = item.no;
    line.description = item.description;
    line.unitOfMeasure = item.baseUnitOfMeasure || 'UND';
    line.unitPrice = item.unitCost;
    line.showItemDrop = false;
    line.notInInventory = false;
    line.itemSugg = [];
  }

  async registerItem(line: NewLine): Promise<void> {
    try {
      await this.itemSvc.create({
        no: line.itemNo,
        description: line.description || line.itemNo,
        description2: '',
        baseUnitOfMeasure: line.unitOfMeasure || 'UND',
        unitPrice: 0,
        unitCost: line.unitPrice,
        standardCost: line.unitPrice,
        type: 'Inventory',
        itemCategoryCode: '',
        inventoryPostingGroup: '',
        genProdPostingGroup: '',
        vatProdPostingGroup: '',
        vendorNo: this.order()?.vendorNo || '',
        vendorItemNo: line.itemNo,
      });
      line.notInInventory = false;
      await this.itemSvc.load({ pageSize: 500 });
    } catch { /* ignore */ }
  }

  removeLine(i: number): void {
    this.newLines.update(lines => lines.filter((_, idx) => idx !== i));
  }

  async saveLines(): Promise<void> {
    const no = this.order()?.no;
    if (!no) return;
    this.savingLines.set(true);
    this.errorMsg.set(null);
    try {
      const result = await this.svc.updateLines(no, this.newLines().map(l => ({
        itemNo: l.itemNo, description: l.description,
        quantity: l.quantity, unitPrice: l.unitPrice,
        lineDiscountPct: l.lineDiscountPct, unitOfMeasure: l.unitOfMeasure,
        lineType: l.lineType
      })));
      const updated = await this.svc.getByNo(no);
      this.order.set(updated);
      this.editingLines.set(false);
      this.newLines.set([]);
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Error al guardar las líneas.');
    } finally {
      this.savingLines.set(false);
    }
  }

  openEditHeader(): void {
    const o = this.order();
    if (!o) return;
    const toDate = (v: string | null | undefined) =>
      v ? new Date(v).toISOString().split('T')[0] : '';
    this.editForm = {
      dueDate: toDate(o.dueDate),
      currencyCode: o.currencyCode ?? '',
      paymentTermsCode: o.paymentTermsCode ?? '',
      externalDocumentNo: o.externalDocumentNo ?? '',
    };
    this.editError.set('');
    this.showEditModal.set(true);
  }

  async saveEditHeader(): Promise<void> {
    const o = this.order();
    if (!o || this.editSaving()) return;
    this.editSaving.set(true);
    this.editError.set('');
    try {
      await this.svc.updateHeader(o.no, {
        dueDate: this.editForm.dueDate || null,
        currencyCode: this.editForm.currencyCode,
        paymentTermsCode: this.editForm.paymentTermsCode,
        externalDocumentNo: this.editForm.externalDocumentNo || null,
      });
      this.showEditModal.set(false);
      this.order.set(await this.svc.getByNo(o.no));
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.editSaving.set(false);
    }
  }

  async postOrder() {
    if (!this.order()) return;
    this.saving.set(true);
    this.errorMsg.set(null);
    try {
      const result = await this.svc.post(this.order()!.no);
      this.router.navigate(['/purchase-invoices', result.invoiceNo]);
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Ocurrió un error al contabilizar el pedido.');
    } finally {
      this.saving.set(false);
    }
  }

  async printPdf(): Promise<void> {
    const o = this.order();
    if (!o || this.printing()) return;
    this.printing.set(true);
    try {
      const pdfData: PurchaseOrderPdfData = {
        no: o.no,
        vendorName: o.vendorName,
        vendorNo: o.vendorNo,
        externalDocumentNo: o.externalDocumentNo,
        postingDate: o.postingDate,
        dueDate: o.dueDate ?? null,
        paymentTermsCode: o.paymentTermsCode,
        currencyCode: o.currencyCode || 'DOP',
        status: o.status,
        lines: (o.lines ?? []).map(l => ({
          lineNo: l.lineNo,
          itemNo: l.itemNo,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          amount: l.amount,
          amountIncludingVat: l.amountIncludingVat,
          unitOfMeasure: l.unitOfMeasure,
        })),
        amount: o.amount,
        amountIncludingVat: o.amountIncludingVat,
      };
      await this.pdfSvc.printPurchaseOrder(pdfData);
    } finally {
      this.printing.set(false);
    }
  }
}
