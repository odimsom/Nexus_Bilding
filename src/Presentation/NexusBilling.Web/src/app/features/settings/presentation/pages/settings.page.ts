import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NoSeriesService, NoSeriesItem } from '../../data/no-series.service';
import { UserService, AppUser } from '../../../../features/security/data/user.service';
import { PaymentTermsService } from '../../../../core/services/payment-terms.service';
import { PaymentMethodService } from '../../../../core/services/payment-method.service';
import { CurrencyService } from '../../../../core/services/currency.service';
import { SalespersonService } from '../../../../core/services/salesperson.service';

type SettingsTab = 'company' | 'ncf' | 'payment' | 'users' | 'posting' | 'sequences' | 'ecf' | 'salespersons';

interface NcfSeries {
  type: string;
  typeLabel: string;
  prefix: string;
  currentNo: number;
  toNo: number;
  expiryDate: string;
  active: boolean;
}

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './settings.page.html',
  styleUrl: './settings.page.css'
})
export class SettingsPage implements OnInit {
  readonly activeTab   = signal<SettingsTab>('company');
  readonly noSeriesSvc = inject(NoSeriesService);
  readonly paymentTermsSvc = inject(PaymentTermsService);
  readonly paymentMethodSvc = inject(PaymentMethodService);
  readonly currencySvc = inject(CurrencyService);
  readonly salespersonSvc = inject(SalespersonService);
  private readonly router = inject(Router);
  private readonly route  = inject(ActivatedRoute);

  readonly tabs: { id: SettingsTab; label: string }[] = [
    { id: 'company',      label: 'Empresa' },
    { id: 'ncf',          label: 'Secuencias NCF' },
    { id: 'ecf',          label: 'Facturación Electrónica' },
    { id: 'payment',      label: 'Pago' },
    { id: 'salespersons', label: 'Vendedores' },
    { id: 'posting',      label: 'Contabilización' },
    { id: 'users',        label: 'Usuarios' },
    { id: 'sequences',    label: 'Secuencias' },
  ];

  // ── Payment Method modal ────────────────────────────────────────
  readonly showPmModal = signal(false);
  readonly pmSaving    = signal(false);
  readonly pmError     = signal('');
  pmForm = { isNew: true, code: '', description: '' };

  openPmModal(): void {
    this.pmForm = { isNew: true, code: '', description: '' };
    this.pmError.set('');
    this.showPmModal.set(true);
  }
  editPmModal(pm: { code: string; description: string }): void {
    this.pmForm = { isNew: false, code: pm.code, description: pm.description };
    this.pmError.set('');
    this.showPmModal.set(true);
  }
  closePmModal(): void { this.showPmModal.set(false); }

  async savePm(): Promise<void> {
    if (!this.pmForm.code.trim() || !this.pmForm.description.trim()) {
      this.pmError.set('Código y descripción son obligatorios.');
      return;
    }
    this.pmSaving.set(true);
    this.pmError.set('');
    try {
      if (this.pmForm.isNew) await this.paymentMethodSvc.create(this.pmForm.code, this.pmForm.description);
      else await this.paymentMethodSvc.update(this.pmForm.code, this.pmForm.description);
      this.closePmModal();
    } catch (e: any) {
      this.pmError.set(e?.error?.error?.message ?? 'Error al guardar.');
    } finally {
      this.pmSaving.set(false);
    }
  }

