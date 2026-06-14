import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../../../../core/services/api.service';
import { PdfService } from '../../../../../shared/services/pdf.service';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { PaymentMethodService } from '../../../../../core/services/payment-method.service';
import { CurrencyService } from '../../../../../core/services/currency.service';
import { SalespersonService } from '../../../../../core/services/salesperson.service';
import { InvoiceService } from '../../../data/invoice.service';
import { SalesOrderDetail } from '../../../domain/invoice.model';
import { CustomerService, CustomerListItem } from '../../../../customers/data/customer.service';
import { exportToCSV } from '../../../../../shared/utils/export.util';

interface NewLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType: string;
}

interface Installment {
  no: number;
  dueDate: string;
  amount: number;
  paid: boolean;
  paidDate: string;
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
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly paymentMethodSvc = inject(PaymentMethodService);
  readonly currencySvc = inject(CurrencyService);
  readonly salespersonSvc = inject(SalespersonService);

  // Detail view state
  order = signal<SalesOrderDetail | null>(null);
  loading = signal(true);
  releasing = signal(false);
  posting = signal(false);
  printing = signal(false);

  showEditModal = signal(false);
  editSaving = signal(false);
  editError = signal('');
  editForm = {
    dueDate: '',
    currencyCode: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    salespersonCode: '',
    externalDocumentNo: '',
  };

  // Shared lines state (edit existing)
  newLines = signal<NewLine[]>([]);
  editingLines = signal(false);
  savingLines = signal(false);

  // Installments
  installments = signal<Installment[]>([]);
  installmentCount = 3;

  isDetailInstallments = computed(() => this.order()?.paymentTermsCode === 'CUOTAS');

  paidCount = computed(() => this.installments().filter(i => i.paid).length);
  paymentPct = computed(() => {
    const total = this.installments().length;
    return total > 0 ? Math.round(this.paidCount() / total * 100) : 0;
  });
  installmentTotal = computed(() => this.installments().reduce((s, i) => s + (i.amount || 0), 0));

  async ngOnInit(): Promise<void> {
    this.paymentTermsSvc.load();
    this.paymentMethodSvc.load();
    this.currencySvc.load();
    this.salespersonSvc.load();
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    if (no === 'new') {
      this.router.navigate(['/sales']);
      return;
    }
    try {
      const detail = await this.svc.getOrderDetail(no);
      this.order.set(detail);
      this.loadInstallments(no);
    } catch {
      this.order.set(null);
    } finally {
      this.loading.set(false);
    }
  }

  private installmentKey(no: string): string { return `nx_inst_${no}`; }

  private loadInstallments(orderNo: string): void {
    try {
      const raw = localStorage.getItem(this.installmentKey(orderNo));
      if (raw) this.installments.set(JSON.parse(raw));
    } catch { /* ignore */ }
  }

  private saveInstallments(orderNo: string): void {
    localStorage.setItem(this.installmentKey(orderNo), JSON.stringify(this.installments()));
  }

  onPaymentTermsChange(code: string): void {
    if (code === 'CUOTAS') {
      this.generateInstallments();
    } else {
      this.installments.set([]);
    }
  }

  generateInstallments(): void {
    const total = this.totalWithVat();
    const count = this.installmentCount;
    if (count <= 0) return;
    const amt = total > 0 ? Math.round((total / count) * 100) / 100 : 0;
    const today = new Date();
    const rows: Installment[] = Array.from({ length: count }, (_, i) => {
      const d = new Date(today);
      d.setMonth(d.getMonth() + i + 1);
      return {
        no: i + 1,
        dueDate: d.toISOString().split('T')[0],
        amount: i === count - 1 && total > 0 ? Math.round((total - amt * (count - 1)) * 100) / 100 : amt,
        paid: false,
        paidDate: '',
      };
    });
    this.installments.set(rows);
  }

  addInstallmentRow(): void {
    const existing = this.installments();
    const lastDate = existing.length > 0 ? existing[existing.length - 1].dueDate : new Date().toISOString().split('T')[0];
    const d = new Date(lastDate);
    d.setMonth(d.getMonth() + 1);
    this.installments.update(rows => [...rows, {
      no: rows.length + 1,
      dueDate: d.toISOString().split('T')[0],
      amount: 0,
      paid: false,
      paidDate: '',
    }]);
    const ord = this.order();
    if (ord) this.saveInstallments(ord.no);
  }

  removeInstallmentRow(i: number): void {
    this.installments.update(rows => rows.filter((_, idx) => idx !== i).map((r, idx) => ({ ...r, no: idx + 1 })));
    const ord = this.order();
    if (ord) this.saveInstallments(ord.no);
  }

  togglePaid(inst: Installment): void {
    inst.paid = !inst.paid;
    inst.paidDate = inst.paid ? new Date().toISOString().split('T')[0] : '';
    this.installments.update(rows => [...rows]);
    const ord = this.order();
    if (ord) this.saveInstallments(ord.no);
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

  // ── Header editing ─────────────────────────────
  openEditHeader(): void {
    const ord = this.order();
    if (!ord) return;
    const toDate = (val: string | null | undefined) =>
      val ? new Date(val).toISOString().split('T')[0] : '';
    this.editForm = {
      dueDate: toDate(ord.dueDate),
      currencyCode: ord.currencyCode ?? '',
      paymentTermsCode: ord.paymentTermsCode ?? '',
      paymentMethodCode: ord.paymentMethodCode ?? '',
      salespersonCode: ord.salespersonCode ?? '',
      externalDocumentNo: ord.externalDocumentNo ?? '',
    };
    this.editError.set('');
    this.showEditModal.set(true);
  }

  async saveEditHeader(): Promise<void> {
    const ord = this.order();
    if (!ord || this.editSaving()) return;
    this.editSaving.set(true);
    this.editError.set('');
    try {
      await this.svc.updateOrderHeader(ord.no, {
        dueDate: this.editForm.dueDate || null,
        currencyCode: this.editForm.currencyCode,
        paymentTermsCode: this.editForm.paymentTermsCode,
        paymentMethodCode: this.editForm.paymentMethodCode,
        salespersonCode: this.editForm.salespersonCode,
        externalDocumentNo: this.editForm.externalDocumentNo || null,
      });
      this.showEditModal.set(false);
      this.order.set(await this.svc.getOrderDetail(ord.no));
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.editSaving.set(false);
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
    return ({ Order: 'Orden', Quote: 'Cotización', Invoice: 'Factura' } as Record<string, string>)[t] ?? t;
  }

  exportLines(): void {
    const ord = this.order();
    if (!ord?.lines?.length) return;
    exportToCSV(`lineas_${ord.no}`,
      ['No. Línea', 'No. Producto', 'Descripción', 'Cantidad', 'U/M', 'Precio Unit.', 'Dto %', 'Importe', 'c/IVA'],
      ord.lines.map(l => [l.lineNo, l.no, l.description, l.quantity, l.unitOfMeasure, l.unitPrice, l.lineDiscount, l.amount, l.amountIncludingVat])
    );
  }

  statusClass(s: string): string {
    return ({ Open: 'nx-badge--info', Released: 'nx-badge--success', Closed: 'nx-badge--outline' } as Record<string, string>)[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return ({ Open: 'Abierta', Released: 'Liberada', Closed: 'Cerrada' } as Record<string, string>)[s] ?? s;
  }
}
