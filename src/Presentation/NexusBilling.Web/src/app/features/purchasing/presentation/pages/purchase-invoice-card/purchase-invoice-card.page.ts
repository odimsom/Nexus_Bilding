import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';
import { VendorService } from '../../../data/vendor.service';
import { PurchaseInvoiceDetail, CreateInvoiceLineForm } from '../../../domain/purchase-invoice.model';
import { Vendor } from '../../../domain/vendor.model';

@Component({
  selector: 'app-purchase-invoice-card',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, CurrencyPipe, DatePipe],
  template: `
    <div class="h-full flex flex-col animate-fade-in max-w-5xl mx-auto w-full" style="padding:var(--nx-space-6);">

      <!-- ===== NEW INVOICE FORM ===== -->
      @if (isNew()) {
        <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
          <a routerLink="/dashboard">Dashboard</a>
          <span class="nx-crumbs__sep">›</span>
          <a routerLink="/purchase-invoices">Facturas de Compra</a>
          <span class="nx-crumbs__sep">›</span>
          <span class="nx-crumb--current">Nueva Factura</span>
        </nav>

        <div class="nx-page-header" style="margin-bottom:var(--nx-space-5);">
          <div>
            <h1 class="nx-page-title">Nueva Factura de Compra</h1>
            <p class="nx-page-subtitle">Complete los datos y agregue las líneas antes de registrar</p>
          </div>
          <div class="nx-page-actions">
            <button class="nx-btn nx-btn--ghost" routerLink="/purchase-invoices">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving() || !newForm.buyFromVendorNo || newLines().length === 0" (click)="saveInvoice()">
              @if (saving()) { Registrando… } @else { Registrar Factura }
            </button>
          </div>
        </div>

        @if (errorMsg()) {
          <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">{{ errorMsg() }}</div>
        }

        <div class="nx-card" style="margin-bottom:var(--nx-space-4);">
          <div class="nx-card__head"><div class="nx-card__title">Datos del Proveedor y Factura</div></div>
          <div class="card-body-pad">
            <div class="new-grid">
              <div class="nx-field" style="position:relative;grid-column:1/-1;">
                <label class="nx-label">Proveedor *</label>
                <input
                  class="nx-input"
                  [(ngModel)]="newForm.buyFromVendorNo"
                  placeholder="Haz clic o escribe para buscar…"
                  autocomplete="off"
                  (focus)="onVendorFocus()"
                  (input)="onVendorInput()"
                  (blur)="hideVendorDrop()"
                />
                @if (showVendorDrop() && vendorSugg().length > 0) {
                  <div class="vendor-drop">
                    @for (v of vendorSugg(); track v.no) {
                      <button type="button" class="vendor-drop__item" (mousedown)="selectVendor(v)">
                        <span class="vendor-drop__no">{{ v.no }}</span>
                        <span class="vendor-drop__name">{{ v.name }}</span>
                        @if (v.city) { <span class="vendor-drop__city">{{ v.city }}</span> }
                      </button>
                    }
                  </div>
                }
              </div>
              <div class="nx-field">
                <label class="nx-label">Nombre del Proveedor</label>
                <input class="nx-input" [(ngModel)]="newForm.payToName" placeholder="Se rellena automáticamente" />
              </div>
              <div class="nx-field">
                <label class="nx-label">No. Ref. Proveedor</label>
                <input class="nx-input" [(ngModel)]="newForm.externalDocumentNo" placeholder="No. de factura del proveedor" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Fecha de Registro</label>
                <input class="nx-input" type="date" [(ngModel)]="newForm.postingDate" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Condición de Pago</label>
                <select class="nx-input" [(ngModel)]="newForm.paymentTermsCode">
                  <option value="">— Sin especificar —</option>
                  <option value="CM">Contado</option>
                  <option value="30">30 días</option>
                  <option value="60">60 días</option>
                  <option value="90">90 días</option>
                </select>
              </div>
            </div>
          </div>
        </div>

        <div class="nx-card" style="margin-bottom:var(--nx-space-4);">
          <div class="nx-card__head" style="justify-content:space-between;">
            <div class="nx-card__title">Líneas de Factura</div>
            <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine()">+ Agregar Línea</button>
          </div>
          <div style="overflow-x:auto;">
            <table class="nx-table">
              <thead>
                <tr>
                  <th>Descripción *</th>
                  <th style="width:90px;">Cantidad *</th>
                  <th style="width:130px;">Costo Unit. *</th>
                  <th style="width:80px;">UM</th>
                  <th style="width:90px;">ITBIS %</th>
                  <th style="width:120px;text-align:right;">Total</th>
                  <th style="width:44px;"></th>
                </tr>
              </thead>
              <tbody>
                @for (line of newLines(); track $index; let i = $index) {
                  <tr>
                    <td><input class="nx-input nx-input--inline" [(ngModel)]="line.description" placeholder="Descripción del artículo o servicio" /></td>
                    <td><input class="nx-input nx-input--inline nx-input--num" type="number" [(ngModel)]="line.quantity" min="0.001" step="1" /></td>
                    <td><input class="nx-input nx-input--inline nx-input--num" type="number" [(ngModel)]="line.unitCost" min="0" step="0.01" /></td>
                    <td><input class="nx-input nx-input--inline" [(ngModel)]="line.unitOfMeasureCode" placeholder="UND" style="max-width:70px;" /></td>
                    <td>
                      <select class="nx-input nx-input--inline" [(ngModel)]="line.vatPct">
                        <option [ngValue]="0">0%</option>
                        <option [ngValue]="18">18%</option>
                      </select>
                    </td>
                    <td style="text-align:right;font-variant-numeric:tabular-nums;white-space:nowrap;padding-right:var(--nx-space-3);">
                      {{ lineTotal(line) | currency:'DOP':'symbol':'1.2-2' }}
                    </td>
                    <td>
                      <button class="nx-iconbtn" style="color:var(--nx-red-500);" (click)="removeLine(i)">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="width:14px;height:14px;"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                      </button>
                    </td>
                  </tr>
                }
                @if (newLines().length === 0) {
                  <tr>
                    <td colspan="7" style="text-align:center;color:var(--nx-text-muted);padding:var(--nx-space-6);font-style:italic;">
                      No hay líneas. Haz clic en "Agregar Línea" para comenzar.
                    </td>
                  </tr>
                }
              </tbody>
              @if (newLines().length > 0) {
                <tfoot>
                  <tr style="border-top:2px solid var(--nx-border);">
                    <td colspan="5" style="text-align:right;color:var(--nx-text-muted);font-size:var(--nx-text-sm);padding:var(--nx-space-2) var(--nx-space-4);">Subtotal</td>
                    <td style="text-align:right;font-variant-numeric:tabular-nums;padding:var(--nx-space-2) var(--nx-space-3);">{{ subtotal() | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td></td>
                  </tr>
                  <tr>
                    <td colspan="5" style="text-align:right;color:var(--nx-text-muted);font-size:var(--nx-text-sm);padding:var(--nx-space-2) var(--nx-space-4);">ITBIS</td>
                    <td style="text-align:right;font-variant-numeric:tabular-nums;padding:var(--nx-space-2) var(--nx-space-3);">{{ totalVat() | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td></td>
                  </tr>
                  <tr style="font-weight:var(--nx-weight-bold);">
                    <td colspan="5" style="text-align:right;padding:var(--nx-space-3) var(--nx-space-4);">Total</td>
                    <td style="text-align:right;font-variant-numeric:tabular-nums;color:var(--nx-teal-400);padding:var(--nx-space-3);">{{ totalWithVat() | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td></td>
                  </tr>
                </tfoot>
              }
            </table>
          </div>
        </div>
      }

      @else if (loading()) {
        <div class="animate-pulse space-y-6">
          <div style="height:32px;background:var(--nx-surface-raised);border-radius:8px;width:25%;"></div>
          <div style="height:160px;background:var(--nx-surface);border-radius:12px;border:1px solid var(--nx-border);"></div>
          <div style="height:256px;background:var(--nx-surface);border-radius:12px;border:1px solid var(--nx-border);"></div>
        </div>
      }

      @else if (invoice()) {
        <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
          <a routerLink="/dashboard">Dashboard</a>
          <span class="nx-crumbs__sep">›</span>
          <a routerLink="/purchase-invoices">Facturas de Compra</a>
          <span class="nx-crumbs__sep">›</span>
          <span class="nx-crumb--current">{{ invoice()!.no }}</span>
        </nav>

        <div class="flex items-center justify-between mb-6">
          <div class="flex items-center gap-4">
            <button routerLink="/purchase-invoices" class="w-10 h-10 flex items-center justify-center rounded-xl bg-neutral-900 border border-neutral-800 text-neutral-400 hover:text-white hover:bg-neutral-800 transition-colors">
              <i class="fi fi-rr-arrow-left"></i>
            </button>
            <div>
              <div class="flex items-center gap-3 mb-1">
                <h1 class="text-3xl font-bold text-white">Factura {{ invoice()!.no }}</h1>
                <span class="px-2.5 py-1 bg-blue-500/10 text-blue-400 border border-blue-500/20 rounded-full text-xs font-medium">Registrada</span>
              </div>
              <p class="text-neutral-400">Proveedor: <span class="text-neutral-300">{{ invoice()!.payToName }}</span></p>
            </div>
          </div>
          <div class="flex items-center gap-3">
            <button class="px-4 py-2 bg-neutral-800 text-white rounded-lg hover:bg-neutral-700 transition-colors border border-neutral-700 font-medium text-sm flex items-center gap-2">
              <i class="fi fi-rr-print"></i> Imprimir
            </button>
          </div>
        </div>

        <div class="grid grid-cols-3 gap-6 mb-6">
          <div class="bg-neutral-900 border border-neutral-800 rounded-xl p-5 shadow-sm">
            <h3 class="text-sm font-medium text-neutral-400 uppercase tracking-wider mb-4">Proveedor</h3>
            <div class="space-y-3 text-sm">
              <div class="flex flex-col">
                <span class="text-neutral-500">No. Proveedor</span>
                <a [routerLink]="['/vendors', invoice()!.buyFromVendorNo]"
                   class="text-teal-400 font-medium hover:text-teal-300 transition-colors flex items-center gap-1 mt-0.5">
                  {{ invoice()!.buyFromVendorNo }}
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" style="width:12px;height:12px;"><path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"/><polyline points="15 3 21 3 21 9"/><line x1="10" y1="14" x2="21" y2="3"/></svg>
                </a>
              </div>
              <div class="flex flex-col">
                <span class="text-neutral-500">Nombre</span>
                <span class="text-neutral-200 font-medium">{{ invoice()!.payToName }}</span>
              </div>
            </div>
          </div>

          <div class="bg-neutral-900 border border-neutral-800 rounded-xl p-5 shadow-sm">
            <h3 class="text-sm font-medium text-neutral-400 uppercase tracking-wider mb-4">Detalles</h3>
            <div class="space-y-3 text-sm">
              <div class="flex flex-col">
                <span class="text-neutral-500">Fecha de Registro</span>
                <span class="text-neutral-200 font-medium">{{ invoice()!.postingDate | date:'dd/MM/yyyy' }}</span>
              </div>
              <div class="flex flex-col">
                <span class="text-neutral-500">Condición de Pago</span>
                <span class="text-neutral-200 font-medium">{{ invoice()!.paymentTermsCode || 'N/A' }}</span>
              </div>
            </div>
          </div>

          <div class="bg-gradient-to-br from-teal-900/40 to-blue-900/40 border border-teal-800/30 rounded-xl p-5 shadow-sm flex flex-col justify-center relative overflow-hidden">
            <div class="absolute -right-6 -bottom-6 opacity-10">
              <i class="fi fi-rr-sack-dollar text-8xl"></i>
            </div>
            <h3 class="text-sm font-medium text-teal-400/80 uppercase tracking-wider mb-2">Total Factura</h3>
            <div class="text-4xl font-bold text-white mb-1">
              {{ invoice()!.amountIncludingVat | currency:'DOP':'symbol':'1.2-2' }}
            </div>
            <div class="text-sm text-teal-300/70">
              Subtotal: {{ invoice()!.amount | currency:'DOP':'symbol':'1.2-2' }}
            </div>
          </div>
        </div>

        <div class="bg-neutral-900 border border-neutral-800 rounded-xl overflow-hidden flex-1 shadow-sm">
          <div class="p-4 border-b border-neutral-800 bg-neutral-900/50">
            <h2 class="font-medium text-white flex items-center gap-2">
              <i class="fi fi-rr-list text-teal-500"></i>
              Líneas de Factura
            </h2>
          </div>
          <div class="overflow-x-auto">
            <table class="w-full text-left text-sm whitespace-nowrap">
              <thead class="bg-neutral-950 text-neutral-400">
                <tr>
                  <th class="px-6 py-3 font-medium">Descripción</th>
                  <th class="px-6 py-3 font-medium text-right">Cantidad</th>
                  <th class="px-6 py-3 font-medium text-right">Precio Unitario</th>
                  <th class="px-6 py-3 font-medium text-right">Monto</th>
                  <th class="px-6 py-3 font-medium text-right">ITBIS</th>
                  <th class="px-6 py-3 font-medium text-right">Total</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-neutral-800/50">
                @for (line of invoice()!.lines; track $index) {
                  <tr class="hover:bg-neutral-800/30 transition-colors">
                    <td class="px-6 py-4 text-neutral-200">{{ line.description }}</td>
                    <td class="px-6 py-4 text-right text-neutral-300">{{ line.quantity }} <span class="text-neutral-500 text-xs ml-1">{{ line.unitOfMeasureCode }}</span></td>
                    <td class="px-6 py-4 text-right text-neutral-300">{{ line.unitCost | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td class="px-6 py-4 text-right text-neutral-300">{{ line.amount | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td class="px-6 py-4 text-right text-neutral-400 text-xs">{{ line.vat | currency:'DOP':'symbol':'1.2-2' }}</td>
                    <td class="px-6 py-4 text-right font-medium text-teal-400">{{ line.amountIncludingVat | currency:'DOP':'symbol':'1.2-2' }}</td>
                  </tr>
                }
                @if (!invoice()!.lines?.length) {
                  <tr>
                    <td colspan="6" class="px-6 py-8 text-center text-neutral-500 italic">No hay líneas en esta factura.</td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        </div>
      }

      @else {
        <div class="nx-empty" style="margin-top:var(--nx-space-8);">
          <p class="nx-empty__title">Factura no encontrada</p>
          <a routerLink="/purchase-invoices" class="nx-btn nx-btn--secondary nx-btn--sm" style="margin-top:var(--nx-space-4);">
            Volver a facturas
          </a>
        </div>
      }

    </div>
  `,
  styles: [`
    :host { display: block; }
    .new-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .card-body-pad { padding:var(--nx-space-5) var(--nx-space-6); }
    .nx-input--inline { padding:4px var(--nx-space-2);height:30px;font-size:var(--nx-text-sm);width:100%;min-width:0; }
    .nx-input--num { text-align:right; }
    .vendor-drop { position:absolute;top:100%;left:0;right:0;z-index:200;background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-md);box-shadow:var(--nx-shadow-lg);max-height:240px;overflow-y:auto;margin-top:2px; }
    .vendor-drop__item { display:flex;align-items:center;gap:var(--nx-space-3);width:100%;padding:var(--nx-space-2) var(--nx-space-3);background:none;border:none;cursor:pointer;text-align:left; }
    .vendor-drop__item:hover { background:var(--nx-surface-raised); }
    .vendor-drop__no { font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-text-muted);min-width:90px;flex-shrink:0; }
    .vendor-drop__name { font-size:var(--nx-text-sm);font-weight:var(--nx-weight-medium);flex:1; }
    .vendor-drop__city { font-size:var(--nx-text-xs);color:var(--nx-text-muted); }
  `]
})
export class PurchaseInvoiceCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly service = inject(PurchaseInvoiceService);
  private readonly vendorSvc = inject(VendorService);

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