  async deletePm(code: string): Promise<void> {
    if (!confirm(`¿Eliminar el método de pago "${code}"?`)) return;
    try { await this.paymentMethodSvc.delete(code); } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al eliminar.');
    }
  }

  // ── Currency modal ───────────────────────────────────────────────
  readonly showCurrModal = signal(false);
  readonly currSaving    = signal(false);
  readonly currError     = signal('');
  currForm = { isNew: true, code: '', description: '', symbol: '' };

  openCurrModal(): void {
    this.currForm = { isNew: true, code: '', description: '', symbol: '' };
    this.currError.set('');
    this.showCurrModal.set(true);
  }
  editCurrModal(c: { code: string; description: string; symbol: string }): void {
    this.currForm = { isNew: false, code: c.code, description: c.description, symbol: c.symbol };
    this.currError.set('');
    this.showCurrModal.set(true);
  }
  closeCurrModal(): void { this.showCurrModal.set(false); }

  async saveCurr(): Promise<void> {
    if (!this.currForm.code.trim() || !this.currForm.description.trim()) {
      this.currError.set('Código y descripción son obligatorios.');
      return;
    }
    this.currSaving.set(true);
    this.currError.set('');
    try {
      if (this.currForm.isNew) await this.currencySvc.create(this.currForm.code, this.currForm.description, this.currForm.symbol || this.currForm.code);
      else await this.currencySvc.update(this.currForm.code, this.currForm.description, this.currForm.symbol);
      this.closeCurrModal();
    } catch (e: any) {
      this.currError.set(e?.error?.error?.message ?? 'Error al guardar.');
    } finally {
      this.currSaving.set(false);
    }
  }

  async deleteCurr(code: string): Promise<void> {
    if (!confirm(`¿Eliminar la moneda "${code}"?`)) return;
    try { await this.currencySvc.delete(code); } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al eliminar.');
    }
  }

  // ── Salesperson modal ────────────────────────────────────────────
  readonly showSpModal = signal(false);
  readonly spSaving    = signal(false);
  readonly spError     = signal('');
  spForm = { isNew: true, code: '', name: '', email: '', phone: '', jobTitle: '' };

  openSpModal(): void {
    this.spForm = { isNew: true, code: '', name: '', email: '', phone: '', jobTitle: '' };
    this.spError.set('');
    this.showSpModal.set(true);
  }
  editSpModal(s: { code: string; name: string; email?: string; phone?: string; jobTitle?: string }): void {
    this.spForm = { isNew: false, code: s.code, name: s.name, email: s.email ?? '', phone: s.phone ?? '', jobTitle: s.jobTitle ?? '' };
    this.spError.set('');
    this.showSpModal.set(true);
  }
  closeSpModal(): void { this.showSpModal.set(false); }

  async saveSp(): Promise<void> {
    if (!this.spForm.code.trim() || !this.spForm.name.trim()) {
      this.spError.set('Código y nombre son obligatorios.');
      return;
    }
    this.spSaving.set(true);
    this.spError.set('');
    try {
      if (this.spForm.isNew) await this.salespersonSvc.create(this.spForm.code, this.spForm.name, this.spForm.email, this.spForm.phone, this.spForm.jobTitle);
      else await this.salespersonSvc.update(this.spForm.code, this.spForm.name, this.spForm.email, this.spForm.phone, this.spForm.jobTitle);
      this.closeSpModal();
    } catch (e: any) {
      this.spError.set(e?.error?.error?.message ?? 'Error al guardar.');
    } finally {
      this.spSaving.set(false);
    }
  }

  async deleteSp(code: string): Promise<void> {
    if (!confirm(`¿Eliminar el vendedor "${code}"?`)) return;
    try { await this.salespersonSvc.delete(code); } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al eliminar.');
    }
  }

  // ── ECF form state ──────────────────────────────────────────────
  ecfForm = {
    rnc: '101-23456-7',
    representativeName: 'Francisco Castro',
    environment: 0,
    p12Path: 'certificados/nexus_billing.p12',
    p12Password: '',
    isActive: true
  };

  // ── NoSeries form state ─────────────────────────────────────────
  readonly showSeriesModal = signal(false);
  readonly seriesSaving    = signal(false);
  readonly seriesError     = signal('');
  editingCode: string | null = null;

  seriesForm: {
    code: string; description: string; defaultNos: boolean; manualNos: boolean;
    startingNo: string; endingNo: string; incrementByNo: number;
  } = { code: '', description: '', defaultNos: true, manualNos: false, startingNo: '', endingNo: '', incrementByNo: 1 };

  readonly userSvc = inject(UserService);

  // ── User modal state ────────────────────────────────────────────
  readonly showUserModal = signal(false);
  readonly userSaving    = signal(false);
  readonly userError     = signal('');
  editingUserId: string | null = null;

  userForm: {
    username: string; password: string; newPassword: string;
    fullName: string; email: string; employeeNo: string; groupCode: string;
  } = { username: '', password: '', newPassword: '', fullName: '', email: '', employeeNo: '', groupCode: '' };

  private returnTo: string | null = null;

  async ngOnInit(): Promise<void> {
    const params = this.route.snapshot.queryParamMap;
    const tab = params.get('tab') as SettingsTab | null;
    if (tab) this.activeTab.set(tab);
    this.returnTo = params.get('returnTo');

    await Promise.all([
      this.noSeriesSvc.load(),
      this.userSvc.loadUsers(),
      this.userSvc.loadGroups(),
      this.paymentTermsSvc.load(),
      this.paymentMethodSvc.load(),
      this.currencySvc.load(),
      this.salespersonSvc.load(),
    ]);
  }

  openNewUser(): void {
    this.editingUserId = null;
    this.userForm = { username: '', password: '', newPassword: '', fullName: '', email: '', employeeNo: '', groupCode: '' };
    this.userError.set('');
    this.showUserModal.set(true);
  }

  editUser(u: AppUser): void {
    this.editingUserId = u.id;
    this.userForm = { username: u.username, password: '', newPassword: '', fullName: u.fullName, email: u.email, employeeNo: u.employeeNo, groupCode: u.groupCode };
    this.userError.set('');
    this.showUserModal.set(true);
  }

  closeUserModal(): void { this.showUserModal.set(false); }

  async saveUser(): Promise<void> {
    if (!this.userForm.email.trim()) { this.userError.set('El email es obligatorio.'); return; }
    if (!this.editingUserId && !this.userForm.username.trim()) { this.userError.set('El usuario es obligatorio.'); return; }
    if (!this.editingUserId && !this.userForm.password) { this.userError.set('La contraseña es obligatoria.'); return; }
    this.userSaving.set(true);
    this.userError.set('');
    try {
      if (this.editingUserId) {
        await this.userSvc.update(this.editingUserId, {
          fullName: this.userForm.fullName, email: this.userForm.email,
          employeeNo: this.userForm.employeeNo, groupCode: this.userForm.groupCode,
          newPassword: this.userForm.newPassword || undefined
        });
      } else {
        await this.userSvc.create({
          username: this.userForm.username, email: this.userForm.email,
          password: this.userForm.password, fullName: this.userForm.fullName,
          employeeNo: this.userForm.employeeNo, groupCode: this.userForm.groupCode
        });
      }
      this.showUserModal.set(false);
    } catch (e: any) {
      this.userError.set(e?.message ?? 'Error al guardar el usuario.');
    } finally {
      this.userSaving.set(false);
    }
  }

  async toggleUserActive(id: string, active: boolean): Promise<void> {
    if (active) await this.userSvc.activate(id);
    else await this.userSvc.deactivate(id);
  }

  openNewSeries(): void {
    this.editingCode = null;
    this.seriesForm = { code: '', description: '', defaultNos: true, manualNos: false, startingNo: '', endingNo: '', incrementByNo: 1 };
    this.seriesError.set('');
    this.showSeriesModal.set(true);
  }

  editSeries(s: NoSeriesItem): void {
    this.editingCode = s.code;
    this.seriesForm = {
      code: s.code, description: s.description, defaultNos: s.defaultNos, manualNos: s.manualNos,
      startingNo: s.startingNo ?? '', endingNo: s.endingNo ?? '', incrementByNo: s.incrementByNo
    };
    this.seriesError.set('');
    this.showSeriesModal.set(true);
  }

  closeSeriesModal(): void { this.showSeriesModal.set(false); }

  async saveSeries(): Promise<void> {
    if (!this.seriesForm.code.trim() || !this.seriesForm.description.trim()) {
      this.seriesError.set('El código y la descripción son obligatorios.');
      return;
    }
    if (!this.seriesForm.startingNo.trim()) {
      this.seriesError.set('El número inicial es obligatorio para generar documentos.');
      return;
    }
    this.seriesSaving.set(true);
    this.seriesError.set('');
    try {
      await this.noSeriesSvc.save({
        code: this.seriesForm.code.trim().toUpperCase(),
        description: this.seriesForm.description.trim(),
        defaultNos: this.seriesForm.defaultNos,
        manualNos: this.seriesForm.manualNos,
        startingNo: this.seriesForm.startingNo.trim() || null,
        endingNo: this.seriesForm.endingNo.trim() || null,
        lastNoUsed: null,
        incrementByNo: this.seriesForm.incrementByNo || 1,
        open: true
      });
      this.showSeriesModal.set(false);
      if (this.returnTo) {
        const dest = this.returnTo;
        this.returnTo = null;
        this.router.navigate([dest]);
      }
    } catch (e: any) {
      this.seriesError.set(e?.message ?? 'Error al guardar la serie.');
    } finally {
      this.seriesSaving.set(false);
    }
  }

  async deleteSeries(code: string): Promise<void> {
    if (!confirm(`¿Eliminar la serie "${code}"? Esta acción no se puede deshacer.`)) return;
    await this.noSeriesSvc.delete(code);
  }

  nextNoPreview(times = 1): string {
    const no = this.seriesForm.startingNo;
    if (!no) return '';
    const match = no.match(/^(.*?)(\d+)$/);
    if (!match) return no;
    const [, prefix, digits] = match;
    const next = parseInt(digits, 10) + (this.seriesForm.incrementByNo || 1) * times;
    return prefix + String(next).padStart(digits.length, '0');
  }

  readonly ncfSeries: NcfSeries[] = [
    { type: 'B01', typeLabel: 'Crédito Fiscal',           prefix: 'B01', currentNo: 95000050, toNo: 95000500, expiryDate: '2025-12-31', active: true },
    { type: 'B02', typeLabel: 'Consumidor Final',          prefix: 'B02', currentNo: 92000120, toNo: 92000500, expiryDate: '2025-12-31', active: true },
    { type: 'B04', typeLabel: 'Nota de Débito',            prefix: 'B04', currentNo: 94000001, toNo: 94000100, expiryDate: '2025-12-31', active: true },
    { type: 'B14', typeLabel: 'Regímenes Especiales',      prefix: 'B14', currentNo: 97000001, toNo: 97000050, expiryDate: '2025-12-31', active: false },
    { type: 'B15', typeLabel: 'Gubernamentales',           prefix: 'B15', currentNo: 98000001, toNo: 98000050, expiryDate: '2025-12-31', active: false },
    { type: 'B16', typeLabel: 'Zonas Francas / Exportación', prefix: 'B16', currentNo: 99000001, toNo: 99000050, expiryDate: '2025-12-31', active: false },
  ];

  available(s: NcfSeries): number {
    return s.toNo - s.currentNo;
  }
}
