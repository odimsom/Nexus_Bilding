import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ApiService } from '../../../core/services/api.service';

export interface AppUser {
  id: string;
  username: string;
  email: string;
  fullName: string;
  employeeNo: string;
  isActive: boolean;
  groupCode: string;
  groupName: string;
}

export interface UserGroup {
  code: string;
  name: string;
  assignToAllNewUsers: boolean;
}

export interface CreateUserData {
  username: string;
  email: string;
  password: string;
  fullName: string;
  employeeNo: string;
  groupCode: string;
}

export interface UpdateUserData {
  fullName: string;
  email: string;
  employeeNo: string;
  groupCode: string;
  newPassword?: string;
}

@Injectable({ providedIn: 'root' })
export class UserService {
  private readonly api = inject(ApiService);

  readonly users  = signal<AppUser[]>([]);
  readonly groups = signal<UserGroup[]>([]);
  readonly loading = signal(false);

  async loadUsers(): Promise<void> {
    this.loading.set(true);
    try {
      const data = await firstValueFrom(this.api.get<AppUser[]>('security/users'));
      this.users.set(data);
    } catch { this.users.set([]); }
    finally { this.loading.set(false); }
  }

  async loadGroups(): Promise<void> {
    try {
      const data = await firstValueFrom(this.api.get<UserGroup[]>('security/groups'));
      this.groups.set(data);
    } catch { this.groups.set([]); }
  }

  async create(data: CreateUserData): Promise<void> {
    await firstValueFrom(this.api.post<{ id: string }>('security/users', data));
    await this.loadUsers();
  }

  async update(id: string, data: UpdateUserData): Promise<void> {
    await firstValueFrom(this.api.put<boolean>(`security/users/${id}`, data));
    await this.loadUsers();
  }

  async activate(id: string): Promise<void> {
    await firstValueFrom(this.api.patch<boolean>(`security/users/${id}/activate`));
    await this.loadUsers();
  }

  async deactivate(id: string): Promise<void> {
    await firstValueFrom(this.api.patch<boolean>(`security/users/${id}/deactivate`));
    await this.loadUsers();
  }
}
