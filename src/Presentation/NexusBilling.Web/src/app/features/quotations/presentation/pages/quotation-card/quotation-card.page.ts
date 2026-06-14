import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { QuotationService } from '../../../data/quotation.service';
import { CustomerService, CustomerListItem } from '../../../../customers/data/customer.service';
import { QuotationDetail, CreateQuotationLineData } from '../../../domain/quotation.model';
import { UserService, AppUser } from '../../../../security/data/user.service';
import { AuthState } from '../../../../auth/presentation/state/auth.state';
import { exportToCSV } from '../../../../../shared/utils/export.util';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { PdfService, QuotationPdfData } from '../../../../../shared/services/pdf.service';
import { CurrencyService } from '../../../../../core/services/currency.service';
import { PaymentMethodService } from '../../../../../core/services/payment-method.service';

const BILLING_TYPES = [
  { value: 0, label: 'Tiempo Estándar' },
  { value: 1, label: 'Valor Único' },
  { value: 2, label: 'Tiempo Estimado' },
] as const;

interface FormLine {
  lineType: 'Item' | 'Service';
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  vatPct: number;
  // service fields
  serviceBillingType: number | null;
  serviceStartDate: string | null;
  serviceEndDate: string | null;
  serviceHours: number | null;
  hourlyRate: number | null;
  resourceNo: string | null;
}

