import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VendorService } from '../../../data/vendor.service';
import { Vendor, CreateVendorFormData } from '../../../domain/vendor.model';

@Component({
  selector: 'app-vendor-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, CurrencyPipe],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando proveedor…</p>
      </div>
    } @else if (vendor()) {
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/vendors">Proveedores</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ vendor()!.name }}</span>
      </nav>

      <div class="nx-page-header">
        <div class="nx-avatar nx-avatar--lg" style="background:var(--nx-blue-100);color:var(--nx-blue-800);flex-shrink:0;">
          {{ vendor()!.name.charAt(0) }}
        </div>
        <div style="flex:1;min-width:0;">
          <h1 class="nx-page-title">{{ vendor()!.name }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);flex-wrap:wrap;">
            <span class="nx-eyebrow">{{ vendor()!.no }}</span>
            @if (vendor()!.rnc) {
              <span class="nx-eyebrow" style="color:var(--nx-text-muted);">RNC: {{ vendor()!.rnc }}</span>
            }
            @if (vendor()!.vendorType) {
              <span class="nx-badge nx-badge--info">{{ vendorTypeLabel(vendor()!.vendorType) }}</span>
            }
            @if (vendor()!.blocked) {
              <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
            } @else {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="openEdit()">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
            Editar
          </button>
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- Identificación Fiscal -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Identificación Fiscal</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">No. Proveedor</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ vendor()!.no }}</dd>
                <dt class="nx-kv__k">Nombre Comercial</dt>
                <dd class="nx-kv__v" style="font-weight:var(--nx-weight-medium);">{{ vendor()!.name }}</dd>
                <dt class="nx-kv__k">RNC / Cédula</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ vendor()!.rnc || '—' }}</dd>
                <dt class="nx-kv__k">Tipo de Proveedor</dt>
                <dd class="nx-kv__v">{{ vendorTypeLabel(vendor()!.vendorType) || '—' }}</dd>
              </dl>
            </div>
          </div>

          <!-- Dirección -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Dirección</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Dirección</dt>
                <dd class="nx-kv__v">{{ vendor()!.address || '—' }}</dd>
                @if (vendor()!.address2) {
                  <dt class="nx-kv__k">Dirección 2</dt>
                  <dd class="nx-kv__v">{{ vendor()!.address2 }}</dd>
                }
                <dt class="nx-kv__k">Ciudad</dt>
                <dd class="nx-kv__v">{{ vendor()!.city || '—' }}</dd>
                <dt class="nx-kv__k">Provincia</dt>
                <dd class="nx-kv__v">{{ vendor()!.province || '—' }}</dd>
                <dt class="nx-kv__k">País</dt>
                <dd class="nx-kv__v">{{ vendor()!.country || 'RD' }}</dd>
              </dl>
            </div>
          </div>

          <!-- Contacto -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Contacto</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Persona de Contacto</dt>
                <dd class="nx-kv__v">{{ vendor()!.contact || '—' }}</dd>
                <dt class="nx-kv__k">Teléfono</dt>
                <dd class="nx-kv__v">
                  @if (vendor()!.phoneNo) {
                    <a [href]="'tel:' + vendor()!.phoneNo" style="color:var(--nx-action);">{{ vendor()!.phoneNo }}</a>
                  } @else { — }
                </dd>
                @if (vendor()!.phoneNo2) {
                  <dt class="nx-kv__k">Teléfono 2</dt>
                  <dd class="nx-kv__v"><a [href]="'tel:' + vendor()!.phoneNo2" style="color:var(--nx-action);">{{ vendor()!.phoneNo2 }}</a></dd>
                }
                <dt class="nx-kv__k">Correo Electrónico</dt>
                <dd class="nx-kv__v">
                  @if (vendor()!.email) {
                    <a [href]="'mailto:' + vendor()!.email" style="color:var(--nx-action);">{{ vendor()!.email }}</a>
                  } @else { — }
                </dd>
                @if (vendor()!.webSite) {
                  <dt class="nx-kv__k">Sitio Web</dt>
                  <dd class="nx-kv__v"><a [href]="vendor()!.webSite" target="_blank" rel="noopener" style="color:var(--nx-action);">{{ vendor()!.webSite }}</a></dd>
                }
              </dl>
            </div>
          </div>

          <!-- Condiciones Comerciales -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Condiciones Comerciales</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Condición de Pago</dt>
                <dd class="nx-kv__v">{{ paymentTermsLabel(vendor()!.paymentTermsCode) || '—' }}</dd>
                <dt class="nx-kv__k">Método de Pago</dt>
                <dd class="nx-kv__v">{{ paymentMethodLabel(vendor()!.paymentMethodCode) || '—' }}</dd>
                <dt class="nx-kv__k">Moneda</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ vendor()!.currencyCode || 'DOP' }}</dd>
                <dt class="nx-kv__k">Límite de Crédito</dt>
                <dd class="nx-kv__v nx-kv__v--mono">
                  {{ (vendor()!.creditLimit ?? 0) | currency:(vendor()!.currencyCode || 'DOP'):'symbol':'1.2-2' }}
                </dd>
              </dl>
            </div>
          </div>

        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Acciones Rápidas</div>
            <div class="nx-factbox__title">Gestión de Proveedor</div>
          </div>
          <div class="nx-factbox__section">
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block" (click)="openEdit()">Editar Proveedor</button>
              <a routerLink="/purchase-invoices/new" [queryParams]="{vendorNo: vendor()!.no, vendorName: vendor()!.name}" class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block">
                Nueva Factura de Compra
              </a>
              @if (vendor()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { Desbloquear }
                </button>
              } @else {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" style="color:var(--nx-red-500);" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { Bloquear Proveedor }
                </button>
              }
            </div>
          </div>
        </div>
      </div>

    } @else {
      <div class="nx-empty">
        <p class="nx-empty__title">Proveedor no encontrado</p>
        <a routerLink="/vendors" class="nx-btn nx-btn--secondary nx-btn--sm">Volver a Proveedores</a>
      </div>
    }

    <!-- Edit Modal -->
    @if (showEdit()) {
      <div class="modal-backdrop" (click)="closeEdit()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Editar: {{ vendor()!.name }}</h2>
            <button class="nx-iconbtn" (click)="closeEdit()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (editError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ editError() }}</div>
            }

            <div class="form-section">
              <div class="form-section__title">Identificación Fiscal</div>
              <div class="form-grid">
                <div class="nx-field"><label class="nx-label">No. Proveedor</label><input class="nx-input" [value]="vendor()!.no" disabled style="opacity:.6;" /></div>
                <div class="nx-field"><label class="nx-label">Nombre / Razón Social *</label><input class="nx-input" [(ngModel)]="form.name" autofocus /></div>
                <div class="nx-field"><label class="nx-label">RNC / Cédula</label><input class="nx-input" [(ngModel)]="form.rnc" placeholder="1-23-45678-9" /></div>
                <div class="nx-field"><label class="nx-label">Tipo de Proveedor</label>
                  <select class="nx-input" [(ngModel)]="form.vendorType">
                    <option value="">— Sin especificar —</option>
                    <option value="local">Local</option>
                    <option value="extranjero">Extranjero</option>
                    <option value="servicio">Servicios</option>
                    <option value="contratista">Contratista</option>
                  </select>
                </div>
              </div>
            </div>

            <div class="form-section">
              <div class="form-section__title">Dirección</div>
              <div class="form-grid">
                <div class="nx-field" style="grid-column:1/-1;"><label class="nx-label">Dirección</label><input class="nx-input" [(ngModel)]="form.address" placeholder="Calle, No., Sector" /></div>
                <div class="nx-field" style="grid-column:1/-1;"><label class="nx-label">Dirección 2</label><input class="nx-input" [(ngModel)]="form.address2" placeholder="Edificio, apto, local" /></div>
                <div class="nx-field"><label class="nx-label">Ciudad</label><input class="nx-input" [(ngModel)]="form.city" placeholder="Ej. Santo Domingo" /></div>
                <div class="nx-field"><label class="nx-label">Provincia</label>
                  <select class="nx-input" [(ngModel)]="form.province">
                    <option value="">— Seleccionar —</option>
                    @for (p of provinces; track p) {
                      <option [value]="p">{{ p }}</option>
                    }
                  </select>
                </div>
                <div class="nx-field"><label class="nx-label">País</label><input class="nx-input" [(ngModel)]="form.country" placeholder="RD" /></div>
              </div>
            </div>

            <div class="form-section">
              <div class="form-section__title">Contacto</div>
              <div class="form-grid">
                <div class="nx-field"><label class="nx-label">Persona de Contacto</label><input class="nx-input" [(ngModel)]="form.contact" /></div>
                <div class="nx-field"><label class="nx-label">Correo Electrónico</label><input class="nx-input" type="email" [(ngModel)]="form.email" placeholder="proveedor@empresa.com" /></div>
                <div class="nx-field"><label class="nx-label">Teléfono</label><input class="nx-input" [(ngModel)]="form.phoneNo" placeholder="809-000-0000" /></div>
                <div class="nx-field"><label class="nx-label">Teléfono 2</label><input class="nx-input" [(ngModel)]="form.phoneNo2" placeholder="829-000-0000" /></div>
                <div class="nx-field" style="grid-column:1/-1;"><label class="nx-label">Sitio Web</label><input class="nx-input" [(ngModel)]="form.webSite" placeholder="https://www.empresa.com" /></div>
              </div>
            </div>

            <div class="form-section">
              <div class="form-section__title">Condiciones Comerciales</div>
              <div class="form-grid">
                <div class="nx-field"><label class="nx-label">Condición de Pago</label>
                  <select class="nx-input" [(ngModel)]="form.paymentTermsCode">
                    <option value="">— Sin especificar —</option>
                    <option value="CM">Contado</option>
                    <option value="15D">15 dias</option>
                    <option value="30D">30 dias</option>
                    <option value="45D">45 dias</option>
                    <option value="60D">60 dias</option>
                    <option value="90D">90 dias</option>
                  </select>
                </div>
                <div class="nx-field"><label class="nx-label">Método de Pago</label>
                  <select class="nx-input" [(ngModel)]="form.paymentMethodCode">
                    <option value="">— Sin especificar —</option>
                    <option value="EFE">Efectivo</option>
                    <option value="CHQ">Cheque</option>
                    <option value="TRF">Transferencia Bancaria</option>
                    <option value="TAR">Tarjeta</option>
                  </select>
                </div>
                <div class="nx-field"><label class="nx-label">Moneda</label>
                  <select class="nx-input" [(ngModel)]="form.currencyCode">
                    <option value="">DOP (Pesos)</option>
                    <option value="USD">USD (Dólares)</option>
                    <option value="EUR">EUR (Euros)</option>
                  </select>
                </div>
                <div class="nx-field"><label class="nx-label">Límite de Crédito</label>
                  <input class="nx-input" type="number" [(ngModel)]="form.creditLimit" min="0" step="1000" placeholder="0.00" />
                </div>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeEdit()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveEdit()">
              @if (saving()) { Guardando… } @else { Guardar Cambios }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:0 auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .nx-kv--2col { display:grid;grid-template-columns:160px 1fr; }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(760px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-section { margin-bottom:var(--nx-space-5); }
    .form-section__title { font-size:var(--nx-text-xs);font-weight:var(--nx-weight-semibold);text-transform:uppercase;letter-spacing:.05em;color:var(--nx-text-muted);margin-bottom:var(--nx-space-3);padding-bottom:var(--nx-space-2);border-bottom:1px solid var(--nx-border); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
})
export class VendorCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(VendorService);

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
    const map: Record<string, string> = {
      CM: 'Contado', '15D': '15 días', '30D': '30 días', '45D': '45 días', '60D': '60 días', '90D': '90 días'
    };
    return map[code] ?? code;
  }

  paymentMethodLabel(code: string): string {
    const map: Record<string, string> = {
      EFE: 'Efectivo', CHQ: 'Cheque', TRF: 'Transferencia Bancaria', TAR: 'Tarjeta'
    };
    return map[code] ?? code;
  }
}
