import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';
import { VendorService } from '../../../data/vendor.service';
import { PurchaseInvoiceDetail, CreateInvoiceLineForm } from '../../../domain/purchase-invoice.model';
import { Vendor } from '../../../domain/vendor.model';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';

@Component({
  selector: 'app-purchase-invoice-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, CurrencyPipe, DatePipe],
  templateUrl: './purchase-invoice-card.page.html',
  styleUrl: './purchase-invoice-card.page.css'
})
export class PurchaseInvoiceCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly service = inject(PurchaseInvoiceService);
  private readonly vendorSvc = inject(VendorService);
  readonly paymentTermsSvc = inject(PaymentTermsService);

  readonly invoice = signal<PurchaseInvoiceDetail | null>(null);
  readonly loading = signal(true);
  readonly isNew = signal(false);
  readonly saving = signal(false);
  readonly errorMsg = signal<string | null>(null);

  showVendorDrop = signal(false);
  vendorSugg = signal<Vendor[]>([]);

  newForm = {
    buyFromVendorNo: '',
    payToName: '',
    postingDate: new Date().toISOString().split('T')[0],
    externalDocumentNo: '',
    paymentTermsCode: '',
    currencyCode: '',
  };

  newLines = signal<CreateInvoiceLineForm[]>([]);

  async ngOnInit() {
    const no = this.route.snapshot.paramMap.get('no');
    const qp = this.route.snapshot.queryParamMap;
    const vendorNo = qp.get('vendorNo');
    const vendorName = qp.get('vendorName');

    if (no === 'new') {
      this.isNew.set(true);
      this.loading.set(false);
      this.paymentTermsSvc.load();
      if (vendorNo) {
        this.newForm.buyFromVendorNo = vendorNo;
        this.newForm.payToName = vendorName ?? '';
      }
      if (this.vendorSvc.items().length === 0) {
        this.vendorSvc.load({ pageSize: 200 });
      }
    } else if (no) {
      this.invoice.set(await this.service.getByNo(no));
      this.loading.set(false);
    } else {
      this.loading.set(false);
    }
  }

  private filterVendors(): void {
    const q = this.newForm.buyFromVendorNo.toLowerCase().trim();
    const all = this.vendorSvc.items();
    this.vendorSugg.set(
      !q ? all.slice(0, 10)
         : all.filter(v => v.no.toLowerCase().includes(q) || v.name.toLowerCase().includes(q)).slice(0, 10)
    );
  }

  onVendorFocus(): void { this.filterVendors(); this.showVendorDrop.set(true); }
  onVendorInput(): void { this.filterVendors(); this.showVendorDrop.set(true); }
  hideVendorDrop(): void { setTimeout(() => this.showVendorDrop.set(false), 150); }

  selectVendor(v: Vendor): void {
    this.newForm.buyFromVendorNo = v.no;
    this.newForm.payToName = v.name;
    this.newForm.paymentTermsCode = v.paymentTermsCode || '';
    this.newForm.currencyCode = v.currencyCode || '';
    this.showVendorDrop.set(false);
  }

  addLine(): void {
    this.newLines.update(lines => [...lines, {
      description: '',
      quantity: 1,
      unitCost: 0,
      unitOfMeasureCode: 'UND',
      vatPct: 18
    }]);
  }

  removeLine(i: number): void {
    this.newLines.update(lines => lines.filter((_, idx) => idx !== i));
  }

  lineTotal(line: CreateInvoiceLineForm): number {
    const base = (line.quantity || 0) * (line.unitCost || 0);
    return Math.round(base * (1 + (line.vatPct || 0) / 100) * 100) / 100;
  }

  subtotal(): number {
    return this.newLines().reduce((s, l) => s + (l.quantity || 0) * (l.unitCost || 0), 0);
  }

  totalVat(): number {
    return this.newLines().reduce((s, l) => {
      const base = (l.quantity || 0) * (l.unitCost || 0);
      return s + base * ((l.vatPct || 0) / 100);
    }, 0);
  }

  totalWithVat(): number {
    return this.subtotal() + this.totalVat();
  }

  async saveInvoice(): Promise<void> {
    if (!this.newForm.buyFromVendorNo) {
      this.errorMsg.set('El proveedor es obligatorio.');
      return;
    }
    const lines = this.newLines();
    if (lines.length === 0) {
      this.errorMsg.set('La factura debe tener al menos una línea.');
      return;
    }
    const invalid = lines.find(l => !l.description?.trim() || !l.quantity || !l.unitCost);
    if (invalid) {
      this.errorMsg.set('Todas las líneas deben tener descripción, cantidad y costo unitario.');
      return;
    }

    this.saving.set(true);
    this.errorMsg.set(null);
    try {
      const no = await this.service.create({
        ...this.newForm,
        lines: lines.map(l => ({
          description: l.description,
          quantity: l.quantity,
          unitCost: l.unitCost,
          unitOfMeasureCode: l.unitOfMeasureCode || 'UND',
          vatPct: l.vatPct ?? 18,
        }))
      });
      this.router.navigate(['/purchase-invoices', no], { replaceUrl: true });
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Error al registrar la factura.');
    } finally {
      this.saving.set(false);
    }
  }
}
