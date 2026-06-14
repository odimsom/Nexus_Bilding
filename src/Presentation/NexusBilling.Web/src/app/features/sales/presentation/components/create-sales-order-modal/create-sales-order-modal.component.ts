import { Component, inject, signal, computed, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { InvoiceService } from '../../../data/invoice.service';
import { ItemService, ItemListItem } from '../../../../inventory/data/item.service';
import { CustomerService, CustomerListItem } from '../../../../customers/data/customer.service';
import { PaymentTermsService } from '../../../../../core/services/payment-terms.service';
import { ApiService } from '../../../../../core/services/api.service';

interface ModalLine {
  itemNo: string;
  description: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
  unitOfMeasure: string;
  type: 'Item' | 'Service' | 'G/L Account';
  suggestions: ItemListItem[];
  showSuggestions: boolean;
}

interface Installment {
  no: number;
  dueDate: string;
  amount: number;
  paid: boolean;
  paidDate: string;
}

@Component({
  selector: 'app-create-sales-order-modal',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="modal-backdrop" (click)="onClose()">
      <div class="modal-box modal-box--wide" (click)="$event.stopPropagation()">

        <div class="modal-header">
          <div>
            <div class="nx-eyebrow" style="margin-bottom:var(--nx-space-1);">{{ docTypeFullLabel(form.documentType) }}</div>
            <h2 class="nx-page-title" style="margin:0;font-size:var(--nx-text-xl);">
              @if (fixedCustomerName) {
                Para <span style="color:var(--nx-action);">{{ fixedCustomerName }}</span>
              } @else {
                Nueva Orden de Venta
              }
            </h2>
          </div>
          <button class="nx-iconbtn" (click)="onClose()">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/>
            </svg>
          </button>
        </div>

        <div class="modal-body">
          @if (errorMsg()) {
            <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">
              <strong>No se pudo crear el documento.</strong>
              <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">{{ errorMsg() }}</p>
            </div>
          }
          @if (successNo()) {
            <div class="nx-callout nx-callout--success" style="margin-bottom:var(--nx-space-4);">
              <strong>Documento creado correctamente.</strong>
              <p style="margin:var(--nx-space-1) 0 0;font-size:var(--nx-text-sm);">
                No. <span style="font-family:var(--nx-font-mono);">{{ successNo() }}</span>
                &mdash; <a [routerLink]="docLink(successNo()!)" class="nx-link">Ver documento</a>
              </p>
            </div>
          }

          <!-- Header fields -->
          <div class="form-grid" style="margin-bottom:var(--nx-space-4);">
            <div class="nx-field">
              <label class="nx-label">Tipo de Documento <span style="color:var(--nx-red-500);">*</span></label>
              <select class="nx-select" [(ngModel)]="form.documentType">
                <option value="Order">Orden de Venta</option>
                <option value="Invoice">Factura Directa</option>
                <option value="Quote">Cotización</option>
              </select>
            </div>

            @if (!fixedCustomerNo) {
              <div class="nx-field" style="position:relative;">
                <label class="nx-label">Cliente <span style="color:var(--nx-red-500);">*</span></label>
                <input class="nx-input" placeholder="Buscar cliente…"
                  [(ngModel)]="customerSearch"
                  (input)="searchCustomers()"
                  (blur)="hideCustomerDrop()"
                  autocomplete="off" />
                @if (showCustomerDrop() && customerSugg().length > 0) {
                  <div class="item-dropdown">
                    @for (c of customerSugg(); track c.no) {
                      <button class="item-dropdown__item" (mousedown)="selectCustomer(c)">
                        <span style="font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-action);">{{ c.no }}</span>
                        <span style="flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ c.name }}</span>
                      </button>
                    }
                  </div>
                }
              </div>
            }

            <div class="nx-field">
              <label class="nx-label">Ref. del cliente</label>
              <input class="nx-input" [(ngModel)]="form.externalDocumentNo" placeholder="No. de orden del cliente" />
            </div>
            <div class="nx-field">
              <label class="nx-label">Fecha <span style="color:var(--nx-red-500);">*</span></label>
              <input class="nx-input" type="date" [(ngModel)]="form.postingDate" />
            </div>
            <div class="nx-field">
              <label class="nx-label">Fecha de Vencimiento</label>
              <input class="nx-input" type="date" [(ngModel)]="form.dueDate" />
            </div>
            <div class="nx-field">
              <label class="nx-label">Condición de Pago</label>
              <select class="nx-select" [(ngModel)]="form.paymentTermsCode" (ngModelChange)="onPaymentTermsChange($event)">
                <option value="">Seleccionar…</option>
                @for (term of paymentTermsSvc.terms(); track term.code) {
                  <option [value]="term.code">{{ term.description }}</option>
                }
              </select>
            </div>
          </div>

          <!-- Lines -->
          <div style="margin-bottom:var(--nx-space-3);">
            <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-2);">
              <span style="font-weight:var(--nx-weight-semibold);font-size:var(--nx-text-sm);">Líneas <span style="color:var(--nx-red-500);">*</span></span>
              <div style="display:flex;gap:var(--nx-space-2);">
                <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine('Item')">+ Artículo</button>
                <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine('Service')">+ Servicio</button>
              </div>
            </div>
            <div style="overflow-x:auto;">
              <table class="nx-table" style="min-width:720px;">
                <thead>
                  <tr>
                    <th style="width:34px;">#</th>
                    <th style="width:70px;">Tipo</th>
                    <th style="min-width:120px;">No. / Ref.</th>
                    <th style="min-width:200px;">Descripción</th>
                    <th style="width:55px;">U/M</th>
                    <th class="nx-th--num" style="width:65px;">Cant.</th>
                    <th class="nx-th--num" style="width:96px;">Precio</th>
                    <th class="nx-th--num" style="width:58px;">Desc.%</th>
                    <th class="nx-th--num" style="width:106px;">Importe</th>
                    <th style="width:34px;"></th>
                  </tr>
                </thead>
                <tbody>
                  @for (line of lines; track $index; let i = $index) {
                    <tr>
                      <td style="color:var(--nx-text-faint);font-size:var(--nx-text-xs);font-family:var(--nx-font-mono);">{{ (i+1)*10000 }}</td>
                      <td>
                        <span class="nx-badge nx-badge--outline" style="font-size:10px;">{{ line.type === 'Item' ? 'Art.' : 'Serv.' }}</span>
                      </td>
                      <td style="position:relative;">
                        <input class="nx-input nx-input--sm" style="width:100%;font-family:var(--nx-font-mono);"
                          [(ngModel)]="line.itemNo"
                          [placeholder]="line.type === 'Service' ? 'Clic para ver servicios' : 'Clic para ver artículos'"
                          (focus)="onItemFocus(i)"
                          (input)="onItemInput(i)"
                          (blur)="closeSuggestionsDelayed(i)"
                          autocomplete="off" />
                        @if (line.showSuggestions && line.suggestions.length > 0) {
                          <div class="item-dropdown">
                            @for (s of line.suggestions; track s.no) {
                              <button class="item-dropdown__item" (mousedown)="selectSuggestion(i, s)">
                                <span style="font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-action);">{{ s.no }}</span>
                                <span style="flex:1;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ s.description }}</span>
                                <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ s.unitPrice | number:'1.2-2' }}</span>
                              </button>
                            }
                          </div>
                        }
                      </td>
                      <td>
                        <input class="nx-input nx-input--sm" [(ngModel)]="line.description" placeholder="Descripción" style="width:100%;" />
                      </td>
                      <td>
                        <input class="nx-input nx-input--sm" [(ngModel)]="line.unitOfMeasure" style="width:50px;" placeholder="UND" />
                      </td>
                      <td>
                        <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.quantity" type="number" min="0.001" step="0.001" style="width:60px;" />
                      </td>
                      <td>
                        <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.unitPrice" type="number" min="0" step="0.01" style="width:90px;" />
                      </td>
                      <td>
                        <input class="nx-input nx-input--sm nx-num" [(ngModel)]="line.lineDiscountPct" type="number" min="0" max="100" style="width:52px;" />
                      </td>
                      <td class="nx-td--num">
                        <span class="nx-num">{{ lineAmount(line) | number:'1.2-2' }}</span>
                      </td>
                      <td>
                        <button class="nx-iconbtn nx-iconbtn--sm" (click)="removeLine(i)" style="color:var(--nx-red-500);">
                          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                            <polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/>
                          </svg>
                        </button>
                      </td>
                    </tr>
                  }
                  @if (lines.length === 0) {
                    <tr>
                      <td colspan="10" style="text-align:center;color:var(--nx-text-muted);padding:var(--nx-space-5);">
                        Agrega productos o servicios con los botones de arriba
                      </td>
                    </tr>
                  }
                </tbody>
                <tfoot>
                  <tr>
                    <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">Subtotal:</td>
                    <td class="nx-td--num nx-num">{{ subtotal() | number:'1.2-2' }}</td>
                    <td></td>
                  </tr>
                  <tr>
                    <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-medium);color:var(--nx-text-muted);font-size:var(--nx-text-sm);">ITBIS (18%):</td>
                    <td class="nx-td--num nx-num">{{ itbis() | number:'1.2-2' }}</td>
                    <td></td>
                  </tr>
                  <tr>
                    <td colspan="8" style="text-align:right;font-weight:var(--nx-weight-bold);">Total c/ITBIS:</td>
                    <td class="nx-td--num nx-num" style="font-weight:var(--nx-weight-bold);font-size:1.05rem;">{{ total() | number:'1.2-2' }}</td>
                    <td></td>
                  </tr>
                </tfoot>
              </table>
            </div>
          </div>

          <!-- Panel de Cuotas -->
          @if (isInstallments()) {
          <div style="margin-top:var(--nx-space-3);padding:var(--nx-space-3);background:var(--nx-surface-raised);border-radius:var(--nx-radius-lg);border:1px solid var(--nx-border);">
            <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-3);">
              <span style="font-weight:var(--nx-weight-semibold);font-size:var(--nx-text-sm);">Plan de Cuotas</span>
              <div style="display:flex;align-items:center;gap:var(--nx-space-2);">
                <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">No. cuotas:</span>
                <input type="number" class="nx-input nx-input--sm" [(ngModel)]="installmentCount" min="1" max="60" style="width:60px;" />
                <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="generateInstallments()">Recalcular</button>
              </div>
            </div>
            @if (installments().length > 0) {
            <div style="overflow-x:auto;">
              <table class="nx-table" style="min-width:380px;">
                <thead>
                  <tr>
                    <th style="width:40px;">No.</th>
                    <th style="min-width:140px;">Fecha Venc.</th>
                    <th class="nx-th--num" style="min-width:120px;">Monto</th>
                    <th style="width:36px;"></th>
                  </tr>
                </thead>
                <tbody>
                  @for (inst of installments(); track inst.no; let i = $index) {
                  <tr>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ inst.no }}</td>
                    <td><input class="nx-input nx-input--sm" type="date" [(ngModel)]="inst.dueDate" /></td>
                    <td><input class="nx-input nx-input--sm nx-num" type="number" [(ngModel)]="inst.amount" min="0" step="0.01" style="text-align:right;" /></td>
                    <td>
                      <button class="nx-iconbtn nx-iconbtn--sm" style="color:var(--nx-red-400);"
                        (click)="installments.update(rows => rows.filter((_,j) => j !== i).map((r,j) => ({...r, no: j+1})))">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/></svg>
                      </button>
                    </td>
                  </tr>
                  }
                </tbody>
                <tfoot>
                  <tr>
                    <td colspan="2" style="text-align:right;font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Total cuotas:</td>
                    <td class="nx-td--num" style="font-weight:var(--nx-weight-bold);">{{ installmentTotal() | number:'1.2-2' }}</td>
                    <td></td>
                  </tr>
                </tfoot>
              </table>
            </div>
            }
          </div>
          }
        </div>

        <div class="modal-footer">
          <button class="nx-btn nx-btn--ghost" (click)="onClose()">Cancelar</button>
          <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="save()">
            @if (saving()) { Guardando… } @else { Crear {{ docTypeShortLabel(form.documentType) }} }
          </button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    :host { display: block; }
    .item-dropdown {
      position: absolute; bottom: 100%; top: auto; left: 0; right: 0; z-index: 200;
      background: var(--nx-surface); border: 1px solid var(--nx-border);
      border-radius: var(--nx-radius-md); box-shadow: var(--nx-shadow-lg);
      max-height: 220px; overflow-y: auto;
      margin-bottom: 2px;
    }
    .item-dropdown__item {
      display: flex; align-items: center; gap: var(--nx-space-2); width: 100%;
      padding: var(--nx-space-2) var(--nx-space-3); background: none; border: none;
      cursor: pointer; font-size: var(--nx-text-sm); text-align: left;
    }
    .item-dropdown__item:hover { background: var(--nx-surface-raised); }
  `]
})
export class CreateSalesOrderModalComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly invoiceSvc = inject(InvoiceService);
  private readonly itemSvc = inject(ItemService);
  private readonly customerSvc = inject(CustomerService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  private readonly api = inject(ApiService);

  // Pre-filled customer (when opened from customer card)
  @Input() fixedCustomerNo?: string;
  @Input() fixedCustomerName?: string;
  @Input() fixedPaymentTermsCode?: string;

  @Output() closed = new EventEmitter<void>();
  @Output() created = new EventEmitter<string>();

  // Form state
  form = {
    documentType: 'Order',
    externalDocumentNo: '',
    postingDate: new Date().toISOString().slice(0, 10),
    dueDate: '',
    paymentTermsCode: '',
  };

  // Customer search (when no fixedCustomerNo)
  customerSearch = '';
  resolvedCustomerNo = '';
  resolvedCustomerName = '';
  showCustomerDrop = signal(false);
  customerSugg = signal<CustomerListItem[]>([]);

  // Lines
  lines: ModalLine[] = [];
  private allItems: ItemListItem[] = [];

  // UI state
  saving = signal(false);
  errorMsg = signal('');
  successNo = signal<string | null>(null);

  // Installments
  installments = signal<Installment[]>([]);
  installmentCount = 3;
  isInstallments = computed(() => this.form.paymentTermsCode === 'CUOTAS');
  installmentTotal = computed(() => this.installments().reduce((s, i) => s + (i.amount || 0), 0));

  // Totals
  subtotal = computed(() => this.lines.reduce((s, l) => s + this.lineAmount(l), 0));
  itbis = computed(() => this.subtotal() * 0.18);
  total = computed(() => this.subtotal() * 1.18);

  async ngOnInit(): Promise<void> {
    await this.paymentTermsSvc.load();
    if (this.fixedPaymentTermsCode) {
      this.form.paymentTermsCode = this.fixedPaymentTermsCode;
    }
    this.loadAllItems();
  }

  private async loadAllItems(): Promise<void> {
    if (this.allItems.length > 0) return;
    try {
      await this.itemSvc.load({ pageSize: 500 });
      this.allItems = this.itemSvc.items();
    } catch { /* ignore */ }
  }

  // ── Customer search ──────────────────────────────────────────────
  async searchCustomers(): Promise<void> {
    const q = this.customerSearch.trim();
    this.resolvedCustomerNo = '';
    if (q.length < 1) { this.showCustomerDrop.set(false); return; }
    await this.customerSvc.load({ search: q, pageSize: 8 });
    this.customerSugg.set(this.customerSvc.items());
    this.showCustomerDrop.set(true);
  }

  selectCustomer(c: CustomerListItem): void {
    this.resolvedCustomerNo = c.no;
    this.resolvedCustomerName = c.name;
    this.customerSearch = c.name;
    if (!this.form.paymentTermsCode && c.paymentTermsCode) {
      this.form.paymentTermsCode = c.paymentTermsCode;
    }
    this.showCustomerDrop.set(false);
  }

  hideCustomerDrop(): void {
    setTimeout(() => this.showCustomerDrop.set(false), 150);
  }

  // ── Lines ───────────────────────────────────────────────────────
  addLine(type: 'Item' | 'Service' = 'Item'): void {
    this.lines = [...this.lines, {
      itemNo: '', description: '', quantity: 1, unitPrice: 0,
      lineDiscountPct: 0, unitOfMeasure: type === 'Item' ? 'UND' : 'HR',
      type, suggestions: [], showSuggestions: false,
    }];
  }

  removeLine(i: number): void {
    this.lines = this.lines.filter((_, idx) => idx !== i);
  }

  lineAmount(line: ModalLine): number {
    const base = line.quantity * line.unitPrice;
    return base - base * (line.lineDiscountPct / 100);
  }

  onItemFocus(i: number): void {
    const line = this.lines[i];
    const q = line.itemNo.toLowerCase();
    const isService = line.type === 'Service';
    const pool = this.allItems.filter(it => {
      const itType = (it.type ?? '').toLowerCase();
      return isService ? itType === 'service' : itType !== 'service';
    });
    const filtered = pool.filter(it =>
      !q || it.no.toLowerCase().includes(q) || it.description.toLowerCase().includes(q)
    ).slice(0, 8);
    this.lines[i] = { ...this.lines[i], suggestions: filtered, showSuggestions: filtered.length > 0 };
  }

  onItemInput(i: number): void {
    this.onItemFocus(i);
  }

  selectSuggestion(i: number, item: ItemListItem): void {
    this.lines[i] = {
      ...this.lines[i],
      itemNo: item.no,
      description: item.description,
      unitPrice: item.unitPrice,
      unitOfMeasure: item.baseUnitOfMeasure,
      suggestions: [],
      showSuggestions: false,
    };
  }

  closeSuggestionsDelayed(i: number): void {
    setTimeout(() => {
      if (this.lines[i]) this.lines[i] = { ...this.lines[i], showSuggestions: false };
    }, 200);
  }

  // ── Installments ─────────────────────────────────────────────────
  onPaymentTermsChange(code: string): void {
    if (code === 'CUOTAS') {
      this.generateInstallments();
    } else {
      this.installments.set([]);
    }
  }

  generateInstallments(): void {
    const totalAmt = this.total();
    const count = this.installmentCount;
    if (count <= 0) return;
    const amt = totalAmt > 0 ? Math.round((totalAmt / count) * 100) / 100 : 0;
    const today = new Date();
    this.installments.set(Array.from({ length: count }, (_, i) => {
      const d = new Date(today);
      d.setMonth(d.getMonth() + i + 1);
      return {
        no: i + 1,
        dueDate: d.toISOString().split('T')[0],
        amount: i === count - 1 && totalAmt > 0 ? Math.round((totalAmt - amt * (count - 1)) * 100) / 100 : amt,
        paid: false,
        paidDate: '',
      };
    }));
  }

  // ── Save ─────────────────────────────────────────────────────────
  async save(): Promise<void> {
    this.errorMsg.set('');

    const customerNo = this.fixedCustomerNo ?? this.resolvedCustomerNo;
    const customerName = this.fixedCustomerName ?? this.resolvedCustomerName;

    if (!customerNo) {
      this.errorMsg.set('Debe seleccionar un cliente.');
      return;
    }
    if (!this.form.postingDate) {
      this.errorMsg.set('La fecha es obligatoria.');
      return;
    }
    if (this.lines.length === 0) {
      this.errorMsg.set('Debes agregar al menos una línea.');
      return;
    }
    const emptyLine = this.lines.find(l => !l.description?.trim());
    if (emptyLine) {
      this.errorMsg.set('Todas las líneas deben tener una descripción.');
      return;
    }
    const zeroQty = this.lines.find(l => !l.quantity || l.quantity <= 0);
    if (zeroQty) {
      this.errorMsg.set('Todas las líneas deben tener una cantidad mayor a cero.');
      return;
    }

    this.saving.set(true);
    try {
      const seriesCode = { Order: 'ORD', Quote: 'COT', Invoice: 'FAC' }[this.form.documentType] ?? 'ORD';
      const no = await this.invoiceSvc.createOrder({
        documentType: this.form.documentType,
        sellToCustomerNo: customerNo,
        sellToCustomerName: customerName,
        externalDocumentNo: this.form.externalDocumentNo,
        currencyCode: '',
        paymentTermsCode: this.form.paymentTermsCode,
        paymentMethodCode: '',
        salespersonCode: '',
        postingDate: this.form.postingDate,
        dueDate: this.form.dueDate || null,
        seriesCode,
        lines: this.lines.map(l => ({
          itemNo: l.itemNo,
          description: l.description,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct,
          unitOfMeasure: l.unitOfMeasure,
          lineType: l.type,
        })),
      });

      if (this.installments().length > 0) {
        localStorage.setItem(`nx_inst_${no}`, JSON.stringify(this.installments()));
      }

      this.successNo.set(no);
      this.created.emit(no);
      this.lines = [];
      this.installments.set([]);

      // Navigate to the created document after a brief pause
      setTimeout(() => {
        const route = this.form.documentType === 'Quote' ? '/quotations' :
                      this.form.documentType === 'Invoice' ? '/invoices' : '/sales';
        this.router.navigate([route, no]);
      }, 1200);
    } catch (e: any) {
      const msg: string = e?.error?.error?.message ?? '';
      if (msg.toLowerCase().includes('serie') || msg.toLowerCase().includes('sequence')) {
        this.errorMsg.set('No hay una secuencia de numeración configurada. Ve a Configuración > Secuencias para crearla.');
      } else if (msg) {
        this.errorMsg.set(msg);
      } else {
        this.errorMsg.set('Ocurrió un error al guardar. Intenta de nuevo.');
      }
    } finally {
      this.saving.set(false);
    }
  }

  // ── Utils ────────────────────────────────────────────────────────
  onClose(): void {
    this.closed.emit();
  }

  docTypeFullLabel(t: string): string {
    return { Order: 'Nueva Orden de Venta', Invoice: 'Nueva Factura Directa', Quote: 'Nueva Cotización' }[t] ?? 'Nuevo Documento';
  }

  docTypeShortLabel(t: string): string {
    return { Order: 'Orden', Invoice: 'Factura', Quote: 'Cotización' }[t] ?? 'Documento';
  }

  docLink(no: string): string[] {
    const route = this.form.documentType === 'Quote' ? '/quotations' :
                  this.form.documentType === 'Invoice' ? '/invoices' : '/sales';
    return [route, no];
  }
}
