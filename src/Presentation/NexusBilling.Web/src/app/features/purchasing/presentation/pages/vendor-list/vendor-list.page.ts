import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VendorService } from '../../../data/vendor.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

@Component({
  selector: 'app-vendor-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './vendor-list.page.html',
  styleUrl: './vendor-list.page.css'
})
export class VendorListPage implements OnInit {
  readonly svc = inject(VendorService);
  private readonly router = inject(Router);
  private readonly excel = inject(ExcelExportService);

  showNewModal = signal(false);
  saving = signal(false);
  createError = signal('');
  newForm = { name: '', address: '', address2: '', city: '', province: '', country: '', contact: '', phoneNo: '', phoneNo2: '', email: '', webSite: '', rnc: '', paymentTermsCode: '', paymentMethodCode: '', currencyCode: '', creditLimit: 0, vendorType: '' };

  sortField = 'name';
  sortAsc = true;

  sorted() {
    return [...this.svc.items()].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (this.sortField) {
        case 'no':        av = a.no || '';        bv = b.no || '';        break;
        case 'name':      av = a.name || '';      bv = b.name || '';      break;
        case 'contact':   av = a.contact || '';   bv = b.contact || '';   break;
        case 'city':      av = a.city || '';      bv = b.city || '';      break;
        case 'blocked':   av = a.blocked ? '1' : '0'; bv = b.blocked ? '1' : '0'; break;
        default:          av = a.name || '';      bv = b.name || '';
      }
      return this.sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
    });
  }

  setSort(f: string): void {
    if (this.sortField === f) this.sortAsc = !this.sortAsc;
    else { this.sortField = f; this.sortAsc = true; }
  }

  si(f: string): string {
    return this.sortField === f ? (this.sortAsc ? '↑' : '↓') : '';
  }

  async ngOnInit() {
    await this.svc.load();
  }

  async exportExcel(): Promise<void> {
    const data = this.svc.items().map(v => ({
      no: v.no,
      name: v.name,
      contact: v.contact || '',
      city: v.city || '',
      status: v.blocked ? 'Bloqueado' : 'Activo'
    }));

    await this.excel.exportAsExcel({
      filename: 'Proveedores_Nexus',
      title: 'Catálogo de Proveedores',
      subtitle: 'Nexus Billing - Módulo de Compras',
      sheetName: 'Proveedores',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Nombre', key: 'name', width: 45 },
        { header: 'Contacto', key: 'contact', width: 25 },
        { header: 'Ciudad', key: 'city', width: 20 },
        { header: 'Estado', key: 'status', width: 12 }
      ],
      data
    });
  }

  openNew() {
    this.newForm = { name: '', address: '', address2: '', city: '', province: '', country: '', contact: '', phoneNo: '', phoneNo2: '', email: '', webSite: '', rnc: '', paymentTermsCode: '', paymentMethodCode: '', currencyCode: '', creditLimit: 0, vendorType: '' };
    this.createError.set('');
    this.showNewModal.set(true);
  }

  closeNew() {
    this.showNewModal.set(false);
  }

  async saveNew() {
    if (!this.newForm.name) {
      this.createError.set('El nombre es obligatorio.');
      return;
    }
    this.saving.set(true);
    this.createError.set('');
    try {
      await this.svc.create(this.newForm);
      await this.svc.load();
      this.closeNew();
    } catch (e: any) {
      this.createError.set(e?.error?.error?.message ?? 'Ocurrió un error inesperado al guardar.');
    } finally {
      this.saving.set(false);
    }
  }
}
