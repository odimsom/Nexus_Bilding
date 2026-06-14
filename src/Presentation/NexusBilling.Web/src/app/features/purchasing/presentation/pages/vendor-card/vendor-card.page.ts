import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VendorService } from '../../../data/vendor.service';
import { Vendor, CreateVendorFormData } from '../../../domain/vendor.model';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { PaymentMethodService } from '../../../../../core/services/payment-method.service';
import { CurrencyService } from '../../../../../core/services/currency.service';

@Component({
  selector: 'app-vendor-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, CurrencyPipe],
  templateUrl: './vendor-card.page.html',
  styleUrl: './vendor-card.page.css'
})
export class VendorCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(VendorService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly paymentMethodSvc = inject(PaymentMethodService);
  readonly currencySvc = inject(CurrencyService);

  vendor = signal<Vendor | null>(null);
  loading = signal(true);
  actionLoading = signal(false);

  showEdit = signal(false);
  saving = signal(false);
  editError = signal<string | null>(null);
  form: CreateVendorFormData = {
    name: '', address: '', address2: '', city: '', province: '', country: '',
    contact: '', phoneNo: '', phoneNo2: '', email: '', webSite: '',
    rnc: '', paymentTermsCode: '', paymentMethodCode: '', currencyCode: '',
    creditLimit: 0, vendorType: ''
  };

  readonly provinces = [
    'Azua', 'Bahoruco', 'Barahona', 'Dajabón', 'Distrito Nacional',
    'Duarte', 'Elías Piña', 'El Seibo', 'Espaillat', 'Hato Mayor',
    'Hermanas Mirabal', 'Independencia', 'La Altagracia', 'La Romana',
    'La Vega', 'María Trinidad Sánchez', 'Monseñor Nouel', 'Monte Cristi',
    'Monte Plata', 'Pedernales', 'Peravia', 'Puerto Plata', 'Samaná',
    'San Cristóbal', 'San José de Ocoa', 'San Juan', 'San Pedro de Macorís',
    'Sánchez Ramírez', 'Santiago', 'Santiago Rodríguez', 'Santo Domingo',
    'Valverde'
  ];

  async ngOnInit() {
    this.paymentTermsSvc.load();
    this.paymentMethodSvc.load();
    this.currencySvc.load();
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const v = await this.svc.getByNo(no);
    this.vendor.set(v);
    this.loading.set(false);
  }

  openEdit() {
    const v = this.vendor()!;
    this.form = {
      name: v.name,
      address: v.address || '',
      address2: v.address2 || '',
      city: v.city || '',
      province: v.province || '',
      country: v.country || '',
      contact: v.contact || '',
      phoneNo: v.phoneNo || '',
      phoneNo2: v.phoneNo2 || '',
      email: v.email || '',
      webSite: v.webSite || '',
      rnc: v.rnc || '',
      paymentTermsCode: v.paymentTermsCode || '',
      paymentMethodCode: v.paymentMethodCode || '',
      currencyCode: v.currencyCode || '',
      creditLimit: v.creditLimit ?? 0,
      vendorType: v.vendorType || '',
    };
    this.editError.set(null);
    this.showEdit.set(true);
  }

  closeEdit() {
    this.showEdit.set(false);
  }

  async saveEdit() {
    if (!this.form.name?.trim()) {
      this.editError.set('El nombre es obligatorio.');
      return;
    }
    this.saving.set(true);
    try {
      await this.svc.update(this.vendor()!.no, this.form);
      const v = await this.svc.getByNo(this.vendor()!.no);
      this.vendor.set(v);
      this.closeEdit();
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al actualizar el proveedor.');
    } finally {
      this.saving.set(false);
    }
  }

  async toggleBlock() {
    const v = this.vendor()!;
    this.actionLoading.set(true);
    try {
      if (v.blocked) await this.svc.unblock(v.no);
      else await this.svc.block(v.no);
      const updated = await this.svc.getByNo(v.no);
      this.vendor.set(updated);
    } catch {
      alert('Error al cambiar el estado del proveedor.');
    } finally {
      this.actionLoading.set(false);
    }
  }

  vendorTypeLabel(code: string): string {
    const map: Record<string, string> = {
      local: 'Local', extranjero: 'Extranjero', servicio: 'Servicios', contratista: 'Contratista'
    };
    return map[code] ?? '';
  }

  paymentTermsLabel(code: string): string {
    return this.paymentTermsSvc.terms().find(t => t.code === code)?.description || code;
  }

  paymentMethodLabel(code: string): string {
    return this.paymentMethodSvc.methods().find(m => m.code === code)?.description ?? code;
  }
}
