import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { VendorService } from '../../../data/vendor.service';
import { Vendor, CreateVendorFormData } from '../../../domain/vendor.model';

@Component({
  selector: 'app-vendor-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
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
          <a [routerLink]="['/purchases']" class="nx-btn nx-btn--primary nx-btn--sm">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/></svg>
            Nueva Orden
          </a>
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 320px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Información General</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">No. Proveedor</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ vendor()!.no }}</dd>
                <dt class="nx-kv__k">Nombre</dt>
                <dd class="nx-kv__v" style="font-weight:var(--nx-weight-medium);">{{ vendor()!.name }}</dd>
                <dt class="nx-kv__k">Dirección</dt>
                <dd class="nx-kv__v">{{ vendor()!.address || '—' }}</dd>
                <dt class="nx-kv__k">Ciudad</dt>
                <dd class="nx-kv__v">{{ vendor()!.city || '—' }}</dd>
                <dt class="nx-kv__k">Contacto</dt>
                <dd class="nx-kv__v">{{ vendor()!.contact || '—' }}</dd>
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
              @if (vendor()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { ✓ Desbloquear }
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
            <div class="form-grid">
              <div class="nx-field"><label class="nx-label">No. Proveedor</label><input class="nx-input" [value]="form.name" disabled style="opacity:.6;" /></div>
              <div class="nx-field"><label class="nx-label">Nombre *</label><input class="nx-input" [(ngModel)]="form.name" /></div>
              <div class="nx-field" style="grid-column:1/-1;"><label class="nx-label">Dirección</label><input class="nx-input" [(ngModel)]="form.address" /></div>
              <div class="nx-field"><label class="nx-label">Ciudad</label><input class="nx-input" [(ngModel)]="form.city" /></div>
              <div class="nx-field"><label class="nx-label">Contacto</label><input class="nx-input" [(ngModel)]="form.contact" /></div>
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
    .nx-kv--2col { display:grid;grid-template-columns:max-content 1fr; }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(720px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
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
  form: CreateVendorFormData = { name: '', address: '', city: '', contact: '' };

  async ngOnInit() {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const v = await this.svc.getByNo(no);
    this.vendor.set(v);
    this.loading.set(false);
  }

  openEdit() {
    const v = this.vendor()!;
    this.form = { name: v.name, address: v.address, city: v.city, contact: v.contact };
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
}
