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
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Proveedores</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-5);">
      <div>
        <h1 class="nx-page-title">Proveedores</h1>
        <p class="nx-page-subtitle">Gestiona tu catálogo de suplidores y abastecimiento</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary" (click)="exportExcel()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/></svg>
          Exportar Excel
        </button>
        <button class="nx-btn nx-btn--primary" (click)="openNew()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Proveedor
        </button>
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (svc.error()) {
      <div class="nx-callout nx-callout--danger">{{ svc.error() }}</div>
    } @else {
      <div class="nx-card">
        <div class="nx-card__head" style="gap:var(--nx-space-4);flex-wrap:wrap;">
          <div class="nx-input-icon" style="max-width:320px;flex:1;">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="11" cy="11" r="8"/><line x1="21" y1="21" x2="16.65" y2="16.65"/></svg>
            <input class="nx-input" placeholder="Buscar por número o nombre…" />
          </div>
          <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
            {{ svc.totalItems() }} proveedores en total
          </div>
        </div>

        @if (svc.items().length === 0) {
          <div class="nx-empty" style="padding:var(--nx-space-8);">
            <div class="nx-empty__icon">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><line x1="19" y1="8" x2="19" y2="14"/><line x1="22" y1="11" x2="16" y2="11"/></svg>
            </div>
            <p class="nx-empty__title">No hay proveedores registrados</p>
            <p class="nx-empty__text">Comienza creando tu primer suplidor en el sistema.</p>
            <button class="nx-btn nx-btn--primary nx-btn--sm" style="margin-top:var(--nx-space-4);" (click)="openNew()">Crear Proveedor</button>
          </div>
        } @else {
          <div style="overflow-x:auto;">
            <table class="nx-table">
              <thead>
                <tr>
                  <th class="sortable" style="width:120px;" (click)="setSort('no')">No. {{ si('no') }}</th>
                  <th class="sortable" (click)="setSort('name')">Nombre {{ si('name') }}</th>
                  <th class="sortable" (click)="setSort('contact')">Contacto {{ si('contact') }}</th>
                  <th class="sortable" (click)="setSort('city')">Ciudad {{ si('city') }}</th>
                  <th class="sortable" (click)="setSort('blocked')">Estado {{ si('blocked') }}</th>
                </tr>
              </thead>
              <tbody>
                @for (v of sorted(); track v.no) {
                  <tr style="cursor:pointer;" [routerLink]="['/vendors', v.no]">
                    <td class="nx-td--doc"><a class="nx-link">{{ v.no }}</a></td>
                    <td style="font-weight:var(--nx-weight-medium);">{{ v.name }}</td>
                    <td>{{ v.contact || '—' }}</td>
                    <td style="color:var(--nx-text-muted);">{{ v.city || '—' }}</td>
                    <td>
                      @if (v.blocked) {
                        <span class="nx-badge nx-badge--danger">Bloqueado</span>
                      } @else {
                        <span class="nx-badge nx-badge--success">Activo</span>
                      }
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        }
      </div>
    }

    <!-- Modal de Nuevo Proveedor -->
    @if (showNewModal()) {
      <div class="modal-backdrop" (click)="closeNew()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Nuevo Proveedor</h2>
            <button class="nx-iconbtn" (click)="closeNew()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (createError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ createError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Nombre del Proveedor / Empresa *</label>
                <input class="nx-input" [(ngModel)]="newForm.name" placeholder="Ej. Distribuidora XYZ S.R.L." autofocus />
              </div>
              <div class="nx-field">
                <label class="nx-label">Contacto principal</label>
                <input class="nx-input" [(ngModel)]="newForm.contact" placeholder="Nombre del representante" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Ciudad</label>
                <input class="nx-input" [(ngModel)]="newForm.city" placeholder="Ej. Santo Domingo" />
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Dirección</label>
                <input class="nx-input" [(ngModel)]="newForm.address" placeholder="Calle, número, sector" />
              </div>
            </div>
            <div class="nx-callout nx-callout--info" style="margin-top:var(--nx-space-4);">
              <strong>Secuencia Automática:</strong> El número de proveedor (VEND-XXXXXX) será asignado por el sistema al guardar.
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeNew()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving() || !newForm.name" (click)="saveNew()">
              @if (saving()) { Guardando… } @else { Guardar Proveedor }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .sortable { cursor:pointer;user-select:none;white-space:nowrap; }
    .sortable:hover { color:var(--nx-text-body); }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(600px,96vw);display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { padding:var(--nx-space-5) var(--nx-space-6); }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
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