@Component({
  selector: 'app-quotation-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './quotation-card.page.html',
  styleUrl: './quotation-card.page.css'
})
export class QuotationCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(QuotationService);
  private readonly customerSvc = inject(CustomerService);
  private readonly userSvc = inject(UserService);
  private readonly authState = inject(AuthState);
  private readonly pdfSvc = inject(PdfService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly currencySvc = inject(CurrencyService);
  readonly paymentMethodSvc = inject(PaymentMethodService);

  employees = signal<AppUser[]>([]);

  isNew = signal(false);
  loading = signal(true);
  saving = signal(false);
  working = signal(false);
  printing = signal(false);
  errorMsg = signal('');
  actionError = signal('');
  detail = signal<QuotationDetail | null>(null);
  showDupDialog = signal(false);
  dupDate = new Date().toISOString().split('T')[0];
  dupValidUntil = '';

  showEditModal = signal(false);
  editSaving = signal(false);
  editError = signal('');
  editForm = {
    validUntilDate: '',
    quotedBy: '',
    observations: '',
    currencyCode: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    externalDocumentNo: '',
  };

  showCustomerDrop = signal(false);
  customerSugg = signal<CustomerListItem[]>([]);

  form = {
    sellToCustomerNo: '',
    sellToCustomerName: '',
    postingDate: new Date().toISOString().split('T')[0],
    validUntilDate: '',
    quotedBy: '',
    observations: '',
    currencyCode: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    externalDocumentNo: '',
  };

  newLines = signal<FormLine[]>([]);

  async ngOnInit(): Promise<void> {
    this.paymentTermsSvc.load();
    this.currencySvc.load();
    this.paymentMethodSvc.load();
    await this.userSvc.loadUsers();
    this.employees.set(this.userSvc.users().filter(u => u.isActive));

    const no = this.route.snapshot.paramMap.get('no');
    if (no === 'new') {
      this.isNew.set(true);
      this.loading.set(false);
      await this.customerSvc.load({ pageSize: 300 });
      // Pre-fill quotedBy with current user if available
      const currentUsername = this.authState.user()?.username;
      const currentUser = currentUsername ? this.userSvc.users().find(u => u.username === currentUsername) : null;
      if (currentUser) this.form.quotedBy = currentUser.employeeNo ? `${currentUser.employeeNo} - ${currentUser.fullName}` : currentUser.fullName;
      return;
    }
    if (no) {
      try {
        this.detail.set(await this.svc.getByNo(no));
      } catch {
        this.detail.set(null);
      }
    }
    this.loading.set(false);
  }

  // Customer autocomplete
  onCustomerFocus(): void { this.filterCustomers(); this.showCustomerDrop.set(true); }
  onCustomerInput(): void { this.filterCustomers(); this.showCustomerDrop.set(true); }
  hideCustomerDrop(): void { setTimeout(() => this.showCustomerDrop.set(false), 150); }

  filterCustomers(): void {
    const q = this.form.sellToCustomerName.toLowerCase().trim();
    const all = this.customerSvc.items();
    this.customerSugg.set(
      !q ? all.slice(0, 10)
         : all.filter(c => c.name.toLowerCase().includes(q) || c.no.toLowerCase().includes(q)).slice(0, 10)
    );
  }

  selectCustomer(c: CustomerListItem): void {
    this.form.sellToCustomerNo = c.no;
    this.form.sellToCustomerName = c.name;
    if (c.paymentTermsCode) this.form.paymentTermsCode = c.paymentTermsCode;
    this.showCustomerDrop.set(false);
  }

  // Lines
  addLine(type: 'Item' | 'Service'): void {
    this.newLines.update(lines => [...lines, {
      lineType: type,
      itemNo: '',
      description: '',
      quantity: type === 'Service' ? 1 : 1,
      unitPrice: 0,
      lineDiscountPct: 0,
      unitOfMeasure: type === 'Service' ? 'HRS' : 'UND',
      vatPct: 18,
      serviceBillingType: type === 'Service' ? 1 : null,
      serviceStartDate: null,
      serviceEndDate: null,
      serviceHours: null,
      hourlyRate: null,
      resourceNo: null,
    }]);
  }

  removeLine(i: number): void {
    this.newLines.update(lines => lines.filter((_, idx) => idx !== i));
  }

  recalcServiceLine(i: number): void {
    this.newLines.update(lines => {
      const l = lines[i];
      if (l.serviceBillingType === 2 && l.serviceHours && l.hourlyRate) {
        l.unitPrice = l.hourlyRate;
        l.quantity = l.serviceHours;
      }
      return [...lines];
    });
  }

  lineBase(line: FormLine): number {
    const qty = (line.serviceBillingType === 2 && line.serviceHours) ? line.serviceHours : line.quantity;
    const price = (line.serviceBillingType === 2 && line.hourlyRate) ? line.hourlyRate : line.unitPrice;
    return qty * price;
  }

  lineTotal(line: FormLine): number {
    const base = this.lineBase(line);
    const afterDisc = base * (1 - (line.lineDiscountPct || 0) / 100);
    return Math.round(afterDisc * (1 + (line.vatPct || 0) / 100) * 100) / 100;
  }

  subtotal = computed(() => this.newLines().reduce((s, l) => {
    const base = this.lineBase(l);
    return s + Math.round(base * (1 - (l.lineDiscountPct || 0) / 100) * 100) / 100;
  }, 0));

  totalItbis = computed(() => this.newLines().reduce((s, l) => {
    const base = this.lineBase(l);
    const after = base * (1 - (l.lineDiscountPct || 0) / 100);
    return s + Math.round(after * ((l.vatPct || 0) / 100) * 100) / 100;
  }, 0));

  totalWithItbis = computed(() => this.subtotal() + this.totalItbis());

  async save(): Promise<void> {
    if (!this.form.sellToCustomerNo) { this.errorMsg.set('El cliente es obligatorio.'); return; }
    const lines = this.newLines();
    if (lines.length === 0) { this.errorMsg.set('Debe agregar al menos una línea.'); return; }
    const invalid = lines.find(l => !l.description?.trim());
    if (invalid) { this.errorMsg.set('Todas las líneas deben tener descripción.'); return; }

    this.saving.set(true);
    this.errorMsg.set('');
    try {
      const no = await this.svc.create({
        sellToCustomerNo: this.form.sellToCustomerNo,
        sellToCustomerName: this.form.sellToCustomerName,
        postingDate: this.form.postingDate,
        validUntilDate: this.form.validUntilDate || null,
        quotedBy: this.form.quotedBy || null,
        observations: this.form.observations || null,
        currencyCode: this.form.currencyCode,
        paymentTermsCode: this.form.paymentTermsCode,
        paymentMethodCode: this.form.paymentMethodCode,
        externalDocumentNo: this.form.externalDocumentNo || null,
        seriesCode: 'COT',
        lines: lines.map<CreateQuotationLineData>(l => ({
          lineType: l.lineType,
          itemNo: l.itemNo,
          description: l.description,
          quantity: l.serviceBillingType === 2 ? (l.serviceHours ?? 1) : l.quantity,
          unitPrice: l.serviceBillingType === 2 ? (l.hourlyRate ?? 0) : l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          unitOfMeasure: l.unitOfMeasure,
          vatPct: l.vatPct,
          serviceBillingType: l.serviceBillingType,
          serviceStartDate: l.serviceStartDate,
          serviceEndDate: l.serviceEndDate,
          serviceHours: l.serviceHours,
          hourlyRate: l.hourlyRate,
          resourceNo: l.resourceNo,
        }))
      });
      this.router.navigate(['/quotes', no], { replaceUrl: true });
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Error al guardar la cotización.');
    } finally {
      this.saving.set(false);
    }
  }

  async convertToOrder(): Promise<void> {
    const d = this.detail();
    if (!d || this.working()) return;
    this.working.set(true);
    this.actionError.set('');
    try {
      const orderNo = await this.svc.convertToOrder(d.no);
      this.router.navigate(['/sales', orderNo]);
    } catch (e: any) {
      this.actionError.set(e?.error?.error?.message ?? 'Error al convertir.');
    } finally {
      this.working.set(false);
    }
  }

  openEditHeader(): void {
    const d = this.detail();
    if (!d) return;
    const toDate = (iso: string | null | undefined) =>
      iso ? new Date(iso).toISOString().split('T')[0] : '';
    this.editForm = {
      validUntilDate: toDate(d.validUntilDate),
      quotedBy: d.quotedBy ?? '',
      observations: d.observations ?? '',
      currencyCode: d.currencyCode ?? '',
      paymentTermsCode: d.paymentTermsCode ?? '',
      paymentMethodCode: d.paymentMethodCode ?? '',
      externalDocumentNo: d.externalDocumentNo ?? '',
    };
    this.editError.set('');
    this.showEditModal.set(true);
  }

  async saveEditHeader(): Promise<void> {
    const d = this.detail();
    if (!d || this.editSaving()) return;
    this.editSaving.set(true);
    this.editError.set('');
    try {
      await this.svc.updateHeader(d.no, {
        validUntilDate: this.editForm.validUntilDate || null,
        quotedBy: this.editForm.quotedBy || null,
        observations: this.editForm.observations || null,
        currencyCode: this.editForm.currencyCode,
        paymentTermsCode: this.editForm.paymentTermsCode,
        paymentMethodCode: this.editForm.paymentMethodCode,
        externalDocumentNo: this.editForm.externalDocumentNo || null,
      });
      this.showEditModal.set(false);
      this.detail.set(await this.svc.getByNo(d.no));
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.editSaving.set(false);
    }
  }

  duplicateQuote(): void {
    this.dupDate = new Date().toISOString().split('T')[0];
    this.dupValidUntil = '';
    this.showDupDialog.set(true);
  }

  async confirmDuplicate(): Promise<void> {
    const d = this.detail();
    if (!d || !this.dupDate || this.working()) return;
    this.working.set(true);
    this.actionError.set('');
    try {
      const newNo = await this.svc.duplicate(d.no, this.dupDate, this.dupValidUntil || null);
      this.showDupDialog.set(false);
      this.router.navigate(['/quotes', newNo]);
    } catch (e: any) {
      this.actionError.set(e?.error?.error?.message ?? 'Error al duplicar.');
    } finally {
      this.working.set(false);
    }
  }

  async printPdf(): Promise<void> {
    const d = this.detail();
    if (!d || this.printing()) return;
    this.printing.set(true);
    try {
      const pdfData: QuotationPdfData = {
        no: d.no,
        customerName: d.sellToCustomerName,
        customerNo: d.sellToCustomerNo,
        postingDate: d.postingDate,
        validUntilDate: d.validUntilDate ? new Date(d.validUntilDate).toLocaleDateString('es-DO') : null,
        quotedBy: d.quotedBy,
        observations: d.observations,
        externalDocumentNo: d.externalDocumentNo,
        paymentTermsCode: d.paymentTermsCode,
        paymentMethodCode: d.paymentMethodCode,
        currencyCode: d.currencyCode || 'DOP',
        amount: d.amount,
        amountIncludingVat: d.amountIncludingVat,
        lines: d.lines.map(l => ({
          lineNo: l.lineNo,
          lineType: l.lineType,
          no: l.no,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          amount: l.amount,
          amountIncludingVat: l.amountIncludingVat,
          vatPct: l.vatPct,
          unitOfMeasure: l.unitOfMeasure,
          serviceBillingType: l.serviceBillingType,
          serviceStartDate: l.serviceStartDate,
          serviceEndDate: l.serviceEndDate,
          serviceHours: l.serviceHours,
          resourceNo: l.resourceNo,
        })),
      };
      await this.pdfSvc.printQuotation(pdfData);
    } finally {
      this.printing.set(false);
    }
  }

  detailStatusBadge(): string {
    const d = this.detail();
    if (!d) return 'nx-badge--outline';
    if (d.status === 'Closed') return 'nx-badge--outline';
    return 'nx-badge--info';
  }

  detailStatusLabel(): string {
    const d = this.detail();
    if (!d) return '';
    return d.status === 'Closed' ? 'Cerrada' : 'Abierta';
  }

  billingTypeLabel(t: number | null): string {
    if (t === null || t === undefined) return '';
    return ([null, 'Tiempo Estándar', 'Valor Único', 'Tiempo Estimado'] as (string | null)[])[t + 1] ?? '';
  }
}
