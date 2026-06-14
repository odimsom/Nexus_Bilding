import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { CustomerService, CustomerListItem, CustomerFormData } from '../../../data/customer.service';
import { CustomerSortField } from '../../../domain/customer.model';
import { ApiService } from '../../../../../core/services/api.service';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { RncFormatDirective } from '../../../../../shared/directives/rnc-format.directive';
import { PhoneFormatDirective } from '../../../../../shared/directives/phone-format.directive';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, RncFormatDirective, PhoneFormatDirective],
  templateUrl: './customer-list.page.html',
  styleUrl: './customer-list.page.css'
})
export class CustomerListPage implements OnInit {
  readonly svc = inject(CustomerService);
  private readonly router = inject(Router);
  private readonly api = inject(ApiService);
  private readonly excel = inject(ExcelExportService);
  readonly paymentTermsSvc = inject(PaymentTermsService);

  searchText = '';
  showBlocked: 'all' | 'active' | 'blocked' = 'all';
  sortField: CustomerSortField = 'name';
  sortAsc = true;

  showModal        = signal(false);
  saving           = signal(false);
  modalError       = signal<string | null>(null);
  noSeriesMissing  = signal(false);
  loadingNextNo    = signal(false);
  previewNo        = signal('');

  form: CustomerFormData = this.emptyForm();

  private _searchTimeout?: ReturnType<typeof setTimeout>;

  activeTagsCount = computed(() => {
    let n = 0;
    if (this.searchText) n++;
    if (this.showBlocked !== 'all') n++;
    return n;
  });

  sorted(): CustomerListItem[] {
    return this.svc.sorted(this.sortField, this.sortAsc);
  }

  ngOnInit(): void { 
    this.paymentTermsSvc.load();
    this.reload(); 
  }

  reload(): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked });
  }

  onSearchChange(): void {
    clearTimeout(this._searchTimeout);
    this._searchTimeout = setTimeout(() => this.reload(), 350);
  }

  onFilterChange(): void { this.reload(); }

  setSort(f: CustomerSortField): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: CustomerSortField): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  clearFilters(): void {
    this.searchText = '';
    this.showBlocked = 'all';
    this.reload();
  }

  goToPage(page: number): void {
    const blocked = this.showBlocked === 'all' ? undefined : this.showBlocked === 'blocked';
    this.svc.load({ search: this.searchText || undefined, blocked, page });
  }

  async exportExcel(): Promise<void> {
    const rows = this.sorted();
    const data = rows.map(r => ({
      no: r.no,
      name: r.name,
      city: r.city || '',
      contact: r.contact || '',
      salesperson: r.salespersonCode || '',
      terms: r.paymentTermsCode || '',
      balance: r.balance,
      balanceDue: r.balanceDue,
      status: r.blocked ? 'Bloqueado' : 'Activo'
    }));

    await this.excel.exportAsExcel({
      filename: 'Clientes_Nexus',
      title: 'Listado de Clientes',
      subtitle: 'Nexus Billing - Módulo de Ventas',
      sheetName: 'Clientes',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Nombre', key: 'name', width: 40 },
        { header: 'Ciudad', key: 'city', width: 20 },
        { header: 'Contacto', key: 'contact', width: 25 },
        { header: 'Vendedor', key: 'salesperson', width: 12 },
        { header: 'Condición Pago', key: 'terms', width: 15 },
        { header: 'Saldo', key: 'balance', width: 15, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
        { header: 'Saldo Vencido', key: 'balanceDue', width: 15, numFmt: '#,##0.00', alignment: { horizontal: 'right' } },
        { header: 'Estado', key: 'status', width: 12 }
      ],
      data
    });
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
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>('administration/no-series/CUST/next'));
      this.previewNo.set(res.nextNo);
      this.form.no = res.nextNo;
    } catch {
      this.previewNo.set('');
      this.form.no = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  closeModal(): void { this.showModal.set(false); }

  goConfigureSeries(): void {
    this.closeModal();
    this.router.navigate(['/settings'], { queryParams: { tab: 'sequences', returnTo: '/customers' } });
  }

  async saveCustomer(): Promise<void> {
    if (!this.form.name?.trim()) {
      this.modalError.set('El nombre es obligatorio.');
      return;
    }
    this.saving.set(true);
    this.modalError.set(null);
    this.noSeriesMissing.set(false);
    try {
      const no = await this.svc.create(this.form);
      this.closeModal();
      this.router.navigate(['/customers', no]);
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? e?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('series')) {
        this.noSeriesMissing.set(true);
        this.modalError.set('No hay una secuencia de numeración configurada para clientes.');
      } else if (msg.toLowerCase().includes('already') || msg.toLowerCase().includes('ya existe') || msg.toLowerCase().includes('duplicate')) {
        this.modalError.set('El número de cliente ya fue asignado a otro registro. Haz clic en el botón actualizar para obtener un número disponible.');
        this.fetchNextNo();
      } else if (msg) {
        this.modalError.set(msg);
      } else {
        this.modalError.set('Ocurrió un error al guardar el cliente. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }

  private emptyForm(): CustomerFormData {
    return {
      no: '', name: '', address: '', city: '', contact: '',
      phoneNo: '', email: '', creditLimit: 0, vatRegistrationNo: '',
      paymentTermsCode: '', paymentMethodCode: '', salespersonCode: '',
      currencyCode: '', customerPostingGroup: 'DOMESTIC', countryRegionCode: 'DO'
    };
  }
}
