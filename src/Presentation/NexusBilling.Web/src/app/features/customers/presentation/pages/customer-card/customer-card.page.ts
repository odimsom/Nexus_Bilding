import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerService, CustomerFormData } from '../../../data/customer.service';
import { Customer } from '../../../domain/customer.model';
import { RncPipe } from '../../../../../shared/pipes/rnc.pipe';
import { PhonePipe } from '../../../../../shared/pipes/phone.pipe';
import { RncFormatDirective } from '../../../../../shared/directives/rnc-format.directive';
import { PhoneFormatDirective } from '../../../../../shared/directives/phone-format.directive';
import { CreateSalesOrderModalComponent } from '../../../../sales/presentation/components/create-sales-order-modal/create-sales-order-modal.component';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { PaymentMethodService } from '../../../../../core/services/payment-method.service';
import { SalespersonService } from '../../../../../core/services/salesperson.service';
import { CurrencyService } from '../../../../../core/services/currency.service';


@Component({
  selector: 'app-customer-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, RncPipe, PhonePipe, RncFormatDirective, PhoneFormatDirective, CreateSalesOrderModalComponent],
  templateUrl: './customer-card.page.html',
  styleUrl: './customer-card.page.css'
})
export class CustomerCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(CustomerService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly paymentMethodSvc = inject(PaymentMethodService);
  readonly salespersonSvc = inject(SalespersonService);
  readonly currencySvc = inject(CurrencyService);

  customer = signal<Customer | null>(null);
  loading = signal(true);
  actionLoading = signal(false);

  showEdit = signal(false);
  saving = signal(false);
  editError = signal<string | null>(null);
  form: CustomerFormData = {} as CustomerFormData;

  showOrder = signal(false);

  utilizacion(): number {
    const c = this.customer();
    if (!c || c.creditLimit <= 0) return 0;
    return Math.min(100, Math.round((c.balance / c.creditLimit) * 100));
  }

  disponible(): number {
    const c = this.customer();
    if (!c) return 0;
    return c.creditLimit > 0 ? c.creditLimit - c.balance : 0;
  }

  async ngOnInit(): Promise<void> {
    this.paymentTermsSvc.load();
    this.paymentMethodSvc.load();
    this.salespersonSvc.load();
    this.currencySvc.load();
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const c = await this.svc.getByNo(no);
    this.customer.set(c);
    this.loading.set(false);
  }

  openEdit(): void {
    const c = this.customer()!;
    this.form = { no: c.no, name: c.name, address: c.address, city: c.city, contact: c.contact, phoneNo: c.phoneNo, email: c.email, creditLimit: c.creditLimit, vatRegistrationNo: c.vatRegistrationNo, paymentTermsCode: c.paymentTermsCode, paymentMethodCode: c.paymentMethodCode, salespersonCode: c.salespersonCode, currencyCode: c.currencyCode, customerPostingGroup: c.customerPostingGroup, countryRegionCode: c.countryRegionCode };
    this.editError.set(null);
    this.showEdit.set(true);
  }

  closeEdit(): void { this.showEdit.set(false); }

  async saveEdit(): Promise<void> {
    if (!this.form.name?.trim()) { this.editError.set('El nombre es obligatorio.'); return; }
    this.saving.set(true);
    this.editError.set(null);
    try {
      await this.svc.update(this.customer()!.no, this.form);
      const updated = await this.svc.getByNo(this.customer()!.no);
      this.customer.set(updated);
      this.closeEdit();
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.saving.set(false);
    }
  }

  async toggleBlock(): Promise<void> {
    const c = this.customer();
    if (!c) return;
    this.actionLoading.set(true);
    try {
      if (c.blocked) await this.svc.unblock(c.no);
      else await this.svc.block(c.no);
      const updated = await this.svc.getByNo(c.no);
      this.customer.set(updated);
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al cambiar el estado del cliente.');
    } finally {
      this.actionLoading.set(false);
    }
  }

  openOrder(): void { this.showOrder.set(true); }
  closeOrder(): void { this.showOrder.set(false); }
}
