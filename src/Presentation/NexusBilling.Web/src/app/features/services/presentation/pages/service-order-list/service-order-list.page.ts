import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { ServiceOrderService, CreateServiceOrderData } from '../../../data/service-order.service';
import { ServiceOrderListItem } from '../../../domain/service-order.model';
import { sortBy } from '../../../../../shared/utils/sort.utils';
import { ApiService } from '../../../../../core/services/api.service';
import { CustomerService } from '../../../../customers/data/customer.service';
import { ItemService, ItemListItem } from '../../../../inventory/data/item.service';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { PaymentMethodService } from '../../../../../core/services/payment-method.service';
import { CurrencyService } from '../../../../../core/services/currency.service';
import { ExcelExportService } from '../../../../../core/services/excel/excel-export.service';

interface ServiceLine {
  no: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  lineType: number;
  lineTypeLabel: string;
  suggestions: ItemListItem[];
  showSuggestions: boolean;
}

@Component({
  selector: 'app-service-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './service-order-list.page.html',
  styleUrl: './service-order-list.page.css'
})
export class ServiceOrderListPage implements OnInit {
  readonly svc = inject(ServiceOrderService);
  private readonly api = inject(ApiService);
  private readonly customerSvc = inject(CustomerService);
  private readonly itemSvc = inject(ItemService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly paymentMethodSvc = inject(PaymentMethodService);
  readonly currencySvc = inject(CurrencyService);
  private readonly excel = inject(ExcelExportService);

  searchText = '';
  activeTab = signal<string>('all');
  filtered = signal<ServiceOrderListItem[]>([]);

  sortField = 'no';
  sortAsc = false;

  sortedFiltered(): ServiceOrderListItem[] {
    return sortBy(this.filtered(), this.sortField, this.sortAsc);
  }

  setSort(f: string): void {
    if (this.sortField === f) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortField = f;
      this.sortAsc = true;
    }
  }

  si(f: string): string {
    if (this.sortField !== f) return '';
    return this.sortAsc ? '↑' : '↓';
  }

  showNew = signal(false);
  saving = signal(false);
  formError = signal<string | null>(null);
  formSuccess = signal<string | null>(null);
  loadingNextNo = signal(false);
  private previewNo = '';

  form = {
    documentType: 1,
    documentNo: '',
    customerNo: '',
    customerSearch: '',
    customerName: '',
    description: '',
    orderDate: new Date().toISOString().slice(0, 10),
    startingDate: '',
    finishingDate: '',
    contractNo: '',
    paymentTermsCode: '',
    paymentMethodCode: '',
    salespersonCode: '',
  };

  lines: ServiceLine[] = [];
  private allItems: ItemListItem[] = [];

  showCustomerSuggestions = signal(false);
  customerSuggestions = signal<{ no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }[]>([]);
  private allCustomers: { no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }[] = [];

  readonly tabs = [
    { id: 'all', label: 'Todos' },
    { id: '0', label: 'Pendiente' },
    { id: '1', label: 'En Proceso' },
    { id: '2', label: 'Terminado' },
    { id: '3', label: 'En Espera' },
  ];

  async ngOnInit(): Promise<void> {
    const customerNo = this.route.snapshot.queryParamMap.get('customerNo');
    if (customerNo) this.searchText = customerNo;
    this.paymentTermsSvc.load();
    this.paymentMethodSvc.load();
    this.currencySvc.load();
    await this.svc.load();
    this.applyFilter();
  }

  async reload(): Promise<void> {
    await this.svc.load();
    this.applyFilter();
  }

  setTab(id: string): void {
    this.activeTab.set(id);
    this.applyFilter();
  }

  applyFilter(): void {
    let items = this.svc.orders();
    const tab = this.activeTab();
    if (tab !== 'all') items = items.filter(o => String(o.status) === tab);
    const q = this.searchText.toLowerCase().trim();
    if (q) items = items.filter(o =>
      o.no.toLowerCase().includes(q) ||
      o.customerNo.toLowerCase().includes(q) ||
      o.customerName.toLowerCase().includes(q) ||
      o.description.toLowerCase().includes(q));
    this.filtered.set(items);
  }

  countForTab(id: string): number {
    const all = this.svc.orders();
    return id === 'all' ? all.length : all.filter(o => String(o.status) === id).length;
  }

  statusClass(status: number): string {
    return ({ 0: 'nx-badge--outline', 1: 'nx-badge--info', 2: 'nx-badge--success', 3: 'nx-badge--warn' } as Record<number, string>)[status] ?? 'nx-badge--outline';
  }

  docTypeLabel(t: number): string {
    return ({ 0: 'Nueva Cotización de Servicio', 1: 'Nueva Orden de Servicio', 2: 'Nueva Factura de Servicio' } as Record<number, string>)[t] ?? 'Nuevo Documento';
  }

