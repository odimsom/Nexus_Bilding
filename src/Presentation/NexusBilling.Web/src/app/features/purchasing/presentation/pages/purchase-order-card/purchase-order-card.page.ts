import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PurchaseOrderService } from '../../../data/purchase.service';
import { PurchaseOrder } from '../../../domain/purchase.model';
import { VendorService } from '../../../data/vendor.service';
import { Vendor } from '../../../domain/vendor.model';

interface NewLine {
  lineType: string;
  itemNo: string;
  description: string;
  unitOfMeasure: string;
  quantity: number;
  unitPrice: number;
  lineDiscountPct: number;
}

@Component({
  selector: 'app-purchase-order-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando pedido...</p>
      </div>
    } @else if (isNew()) {

      <!-- ── NEW ORDER FORM ── -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/purchases">Pedidos de Compra</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">Nuevo Pedido</span>
      </nav>

      <div class="nx-page-header">
        <div>
          <h1 class="nx-page-title">Nuevo Pedido de Compra</h1>
          <p class="nx-page-subtitle">{{ newLines().length }} línea(s)</p>
        </div>
        <div class="nx-page-actions">
          <a routerLink="/purchases" class="nx-btn nx-btn--ghost">Cancelar</a>
          <button class="nx-btn nx-btn--primary"
            [disabled]="saving() || !newHeader.buyFromVendorNo || newLines().length === 0"
            (click)="saveNew()">
            @if (saving()) { Guardando... } @else { Guardar Pedido }
          </button>
        </div>
      </div>

      @if (errorMsg()) {
        <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">{{ errorMsg() }}</div>
      }

      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- Header card -->
          <div class="nx-card" style="padding:var(--nx-space-5);">
            <div class="nx-card__head" style="margin-bottom:var(--nx-space-4);">
              <div class="nx-card__title">Encabezado</div>
            </div>
            <div class="form-grid">
              <div class="nx-field" style="position:relative;">
                <label class="nx-label">Proveedor *</label>
                <input
                  class="nx-input"
                  [(ngModel)]="newHeader.vendorSearch"
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
                <input class="nx-input" [(ngModel)]="newHeader.payToName" placeholder="Se rellena automáticamente" />
              </div>

              <div class="nx-field">
                <label class="nx-label">Fecha de Contabilización</label>
                <input class="nx-input" type="date" [(ngModel)]="newHeader.postingDate" />
              </div>

              <div class="nx-field">
                <label class="nx-label">No. Documento Externo</label>
                <input class="nx-input" [(ngModel)]="newHeader.externalDocumentNo" placeholder="Factura/Recibo del proveedor" />
              </div>

              <div class="nx-field">
                <label class="nx-label">Condición de Pago</label>
                <select class="nx-input" [(ngModel)]="newHeader.paymentTermsCode">
                  <option value="">- Seleccionar -</option>
                  <option value="CM">Contado</option>
                  <option value="15D">15 Días</option>
                  <option value="30D">30 Días</option>
                  <option value="45D">45 Días</option>
                  <option value="60D">60 Días</option>
                </select>
              </div>

              <div class="nx-field">
                <label class="nx-label">Moneda</label>
                <select class="nx-input" [(ngModel)]="newHeader.currencyCode">
                  <option value="">DOP (defecto)</option>
                  <option value="USD">USD</option>
                  <option value="EUR">EUR</option>
                </select>
              </div>
            </div>
          </div>

          <!-- Lines card -->
          <div class="nx-card" style="padding:var(--nx-space-5);">
            <div class="nx-card__head" style="margin-bottom:var(--nx-space-4);">
              <div class="nx-card__title">Líneas del Pedido</div>
              <button type="button" class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine()">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                Añadir línea
              </button>
            </div>

            @if (newLines().length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-5);">
                <p class="nx-empty__text">Agrega al menos una línea para guardar el pedido.</p>
              </div>
            } @else {
              <div style="overflow-x:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th style="width:80px;">Tipo</th>
                      <th style="width:120px;">No. Artículo</th>
                      <th>Descripción</th>
                      <th style="width:80px;">Cant.</th>
                      <th style="width:70px;">UM</th>
                      <th style="width:110px;">Precio Unit.</th>
                      <th style="width:80px;">Desc.%</th>
                      <th style="width:110px;text-align:right;">Total</th>
                      <th style="width:40px;"></th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (line of newLines(); track $index; let i = $index) {
                      <tr>
                        <td>
                          <select class="nx-input nx-input--sm" [(ngModel)]="line.lineType">
                            <option value="Item">Artículo</option>
                            <option value="G/L Account">C/G</option>
                            <option value="Service">Servicio</option>
                          </select>
                        </td>
                        <td><input class="nx-input nx-input--sm nx-td--mono" [(ngModel)]="line.itemNo" placeholder="No." /></td>
                        <td><input class="nx-input nx-input--sm" [(ngModel)]="line.description" placeholder="Descripción" /></td>
                        <td><input class="nx-input nx-input--sm" type="number" min="0" step="0.001" [(ngModel)]="line.quantity" /></td>
                        <td><input class="nx-input nx-input--sm" [(ngModel)]="line.unitOfMeasure" placeholder="UND" /></td>
                        <td><input class="nx-input nx-input--sm" type="number" min="0" step="0.01" [(ngModel)]="line.unitPrice" /></td>
                        <td><input class="nx-input nx-input--sm" type="number" min="0" max="100" step="0.1" [(ngModel)]="line.lineDiscountPct" /></td>
                        <td style="text-align:right;font-weight:var(--nx-weight-medium);white-space:nowrap;">
                          {{ lineTotal(line) | number:'1.2-2' }}
                        </td>
                        <td>
                          <button type="button" class="nx-iconbtn nx-iconbtn--sm nx-iconbtn--danger" (click)="removeLine(i)" title="Eliminar">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6M14 11v6"/></svg>
                          </button>
                        </td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
            }
          </div>
        </div>

        <!-- Totals factbox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Resumen</div>
            <div class="nx-factbox__title">Totales</div>
          </div>
          <div class="nx-factbox__section">
            <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-2);">
              <span style="color:var(--nx-text-muted);">Subtotal</span>
              <span class="nx-td--mono">{{ subtotal() | number:'1.2-2' }}</span>
            </div>
            <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-3);">
              <span style="color:var(--nx-text-muted);">ITBIS (18%)</span>
              <span class="nx-td--mono">{{ totalVat() | number:'1.2-2' }}</span>
            </div>
            <div style="display:flex;justify-content:space-between;padding-top:var(--nx-space-3);border-top:1px solid var(--nx-border);font-weight:var(--nx-weight-bold);font-size:var(--nx-text-lg);">
              <span>Total</span>
              <span>{{ totalWithVat() | number:'1.2-2' }}</span>
            </div>
          </div>
        </div>
      </div>

    } @else if (order()) {

      <!-- ── EXISTING ORDER DETAIL ── -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/purchases">Pedidos de Compra</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ order()!.no }}</span>
      </nav>

      <div class="nx-page-header">
        <div style="flex:1;min-width:0;">
          <h1 class="nx-page-title">Pedido: {{ order()!.no }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);flex-wrap:wrap;">
            <span class="nx-eyebrow">{{ order()!.vendorName || order()!.vendorNo }}</span>
            @if (order()!.status === 'Open') {
              <span class="nx-badge nx-badge--warn"><span class="nx-badge__dot"></span>Abierto</span>
            } @else if (order()!.status === 'Released') {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Lanzado</span>
            } @else {
              <span class="nx-badge nx-badge--info"><span class="nx-badge__dot"></span>{{ order()!.status }}</span>
            }
            <span style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">
              Fecha: {{ order()!.postingDate | date:'shortDate' }}
            </span>
          </div>
        </div>
        <div class="nx-page-actions">
          @if (order()!.status === 'Open' && !editingLines()) {
            <button class="nx-btn nx-btn--secondary" (click)="startEditLines()">Editar Líneas</button>
          }
          @if (order()!.status === 'Open') {
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="postOrder()">
              @if (saving()) { Procesando... } @else { Contabilizar }
            </button>
          }
        </div>
      </div>

      @if (errorMsg()) {
        <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-4);">{{ errorMsg() }}</div>
      }

      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- Lines card -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Líneas del Pedido</div>
              @if (editingLines()) {
                <div style="display:flex;gap:var(--nx-space-2);">
                  <button type="button" class="nx-btn nx-btn--secondary nx-btn--sm" (click)="addLine()">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
                    Añadir
                  </button>
                  <button type="button" class="nx-btn nx-btn--ghost nx-btn--sm" (click)="cancelEditLines()">Cancelar</button>
                  <button type="button" class="nx-btn nx-btn--primary nx-btn--sm" [disabled]="savingLines()" (click)="saveLines()">
                    @if (savingLines()) { Guardando... } @else { Guardar Líneas }
                  </button>
                </div>
              }
            </div>

            @if (editingLines()) {
              <!-- Edit mode -->
              @if (newLines().length === 0) {
                <div class="nx-empty" style="padding:var(--nx-space-5);">
                  <p class="nx-empty__text">No hay líneas. Agrega al menos una.</p>
                </div>
              } @else {
                <div style="overflow-x:auto;">
                  <table class="nx-table">
                    <thead>
                      <tr>
                        <th style="width:80px;">Tipo</th>
                        <th style="width:120px;">No. Artículo</th>
                        <th>Descripción</th>
                        <th style="width:80px;">Cant.</th>
                        <th style="width:70px;">UM</th>
                        <th style="width:110px;">Precio Unit.</th>
                        <th style="width:80px;">Desc.%</th>
                        <th style="width:110px;text-align:right;">Total</th>
                        <th style="width:40px;"></th>
                      </tr>
                    </thead>
                    <tbody>
                      @for (line of newLines(); track $index; let i = $index) {
                        <tr>
                          <td>
                            <select class="nx-input nx-input--sm" [(ngModel)]="line.lineType">
                              <option value="Item">Artículo</option>
                              <option value="G/L Account">C/G</option>
                              <option value="Service">Servicio</option>
                            </select>
                          </td>
                          <td><input class="nx-input nx-input--sm nx-td--mono" [(ngModel)]="line.itemNo" placeholder="No." /></td>
                          <td><input class="nx-input nx-input--sm" [(ngModel)]="line.description" placeholder="Descripción" /></td>
                          <td><input class="nx-input nx-input--sm" type="number" min="0" step="0.001" [(ngModel)]="line.quantity" /></td>
                          <td><input class="nx-input nx-input--sm" [(ngModel)]="line.unitOfMeasure" placeholder="UND" /></td>
                          <td><input class="nx-input nx-input--sm" type="number" min="0" step="0.01" [(ngModel)]="line.unitPrice" /></td>
                          <td><input class="nx-input nx-input--sm" type="number" min="0" max="100" step="0.1" [(ngModel)]="line.lineDiscountPct" /></td>
                          <td style="text-align:right;font-weight:var(--nx-weight-medium);white-space:nowrap;">
                            {{ lineTotal(line) | number:'1.2-2' }}
                          </td>
                          <td>
                            <button type="button" class="nx-iconbtn nx-iconbtn--sm nx-iconbtn--danger" (click)="removeLine(i)">
                              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6M14 11v6"/></svg>
                            </button>
                          </td>
                        </tr>
                      }
                    </tbody>
                  </table>
                </div>
              }
            } @else {
              <!-- Read-only mode -->
              @if (order()!.lines && order()!.lines.length > 0) {
                <div style="overflow-x:auto;">
                  <table class="nx-table">
                    <thead>
                      <tr>
                        <th>Tipo</th>
                        <th>No.</th>
                        <th>Descripción</th>
                        <th>Cant.</th>
                        <th>UM</th>
                        <th style="text-align:right;">Precio Unit.</th>
                        <th style="text-align:right;">Total c/IVA</th>
                      </tr>
                    </thead>
                    <tbody>
                      @for (line of order()!.lines; track line.lineNo) {
                        <tr>
                          <td style="color:var(--nx-text-muted);font-size:var(--nx-text-xs);">Artículo</td>
                          <td class="nx-td--mono">{{ line.itemNo }}</td>
                          <td>{{ line.description }}</td>
                          <td>{{ line.quantity }}</td>
                          <td>{{ line.unitOfMeasure }}</td>
                          <td style="text-align:right;">{{ line.unitPrice | number:'1.2-2' }}</td>
                          <td style="text-align:right;font-weight:var(--nx-weight-medium);">{{ line.amountIncludingVat | number:'1.2-2' }}</td>
                        </tr>
                      }
                    </tbody>
                  </table>
                </div>
              } @else {
                <div class="nx-empty" style="padding:var(--nx-space-5);">
                  <p class="nx-empty__text">No hay líneas en este pedido.</p>
                  @if (order()!.status === 'Open') {
                    <button type="button" class="nx-btn nx-btn--secondary nx-btn--sm" style="margin-top:var(--nx-space-3);" (click)="startEditLines()">Agregar líneas</button>
                  }
                </div>
              }
            }
          </div>
        </div>

        <!-- Totals factbox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Resumen</div>
            <div class="nx-factbox__title">Totales</div>
          </div>
          <div class="nx-factbox__section">
            @if (editingLines()) {
              <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-2);">
                <span style="color:var(--nx-text-muted);">Subtotal</span>
                <span class="nx-td--mono">{{ subtotal() | number:'1.2-2' }}</span>
              </div>
              <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-3);">
                <span style="color:var(--nx-text-muted);">ITBIS (18%)</span>
                <span class="nx-td--mono">{{ totalVat() | number:'1.2-2' }}</span>
              </div>
              <div style="display:flex;justify-content:space-between;padding-top:var(--nx-space-3);border-top:1px solid var(--nx-border);font-weight:var(--nx-weight-bold);font-size:var(--nx-text-lg);">
                <span>Total</span>
                <span>{{ totalWithVat() | number:'1.2-2' }}</span>
              </div>
            } @else {
              <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-2);">
                <span style="color:var(--nx-text-muted);">Subtotal</span>
                <span class="nx-td--mono">{{ order()!.amount | number:'1.2-2' }}</span>
              </div>
              <div style="display:flex;justify-content:space-between;margin-bottom:var(--nx-space-3);">
                <span style="color:var(--nx-text-muted);">ITBIS (18%)</span>
                <span class="nx-td--mono">{{ (order()!.amountIncludingVat - order()!.amount) | number:'1.2-2' }}</span>
              </div>
              <div style="display:flex;justify-content:space-between;padding-top:var(--nx-space-3);border-top:1px solid var(--nx-border);font-weight:var(--nx-weight-bold);font-size:var(--nx-text-lg);">
                <span>Total</span>
                <span>{{ order()!.amountIncludingVat | number:'1.2-2' }}</span>
              </div>
            }

            @if (order()!.externalDocumentNo) {
              <div style="margin-top:var(--nx-space-4);padding-top:var(--nx-space-3);border-top:1px solid var(--nx-border);">
                <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Ref. Proveedor</div>
                <div class="nx-td--mono" style="font-size:var(--nx-text-sm);">{{ order()!.externalDocumentNo }}</div>
              </div>
            }
          </div>
        </div>
      </div>

    } @else {
      <div class="nx-empty">
        <p class="nx-empty__title">Pedido no encontrado</p>
        <a routerLink="/purchases" class="nx-btn nx-btn--secondary nx-btn--sm">Volver</a>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
    .nx-input--sm { padding:var(--nx-space-1) var(--nx-space-2);font-size:var(--nx-text-sm);height:32px; }
    .nx-iconbtn--danger:hover { color:var(--nx-danger);border-color:var(--nx-danger); }
    .vendor-drop { position:absolute;top:100%;left:0;right:0;z-index:200;background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-md);box-shadow:var(--nx-shadow-lg);max-height:240px;overflow-y:auto;margin-top:2px; }
    .vendor-drop__item { display:flex;align-items:center;gap:var(--nx-space-3);width:100%;padding:var(--nx-space-2) var(--nx-space-3);background:none;border:none;cursor:pointer;text-align:left; }
    .vendor-drop__item:hover { background:var(--nx-surface-raised); }
    .vendor-drop__no { font-family:var(--nx-font-mono);font-size:var(--nx-text-xs);color:var(--nx-text-muted);min-width:90px;flex-shrink:0; }
    .vendor-drop__name { font-size:var(--nx-text-sm);font-weight:var(--nx-weight-medium);flex:1; }
    .vendor-drop__city { font-size:var(--nx-text-xs);color:var(--nx-text-muted); }
  `]
})
export class PurchaseOrderCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly svc = inject(PurchaseOrderService);
  private readonly vendorSvc = inject(VendorService);

  isNew = signal(false);
  loading = signal(true);
  saving = signal(false);
  savingLines = signal(false);
  errorMsg = signal<string | null>(null);
  editingLines = signal(false);

  order = signal<PurchaseOrder | null>(null);

  showVendorDrop = signal(false);
  vendorSugg = signal<Vendor[]>([]);

  newLines = signal<NewLine[]>([]);

  newHeader = {
    buyFromVendorNo: '',
    vendorSearch: '',
    payToName: '',
    postingDate: new Date().toISOString().split('T')[0],
    externalDocumentNo: '',
    paymentTermsCode: '',
    currencyCode: '',
  };

  subtotal = computed(() =>
    this.newLines().reduce((s, l) => s + this.lineTotal(l), 0)
  );
  totalVat = computed(() => this.subtotal() * 0.18);
  totalWithVat = computed(() => this.subtotal() * 1.18);

  async ngOnInit() {
    const no = this.route.snapshot.paramMap.get('no');
    if (no === 'new') {
      this.isNew.set(true);
      this.loading.set(false);
      if (this.vendorSvc.items().length === 0) {
        this.vendorSvc.load({ pageSize: 200 });
      }
    } else if (no) {
      this.loading.set(true);
      const res = await this.svc.getByNo(no);
      this.order.set(res);
      this.loading.set(false);
    }
  }

  private filterVendors(): void {
    const q = this.newHeader.vendorSearch.toLowerCase().trim();
    const all = this.vendorSvc.items();
    this.vendorSugg.set(
      !q ? all.slice(0, 10) : all.filter(v =>
        v.no.toLowerCase().includes(q) || v.name.toLowerCase().includes(q)
      ).slice(0, 10)
    );
  }

  onVendorFocus(): void { this.filterVendors(); this.showVendorDrop.set(true); }
  onVendorInput(): void { this.filterVendors(); this.showVendorDrop.set(true); }
  hideVendorDrop(): void { setTimeout(() => this.showVendorDrop.set(false), 150); }

  selectVendor(v: Vendor): void {
    this.newHeader.buyFromVendorNo = v.no;
    this.newHeader.vendorSearch = v.name;
    this.newHeader.payToName = v.name;
    if (v.paymentTermsCode) this.newHeader.paymentTermsCode = v.paymentTermsCode;
    this.showVendorDrop.set(false);
  }

  addLine(): void {
    this.newLines.update(lines => [...lines, {
      lineType: 'Item', itemNo: '', description: '',
      unitOfMeasure: 'UND', quantity: 1, unitPrice: 0, lineDiscountPct: 0
    }]);
  }

  removeLine(i: number): void {
    this.newLines.update(lines => lines.filter((_, idx) => idx !== i));
  }

  lineTotal(line: NewLine): number {
    const base = line.quantity * line.unitPrice;
    const disc = base * (line.lineDiscountPct / 100);
    return base - disc;
  }

  async saveNew() {
    if (!this.newHeader.buyFromVendorNo) {
      this.errorMsg.set('Selecciona un proveedor.');
      return;
    }
    if (this.newLines().length === 0) {
      this.errorMsg.set('Agrega al menos una línea al pedido.');
      return;
    }
    this.saving.set(true);
    this.errorMsg.set(null);
    try {
      const no = await this.svc.create({
        buyFromVendorNo: this.newHeader.buyFromVendorNo,
        payToName: this.newHeader.payToName,
        postingDate: this.newHeader.postingDate,
        currencyCode: this.newHeader.currencyCode,
        paymentTermsCode: this.newHeader.paymentTermsCode,
        externalDocumentNo: this.newHeader.externalDocumentNo,
        lines: this.newLines().map(l => ({
          lineType: l.lineType,
          itemNo: l.itemNo,
          description: l.description,
          unitOfMeasure: l.unitOfMeasure,
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          lineDiscountPct: l.lineDiscountPct
        }))
      });
      this.router.navigate(['/purchases', no], { replaceUrl: true });
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Ocurrió un error al crear el pedido.');
    } finally {
      this.saving.set(false);
    }
  }

  startEditLines(): void {
    const lines = (this.order()?.lines ?? []).map(l => ({
      lineType: 'Item',
      itemNo: l.itemNo,
      description: l.description,
      unitOfMeasure: l.unitOfMeasure,
      quantity: l.quantity,
      unitPrice: l.unitPrice,
      lineDiscountPct: l.lineDiscountPct
    }));
    this.newLines.set(lines);
    this.editingLines.set(true);
  }

  cancelEditLines(): void {
    this.editingLines.set(false);
    this.newLines.set([]);
    this.errorMsg.set(null);
  }

  async saveLines(): Promise<void> {
    const no = this.order()?.no;
    if (!no) return;
    this.savingLines.set(true);
    this.errorMsg.set(null);
    try {
      const result = await this.svc.updateLines(no, this.newLines().map(l => ({
        itemNo: l.itemNo, description: l.description,
        quantity: l.quantity, unitPrice: l.unitPrice,
        lineDiscountPct: l.lineDiscountPct, unitOfMeasure: l.unitOfMeasure,
        lineType: l.lineType
      })));
      const updated = await this.svc.getByNo(no);
      this.order.set(updated);
      this.editingLines.set(false);
      this.newLines.set([]);
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Error al guardar las líneas.');
    } finally {
      this.savingLines.set(false);
    }
  }

  async postOrder() {
    if (!this.order()) return;
    this.saving.set(true);
    this.errorMsg.set(null);
    try {
      const result = await this.svc.post(this.order()!.no);
      this.router.navigate(['/purchase-invoices', result.invoiceNo]);
    } catch (e: any) {
      this.errorMsg.set(e?.error?.error?.message ?? 'Ocurrió un error al contabilizar el pedido.');
    } finally {
      this.saving.set(false);
    }
  }
}
