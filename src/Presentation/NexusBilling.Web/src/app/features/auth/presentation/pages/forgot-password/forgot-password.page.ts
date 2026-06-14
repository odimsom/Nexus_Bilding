import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../../../../core/services/api.service';
import { firstValueFrom } from 'rxjs';

type Step = 'form' | 'sent';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './forgot-password.page.html',
  styleUrl: './forgot-password.page.css'
})
export class ForgotPasswordPage {
  private readonly api = inject(ApiService);

  email = '';
  readonly step    = signal<Step>('form');
  readonly loading = signal(false);
  readonly error   = signal('');

  async submit(): Promise<void> {
    if (!this.email.trim()) return;
    if (!this.email.includes('@')) {
      this.error.set('Ingresá un correo válido.');
      return;
    }
    this.loading.set(true);
    this.error.set('');
    try {
      await firstValueFrom(this.api.post('auth/forgot-password', { email: this.email.trim() }));
    } catch {
      // Always show success to avoid email enumeration
    } finally {
      this.loading.set(false);
      this.step.set('sent');
    }
  }
}
