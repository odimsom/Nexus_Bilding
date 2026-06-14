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
  templateUrl: './user-list.page.html',
  styleUrl: './user-list.page.css'
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
  
  form = { username: '', email: '', fullName: '', password: '', employeeNo: '', groupCode: '' };

  editingId = signal<string | null>(null);
  editForm = { fullName: '', email: '', employeeNo: '', groupCode: '', newPassword: '' };

  async ngOnInit() {
    await Promise.all([this.svc.loadUsers(), this.svc.loadGroups()]);
  }

  async loadUsers() {
    await this.svc.loadUsers();
  }

  openModal() {
    this.form = { username: '', email: '', fullName: '', password: '', employeeNo: '', groupCode: '' };
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
      await this.svc.create({ ...this.form });
      this.closeModal();
    } catch (e: any) {
      this.modalError.set(e?.error?.error?.message ?? 'Error al crear usuario.');
    } finally {
      this.saving.set(false);
    }
  }

  openEdit(u: AppUser): void {
    this.editingId.set(u.id);
    this.editForm = {
      fullName: u.fullName,
      email: u.email,
      employeeNo: u.employeeNo || '',
      groupCode: u.groupCode || '',
      newPassword: ''
    };
    this.modalError.set(null);
  }

  closeEdit(): void {
    this.editingId.set(null);
  }

  async saveEdit(): Promise<void> {
    if (!this.editForm.email?.trim()) {
      this.modalError.set('El email es obligatorio.');
      return;
    }
    this.saving.set(true);
    try {
      await this.svc.update(this.editingId()!, {
        fullName: this.editForm.fullName,
        email: this.editForm.email,
        employeeNo: this.editForm.employeeNo,
        groupCode: this.editForm.groupCode,
        newPassword: this.editForm.newPassword || undefined
      });
      this.closeEdit();
    } catch (e: any) {
      this.modalError.set(e?.error?.error?.message ?? 'Error al actualizar usuario.');
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
    } catch {
      alert('Error al cambiar el estado del usuario.');
    }
  }
}