  docTypeShortLabel(t: number): string {
    return ({ 0: 'Cotización', 1: 'Orden', 2: 'Factura' } as Record<number, string>)[t] ?? 'Documento';
  }

  lineTypeLabel(t: number): string {
    return ({ 1: 'Art.', 2: 'Rec.', 3: 'Costo', 4: 'CG' } as Record<number, string>)[t] ?? '';
  }

  linePlaceholder(t: number): string {
    return ({ 1: 'ART-00001', 2: 'REC-001', 3: 'COSTO-01', 4: '5010' } as Record<number, string>)[t] ?? '';
  }

  private seriesForDocType(t: number): string {
    return ({ 0: 'SCOT', 1: 'OSV', 2: 'SFAC' } as Record<number, string>)[t] ?? 'OSV';
  }

  openNew(): void {
    this.lines = [];
    this.formError.set(null);
    this.formSuccess.set(null);
    this.form.documentNo = '';
    this.form.customerNo = '';
    this.form.customerSearch = '';
    this.form.customerName = '';
    this.form.description = '';
    this.form.orderDate = new Date().toISOString().slice(0, 10);
    this.form.startingDate = '';
    this.form.finishingDate = '';
    this.form.contractNo = '';
    this.showNew.set(true);
    this.loadAllItems();
    this.loadAllCustomers();
    this.refreshNextNo();
  }

  closeNew(): void { this.showNew.set(false); }

  async onDocTypeChange(): Promise<void> {
    this.form.documentNo = '';
    await this.refreshNextNo();
  }

  async refreshNextNo(): Promise<void> {
    const series = this.seriesForDocType(this.form.documentType);
    this.loadingNextNo.set(true);
    try {
      const res = await firstValueFrom(this.api.get<{ code: string; nextNo: string }>(`administration/no-series/${series}/next`));
      this.previewNo = res.nextNo;
      this.form.documentNo = res.nextNo;
    } catch {
      this.previewNo = '';
      this.form.documentNo = '';
    } finally {
      this.loadingNextNo.set(false);
    }
  }

  private async loadAllItems(): Promise<void> {
    if (this.allItems.length > 0) return;
    try {
      await this.itemSvc.load({ pageSize: 500 });
      this.allItems = this.itemSvc.items();
    } catch { /* ignore */ }
  }

  private async loadAllCustomers(): Promise<void> {
    if (this.allCustomers.length > 0) return;
    try {
      await this.customerSvc.load({ pageSize: 500 });
      this.allCustomers = this.customerSvc.items().map(c => ({
        no: c.no, name: c.name,
        paymentTermsCode: c.paymentTermsCode,
        paymentMethodCode: (c as any).paymentMethodCode || '',
        salespersonCode: c.salespersonCode,
      }));
    } catch { /* ignore */ }
  }

  onCustomerFocus(): void {
    const q = this.form.customerSearch.trim().toLowerCase();
    if (!q) {
      this.customerSuggestions.set(this.allCustomers.slice(0, 10));
      this.showCustomerSuggestions.set(this.allCustomers.length > 0);
    }
  }

  onCustomerInput(): void {
    const q = this.form.customerSearch.trim().toLowerCase();
    if (!q) {
      this.customerSuggestions.set(this.allCustomers.slice(0, 10));
      this.showCustomerSuggestions.set(this.allCustomers.length > 0);
      return;
    }
    const matches = this.allCustomers.filter(c => c.no.toLowerCase().includes(q) || c.name.toLowerCase().includes(q)).slice(0, 8);
    this.customerSuggestions.set(matches);
    this.showCustomerSuggestions.set(matches.length > 0);
  }

  selectCustomer(c: { no: string; name: string; paymentTermsCode: string; paymentMethodCode: string; salespersonCode: string }): void {
    this.form.customerNo = c.no;
    this.form.customerName = c.name;
    this.form.customerSearch = `${c.no} · ${c.name}`;
    this.form.paymentTermsCode = c.paymentTermsCode || this.form.paymentTermsCode;
    this.form.paymentMethodCode = c.paymentMethodCode || this.form.paymentMethodCode;
    this.form.salespersonCode = c.salespersonCode || this.form.salespersonCode;
    this.showCustomerSuggestions.set(false);
  }

  closeCustomerSuggestionsDelayed(): void {
    setTimeout(() => this.showCustomerSuggestions.set(false), 200);
  }

  addLine(lineType: number): void {
    this.lines = [...this.lines, {
      no: '', description: '', quantity: 1, unitPrice: 0,
      lineDiscountPct: 0, unitOfMeasure: 'UND',
      lineType, lineTypeLabel: this.lineTypeLabel(lineType),
      suggestions: [], showSuggestions: false
    }];
  }

  removeLine(i: number): void {
    this.lines = this.lines.filter((_, idx) => idx !== i);
  }

