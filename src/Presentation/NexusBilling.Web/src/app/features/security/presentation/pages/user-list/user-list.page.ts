import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserService, AppUser } from '../../../data/user.service';
import { sortBy } from '../../../../../shared/utils/sort.utils';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Usuarios</span>
    </nav>

    <div class="nx-page-header">
      <div>
        <h1 class="nx-page-title">Gestión de Usuarios</h1>
        <p class="nx-page-subtitle">Administra los accesos y roles del sistema</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openModal()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Usuario
        </button>
      </div>
    </div>

    @if (loading()) {
      <div class="nx-empty">
        <div class="nx-spinner"></div>
      </div>
    } @else {
      <div class="nx-card">
        <table class="nx-table">
          <thead>
            <tr>
              <th class="sortable" (click)="setSort('username')">Usuario {{ si('username') }}</th>
              <th class="sortable" (click)="setSort('fullName')">Nombre Completo {{ si('fullName') }}</th>
              <th class="sortable" (click)="setSort('email')">Email {{ si('email') }}</th>
              <th class="sortable" (click)="setSort('isActive')">Estado {{ si('isActive') }}</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            @for (u of sorted(); track u.id) {
              <tr>
                <td style="font-weight:var(--nx-weight-medium);">{{ u.username }}</td>
                <td>{{ u.fullName || '—' }}</td>
                <td style="color:var(--nx-text-muted);">{{ u.email }}</td>
                <td>
                  @if (u.isActive) {
                    <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
                  } @else {
                    <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Inactivo</span>
                  }
                </td>
                <td style="text-align:right;">
                  <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="toggleActive(u)">
                    {{ u.isActive ? 'Desactivar' : 'Activar' }}
                  </button>
                </td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    }

    <!-- Modal Nuevo Usuario -->
    @if (showModal()) {
      <div class="modal-backdrop" (click)="closeModal()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Nuevo Usuario</h2>
            <button class="nx-iconbtn" (click)="closeModal()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (modalError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ modalError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field"><label class="nx-label">Usuario *</label><input class="nx-input" [(ngModel)]="form.username" /></div>
              <div class="nx-field"><label class="nx-label">Email *</label><input class="nx-input" type="email" [(ngModel)]="form.email" /></div>
              <div class="nx-field"><label class="nx-label">Nombre Completo *</label><input class="nx-input" [(ngModel)]="form.fullName" /></div>
              <div class="nx-field"><label class="nx-label">Contraseña *</label><input class="nx-input" type="password" [(ngModel)]="form.password" /></div>
              <div class="nx-field"><label class="nx-label">No. Empleado</label><input class="nx-input" [(ngModel)]="form.employeeNo" /></div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeModal()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveUser()">
              @if (saving()) { Creando... } @else { Crear Usuario }
            </button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .sortable { cursor:pointer; user-select:none; white-space:nowrap; }
    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(500px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }
  `]
})
export class UserListPage implements OnInit {
  readonly svc = inject(UserService);

  get users() { return this.svc.users; }
  get loading() { return this.svc.loading; }

  sortField = 'username';
  sortAsc = true;

  sorted(): AppUser[] {
    const items = this.svc.users();
    if (this.sortField === 'isActive') {
      return [...items].sort((a, b) => {
        const av = a.isActive ? 1 : 0;
        const bv = b.isActive ? 1 : 0;
        return this.sortAsc ? av - bv : bv - av;
      });
    }
    return sortBy(items, this.sortField, this.sortAsc);
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

  showModal = signal(false);
  saving = signal(false);
  modalError = signal<string | null>(null);
  
  form = { username: '', email: '', fullName: '', password: '', employeeNo: '' };

  async ngOnInit() {
    await this.loadUsers();
  }

  async loadUsers() {
    await this.svc.loadUsers();
  }

  openModal() {
    this.form = { username: '', email: '', fullName: '', password: '', employeeNo: '' };
    this.modalError.set(null);
    this.showModal.set(true);
  }

  closeModal() {
    this.showModal.set(false);
  }

  async saveUser() {
    if (!this.form.username || !this.form.email || !this.form.password) {
      this.modalError.set('Llene todos los campos requeridos (*).');
      return;
    }
    this.saving.set(true);
    try {
      await this.svc.create({ ...this.form, groupCode: '' });
      this.closeModal();
      await this.loadUsers();
    } catch (e: any) {
      this.modalError.set(e?.error?.error?.message ?? 'Error al crear usuario.');
    } finally {
      this.saving.set(false);
    }
  }

  async toggleActive(u: AppUser) {
    try {
      if (u.isActive) {
        await this.svc.deactivate(u.id);
      } else {
        await this.svc.activate(u.id);
      }
      await this.loadUsers();
    } catch {
      alert('Error al cambiar el estado del usuario.');
    }
  }
}
