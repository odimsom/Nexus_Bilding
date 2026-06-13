import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../../../../core/services/api.service';
import { PdfService } from '../../../../../shared/services/pdf.service';
import { InvoiceService } from '../../../data/invoice.service';
import { SalesOrderDetail } from '../../../domain/invoice.model';
import { CustomerService, CustomerListItem } from '../../../../customers/data/customer.service';

interface NewLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType: string;
}

@Component({
  selector: 'app-sales-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './sales-order-card.page.html',
  styleUrl: './sales-order-card.page.css'
})
export class SalesOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(InvoiceService);
  private readonly api = inject(ApiService);
  private readonly pdfSvc = inject(PdfService);
  private readonly customerSvc = inject(CustomerService);

  // Detail view state
  order = signal<SalesOrderDetail | null>(null);
  loading = signal(true);
  releasing = signal(false);
  posting = signal(false);
  printing = signal(false);

  // New order form state
  isNew = signal(false);
  saving = signal(false);
  errorMsg = signal('');
  showCustomerDrop = signal(false);
  customerSugg = signal<CustomerListItem[]>([]);

  newHeader = {
    documentType: 'Order',
    sellToCustomerNo: '',
    sellToCustomerName: '',
    postingDate: new Date().toISOString().split('T')[0],
    dueDate: '',
    externalDocumentNo: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    currencyCode: '',
  };

  // Shared lines state (new form + edit existing)
  newLines = signal<NewLine[]>([]);
  editingLines = signal(false);
  savingLines = signal(false);

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    if (no === 'new') {
      this.isNew.set(true);
      this.loading.set(false);
      return;
    }
    try {
      const detail = await this.svc.getOrderDetail(no);
      this.order.set(detail);
    } catch {
      this.order.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  // ── Customer autocomplete ──────────────────────
  async searchCustomers(e: Event): Promise<void> {
    const q = (e.target as HTMLInputElement).value.trim();
    this.newHeader.sellToCustomerNo = '';
    if (q.length < 1) { this.showCustomerDrop.set(false); return; }
    await this.customerSvc.load({ search: q, pageSize: 8 });
    this.customerSugg.set(this.customerSvc.items());
    this.showCustomerDrop.set(true);
  }

  selectCustomer(c: CustomerListItem): void {
    this.newHeader.sellToCustomerNo = c.no;
    this.newHeader.sellToCustomerName = c.name;
    if (!this.newHeader.paymentTermsCode && c.paymentTermsCode) {
      this.newHeader.paymentTermsCode = c.paymentTermsCode;
    }
    this.showCustomerDrop.set(false);
  }

  hideCustomerDrop(): void {
    setTimeout(() => this.showCustomerDrop.set(false), 150);
  }

  // ── Lines ──────────────────────────────────────
  addLine(): void {
    this.newLines.update(lines => [...lines, {
      itemNo: '', description: '', quantity: 1,
      unitPrice: 0, lineDiscountPct: 0, unitOfMeasure: 'UND', lineType: 'Item'
    }]);
  }

  removeLine(i: number): void {
    this.newLines.update(lines => lines.filter((_, idx) => idx !== i));
  }

  lineTotal(line: NewLine): number {
    const base = line.quantity * line.unitPrice;
    const disc = base * (line.lineDiscountPct / 100);
    return Math.round((base - disc) * 100) / 100;
  }

  subtotal = computed(() => this.newLines().reduce((s, l) => s + this.lineTotal(l), 0));
  totalVat = computed(() => Math.round(this.subtotal() * 0.18 * 100) / 100);
  totalWithVat = computed(() => Math.round((this.subtotal() + this.totalVat()) * 100) / 100);

  // ── Save new order ─────────────────────────────
  async saveOrder(): Promise<void> {
    if (!this.newHeader.sellToCustomerNo) {
      this.errorMsg.set('Debe seleccionar un cliente.');
      return;
    }
    const lines = this.newLines();
    if (lines.length === 0) {
      this.errorMsg.set('Debe agregar al menos una línea.');
      return;
    }
    this.saving.set(true);
    this.errorMsg.set('');
    try {
      const no = await this.svc.createOrder({
        documentType: this.newHeader.documentType,
        sellToCustomerNo: this.newHeader.sellToCustomerNo,
        sellToCustomerName: this.newHeader.sellToCustomerName,
        postingDate: this.newHeader.postingDate,
        dueDate: this.newHeader.dueDate || null,
        externalDocumentNo: this.newHeader.externalDocumentNo,
        paymentTermsCode: this.newHeader.paymentTermsCode,
        paymentMethodCode: this.newHeader.paymentMethodCode,
        currencyCode: this.newHeader.currencyCode,
        salespersonCode: '',
        seriesCode: this.newHeader.documentType === 'Quote' ? 'COT' : 'ORD',
        lines: lines.map(l => ({
          itemNo: l.itemNo,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          unitOfMeasure: l.unitOfMeasure,
          lineType: l.lineType,
        })),
      });
      this.router.navigate(['/sales', no]);
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Error al guardar la orden.');
    } finally {
      this.saving.set(false);
    }
  }

  // ── Edit lines on existing order ───────────────
  startEditLines(): void {
    const ord = this.order();
    if (!ord) return;
    this.newLines.set(ord.lines.map(l => ({
      itemNo: l.no || '',
      description: l.description,
      quantity: l.quantity,
      unitPrice: l.unitPrice,
      lineDiscountPct: l.lineDiscount || 0,
      unitOfMeasure: l.unitOfMeasure || '',
      lineType: l.type || 'Item',
    })));
    this.editingLines.set(true);
  }

  cancelEditLines(): void {
    this.editingLines.set(false);
    this.newLines.set([]);
  }

  async saveLines(): Promise<void> {
    const ord = this.order();
    if (!ord) return;
    this.savingLines.set(true);
    try {
      await this.svc.updateLines(ord.no, this.newLines().map(l => ({
        itemNo: l.itemNo,
        description: l.description,
        quantity: l.quantity,
        unitPrice: l.unitPrice,
        lineDiscountPct: l.lineDiscountPct,
        unitOfMeasure: l.unitOfMeasure,
        lineType: l.lineType,
      })));
      const updated = await this.svc.getOrderDetail(ord.no);
      this.order.set(updated);
      this.editingLines.set(false);
      this.newLines.set([]);
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al guardar las líneas.');
    } finally {
      this.savingLines.set(false);
    }
  }

  // ── Existing order actions ─────────────────────
  async postOrder(): Promise<void> {
    const ord = this.order();
    if (!ord || this.posting()) return;
    this.posting.set(true);
    try {
      const result = await firstValueFrom(this.api.post<{ invoiceNo: string }>(`sales/orders/${encodeURIComponent(ord.no)}/post`, {}));
      this.router.navigate(['/invoices', result.invoiceNo]);
    } catch (err: any) {
      alert(err?.error?.error?.message ?? 'Error al publicar la orden.');
    } finally {
      this.posting.set(false);
    }
  }

  async releaseOrder(): Promise<void> {
    const ord = this.order();
    if (!ord || this.releasing()) return;
    this.releasing.set(true);
    try {
      await firstValueFrom(this.api.patch<any>(`sales/orders/${encodeURIComponent(ord.no)}/release`));
      const updated = await this.svc.getOrderDetail(ord.no);
      this.order.set(updated);
    } catch (err) {
      console.error('Error liberando orden:', err);
    } finally {
      this.releasing.set(false);
    }
  }

  async printPdf(): Promise<void> {
    const ord = this.order();
    if (!ord || this.printing()) return;
    this.printing.set(true);
    try {
      await this.pdfSvc.printSalesOrder({
        no: ord.no, documentType: ord.documentType,
        customerName: ord.sellToCustomerName, customerNo: ord.sellToCustomerNo,
        externalDocumentNo: ord.externalDocumentNo, salespersonCode: ord.salespersonCode,
        postingDate: ord.postingDate, dueDate: ord.dueDate,
        paymentTerms: ord.paymentTermsCode, paymentMethodCode: ord.paymentMethodCode,
        currencyCode: ord.currencyCode, status: ord.status,
        lines: ord.lines.map(l => ({
          lineNo: l.lineNo, no: l.no, description: l.description,
          quantity: l.quantity, unitOfMeasure: l.unitOfMeasure,
          unitPrice: l.unitPrice, lineDiscount: l.lineDiscount,
          amount: l.amount, vat: l.vat, amountIncludingVat: l.amountIncludingVat,
        })),
        amount: ord.amount, amountIncludingVat: ord.amountIncludingVat,
      });
    } finally {
      this.printing.set(false);
    }
  }

  docTypeLabel(t: string): string {
    return ({ Order: 'Pedido', Quote: 'Cotización', Invoice: 'Factura' } as Record<string, string>)[t] ?? t;
  }

  statusClass(s: string): string {
    return ({ Open: 'nx-badge--info', Released: 'nx-badge--success', Closed: 'nx-badge--outline' } as Record<string, string>)[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return ({ Open: 'Abierta', Released: 'Liberada', Closed: 'Cerrada' } as Record<string, string>)[s] ?? s;
  }
}