  onLineFocus(i: number): void {
    if (this.lines[i].lineType !== 1) return;
    const q = this.lines[i].no.trim();
    if (!q) {
      const suggestions = this.allItems.slice(0, 10);
      this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
    }
  }

  onLineInput(i: number): void {
    if (this.lines[i].lineType !== 1) return;
    const q = this.lines[i].no.toLowerCase().trim();
    if (!q) {
      const suggestions = this.allItems.slice(0, 10);
      this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
      return;
    }
    const suggestions = this.allItems.filter(it => it.no.toLowerCase().includes(q) || it.description.toLowerCase().includes(q)).slice(0, 8);
    this.lines[i] = { ...this.lines[i], suggestions, showSuggestions: suggestions.length > 0 };
  }

  selectLineSuggestion(i: number, item: ItemListItem): void {
    this.lines[i] = {
      ...this.lines[i],
      no: item.no, description: item.description,
      unitPrice: item.unitPrice, unitOfMeasure: item.baseUnitOfMeasure,
      suggestions: [], showSuggestions: false,
    };
  }

  closeLineSuggestionsDelayed(i: number): void {
    setTimeout(() => {
      if (this.lines[i]) this.lines[i] = { ...this.lines[i], showSuggestions: false };
    }, 200);
  }

  lineAmount(line: ServiceLine): number {
    const base = line.quantity * line.unitPrice;
    return base - (base * (line.lineDiscountPct / 100));
  }

  subtotal(): number { return this.lines.reduce((s, l) => s + this.lineAmount(l), 0); }
  itbis(): number { return this.subtotal() * 0.18; }
  total(): number { return this.subtotal() * 1.18; }

  async exportExcel(): Promise<void> {
    await this.excel.exportAsExcel({
      filename: 'Ordenes_Servicio',
      title: 'Órdenes de Servicio',
      subtitle: 'Nexus Billing - Módulo de Servicios',
      sheetName: 'Servicios',
      columns: [
        { header: 'No.', key: 'no', width: 15 },
        { header: 'Tipo', key: 'tipo', width: 14 },
        { header: 'Cliente No.', key: 'customerNo', width: 14 },
        { header: 'Cliente', key: 'customerName', width: 35 },
        { header: 'Descripción', key: 'description', width: 35 },
        { header: 'Fecha', key: 'fecha', width: 14 },
        { header: 'Estado', key: 'estado', width: 14 },
      ],
      data: this.sortedFiltered().map(o => ({
        no: o.no,
        tipo: o.documentType,
        customerNo: o.customerNo,
        customerName: o.customerName,
        description: o.description,
        fecha: o.orderDate ?? '',
        estado: o.statusLabel,
      })),
    });
  }

  async save(): Promise<void> {
    this.formError.set(null);
    this.formSuccess.set(null);

    if (!this.form.customerNo) { this.formError.set('Debes seleccionar un cliente.'); return; }
    if (!this.form.orderDate) { this.formError.set('La fecha de la orden es obligatoria.'); return; }
    if (this.lines.length === 0) { this.formError.set('Debes agregar al menos una línea.'); return; }
    if (this.lines.some(l => !l.description?.trim())) { this.formError.set('Todas las líneas deben tener descripción.'); return; }
    if (this.lines.some(l => !l.quantity || l.quantity <= 0)) { this.formError.set('Todas las líneas deben tener cantidad mayor a cero.'); return; }

    this.saving.set(true);
    try {
      const typedNo = this.form.documentNo?.trim() || '';
      const useManual = typedNo && typedNo !== this.previewNo;
      const seriesCode = this.seriesForDocType(this.form.documentType);
      const data: CreateServiceOrderData = {
        documentType: this.form.documentType,
        customerNo: this.form.customerNo,
        customerName: this.form.customerName,
        description: this.form.description,
        orderDate: this.form.orderDate,
        startingDate: this.form.startingDate || null,
        finishingDate: this.form.finishingDate || null,
        paymentTermsCode: this.form.paymentTermsCode,
        paymentMethodCode: this.form.paymentMethodCode,
        salespersonCode: this.form.salespersonCode,
        currencyCode: '',
        contractNo: this.form.contractNo,
        seriesCode: useManual ? null : seriesCode,
        manualNo: useManual ? typedNo : null,
        lines: this.lines.map(l => ({
          no: l.no, description: l.description,
          quantity: l.quantity, unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct, unitOfMeasure: l.unitOfMeasure,
          lineType: l.lineType,
        })),
      };
      const no = await this.svc.create(data);
      this.formSuccess.set(no);
      this.lines = [];
      await this.reload();
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('sequence')) {
        this.formError.set('No hay secuencia configurada para este tipo de documento. Ve a Configuración > Secuencias.');
      } else if (msg) {
        this.formError.set(msg);
      } else {
        this.formError.set('Error al guardar. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }
}
