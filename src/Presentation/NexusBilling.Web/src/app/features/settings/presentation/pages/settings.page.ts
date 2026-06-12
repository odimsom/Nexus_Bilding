import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NoSeriesService, NoSeriesItem } from '../../data/no-series.service';

type SettingsTab = 'company' | 'ncf' | 'payment' | 'users' | 'posting' | 'sequences';

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
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Configuración</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-6);">
      <div>
        <h1 class="nx-page-title">Configuración</h1>
        <p class="nx-page-subtitle">Ajusta la información de tu empresa, secuencias NCF y parámetros del sistema.</p>
      </div>
    </div>

    <!-- Tab navigation -->
    <div class="settings-tabs" style="margin-bottom:var(--nx-space-5);">
      @for (tab of tabs; track tab.id) {
        <button
          class="settings-tab"
          [class.active]="activeTab() === tab.id"
          (click)="activeTab.set(tab.id)"
        >
          {{ tab.label }}
        </button>
      }
    </div>

    <!-- ── EMPRESA ─────────────────────────────────────────────── -->
    @if (activeTab() === 'company') {
      <div style="max-width:680px;">
        <div class="nx-card" style="margin-bottom:var(--nx-space-4);">
          <div class="nx-card__head">
            <div class="nx-card__title">Información de la Empresa</div>
            <div class="nx-card__actions">
              <button class="nx-btn nx-btn--primary nx-btn--sm">Guardar</button>
            </div>
          </div>
          <div class="nx-card__body" style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
            <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
              <label class="nx-field">
                <span class="nx-label">Nombre comercial</span>
                <input type="text" class="nx-input" value="Nexus Demo S.R.L." />
              </label>
              <label class="nx-field">
                <span class="nx-label">RNC</span>
                <input type="text" class="nx-input" placeholder="1-23-45678-9" />
              </label>
            </div>
            <label class="nx-field">
              <span class="nx-label">Dirección</span>
              <input type="text" class="nx-input" placeholder="Av. Principal #123" />
            </label>
            <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
              <label class="nx-field">
                <span class="nx-label">Ciudad</span>
                <input type="text" class="nx-input" value="Santo Domingo" />
              </label>
              <label class="nx-field">
                <span class="nx-label">País</span>
                <select class="nx-select">
                  <option value="DO" selected>República Dominicana</option>
                  <option value="US">Estados Unidos</option>
                </select>
              </label>
            </div>
            <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
              <label class="nx-field">
                <span class="nx-label">Teléfono</span>
                <input type="tel" class="nx-input" placeholder="809-000-0000" />
              </label>
              <label class="nx-field">
                <span class="nx-label">Email corporativo</span>
                <input type="email" class="nx-input" placeholder="info@empresa.com" />
              </label>
            </div>
            <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
              <label class="nx-field">
                <span class="nx-label">Moneda base</span>
                <select class="nx-select">
                  <option value="DOP" selected>DOP — Peso Dominicano</option>
                  <option value="USD">USD — Dólar</option>
                </select>
              </label>
              <label class="nx-field">
                <span class="nx-label">Ejercicio fiscal</span>
                <select class="nx-select">
                  <option value="JAN" selected>Enero–Diciembre</option>
                  <option value="APR">Abril–Marzo</option>
                </select>
              </label>
            </div>
          </div>
        </div>

        <div class="nx-card">
          <div class="nx-card__head">
            <div class="nx-card__title">Logo de la Empresa</div>
          </div>
          <div class="nx-card__body">
            <div style="display:flex;align-items:center;gap:var(--nx-space-4);">
              <div style="width:80px;height:80px;border:2px dashed var(--nx-border);border-radius:var(--nx-radius-md);display:flex;align-items:center;justify-content:center;color:var(--nx-text-faint);">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" style="width:32px;height:32px;"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="17 8 12 3 7 8"/><line x1="12" y1="3" x2="12" y2="15"/></svg>
              </div>
              <div>
                <p style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);margin-bottom:var(--nx-space-2);">PNG, JPG o SVG. Máximo 2MB.</p>
                <button class="nx-btn nx-btn--secondary nx-btn--sm">Subir logo</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    }

    <!-- ── NCF ─────────────────────────────────────────────────── -->
    @if (activeTab() === 'ncf') {
      <div>
        <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-4);">
          <div>
            <h2 style="font-size:var(--nx-text-base);font-weight:var(--nx-weight-semibold);">Secuencias NCF — DGII</h2>
            <p style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Administra los números de comprobante fiscal autorizados por la DGII.</p>
          </div>
          <button class="nx-btn nx-btn--primary nx-btn--sm">+ Nueva Secuencia</button>
        </div>

        <div class="nx-card" style="overflow:hidden;">
          <div style="overflow-x:auto;">
            <table class="nx-table" aria-label="Secuencias NCF">
              <thead>
                <tr>
                  <th>Tipo NCF</th>
                  <th>Prefijo</th>
                  <th class="nx-th--num">No. Actual</th>
                  <th class="nx-th--num">Hasta No.</th>
                  <th class="nx-th--num">Disponibles</th>
                  <th>Vence</th>
                  <th>Estado</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                @for (s of ncfSeries; track s.type) {
                  <tr>
                    <td>
                      <div style="font-weight:var(--nx-weight-medium);">{{ s.typeLabel }}</div>
                      <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);font-family:var(--nx-font-mono);">{{ s.type }}</div>
                    </td>
                    <td class="nx-td--doc">{{ s.prefix }}</td>
                    <td class="nx-td--num nx-num">{{ s.currentNo | number:'8.0-0' }}</td>
                    <td class="nx-td--num nx-num">{{ s.toNo | number:'8.0-0' }}</td>
                    <td class="nx-td--num">
                      <span class="nx-num" [style.color]="available(s) < 100 ? 'var(--nx-money-negative)' : available(s) < 500 ? 'var(--nx-amber-600)' : ''">
                        {{ available(s) | number:'1.0-0' }}
                      </span>
                    </td>
                    <td style="font-size:var(--nx-text-sm);">{{ s.expiryDate }}</td>
                    <td>
                      @if (s.active) {
                        <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activa</span>
                      } @else {
                        <span class="nx-badge nx-badge--outline">Inactiva</span>
                      }
                    </td>
                    <td>
                      <button class="nx-btn nx-btn--ghost nx-btn--sm">Editar</button>
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        </div>

        <div class="nx-callout nx-callout--info" style="margin-top:var(--nx-space-4);">
          <strong>Nota DGII:</strong> Los NCF deben solicitarse en la Oficina Virtual de la DGII (dgii.gov.do). Las secuencias con menos de 100 disponibles requieren renovación inmediata.
        </div>
      </div>
    }

    <!-- ── CONDICIONES DE PAGO ─────────────────────────────────── -->
    @if (activeTab() === 'payment') {
      <div>
        <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-4);">
          <div>
            <h2 style="font-size:var(--nx-text-base);font-weight:var(--nx-weight-semibold);">Condiciones y Métodos de Pago</h2>
            <p style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Define los plazos y formas de pago disponibles en el sistema.</p>
          </div>
        </div>

        <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Condiciones de Pago</div>
              <div class="nx-card__actions">
                <button class="nx-btn nx-btn--ghost nx-btn--sm">+ Agregar</button>
              </div>
            </div>
            <div style="overflow:hidden;">
              <table class="nx-table">
                <thead><tr><th>Código</th><th>Descripción</th><th>Días</th><th></th></tr></thead>
                <tbody>
                  @for (pt of paymentTerms; track pt.code) {
                    <tr>
                      <td class="nx-td--doc">{{ pt.code }}</td>
                      <td>{{ pt.desc }}</td>
                      <td class="nx-td--num nx-num">{{ pt.days }}</td>
                      <td><button class="nx-btn nx-btn--ghost nx-btn--sm">Editar</button></td>
                    </tr>
                  }
                </tbody>
              </table>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Métodos de Pago</div>
              <div class="nx-card__actions">
                <button class="nx-btn nx-btn--ghost nx-btn--sm">+ Agregar</button>
              </div>
            </div>
            <div style="overflow:hidden;">
              <table class="nx-table">
                <thead><tr><th>Código</th><th>Descripción</th><th></th></tr></thead>
                <tbody>
                  @for (pm of paymentMethods; track pm.code) {
                    <tr>
                      <td class="nx-td--doc">{{ pm.code }}</td>
                      <td>{{ pm.desc }}</td>
                      <td><button class="nx-btn nx-btn--ghost nx-btn--sm">Editar</button></td>
                    </tr>
                  }
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    }

    <!-- ── USUARIOS ─────────────────────────────────────────────── -->
    @if (activeTab() === 'users') {
      <div>
        <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-4);">
          <div>
            <h2 style="font-size:var(--nx-text-base);font-weight:var(--nx-weight-semibold);">Usuarios y Roles</h2>
            <p style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Gestiona los usuarios del sistema y sus permisos de acceso.</p>
          </div>
          <button class="nx-btn nx-btn--primary nx-btn--sm">+ Invitar Usuario</button>
        </div>

        <div class="nx-card" style="overflow:hidden;">
          <div style="overflow-x:auto;">
            <table class="nx-table" aria-label="Usuarios">
              <thead>
                <tr>
                  <th>Usuario</th>
                  <th>Email</th>
                  <th>Rol</th>
                  <th>Último acceso</th>
                  <th>Estado</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                @for (u of users; track u.email) {
                  <tr>
                    <td>
                      <div style="display:flex;align-items:center;gap:var(--nx-space-2);">
                        <div class="nx-avatar nx-avatar--sm nx-avatar--circle" style="background:var(--nx-indigo-100);color:var(--nx-indigo-800);">
                          {{ u.name.charAt(0) }}
                        </div>
                        <span style="font-weight:var(--nx-weight-medium);">{{ u.name }}</span>
                      </div>
                    </td>
                    <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ u.email }}</td>
                    <td>
                      <span class="nx-badge nx-badge--outline">{{ u.role }}</span>
                    </td>
                    <td style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ u.lastAccess }}</td>
                    <td>
                      @if (u.active) {
                        <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
                      } @else {
                        <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Suspendido</span>
                      }
                    </td>
                    <td>
                      <button class="nx-btn nx-btn--ghost nx-btn--sm">Editar</button>
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
        </div>
      </div>
    }

    <!-- ── GRUPOS CONTABILIZACIÓN ──────────────────────────────── -->
    @if (activeTab() === 'posting') {
      <div>
        <div style="margin-bottom:var(--nx-space-4);">
          <h2 style="font-size:var(--nx-text-base);font-weight:var(--nx-weight-semibold);">Grupos de Contabilización</h2>
          <p style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Configuración del Mayor General y las cuentas contables por defecto.</p>
        </div>

        <div class="nx-callout nx-callout--info" style="margin-bottom:var(--nx-space-4);">
          Los grupos de contabilización definen a qué cuenta del Mayor General se registran las transacciones. Esta configuración requiere conocimiento contable.
        </div>

        <div style="display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4);">
          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Grupos de Clientes</div>
              <div class="nx-card__actions">
                <button class="nx-btn nx-btn--ghost nx-btn--sm">+ Agregar</button>
              </div>
            </div>
            <div style="overflow:hidden;">
              <table class="nx-table">
                <thead><tr><th>Código</th><th>Descripción</th><th>Cta. Cobrar</th></tr></thead>
                <tbody>
                  <tr><td class="nx-td--doc">DOMESTIC</td><td>Clientes locales</td><td class="nx-num">1101-001</td></tr>
                  <tr><td class="nx-td--doc">FOREIGN</td><td>Clientes extranjeros</td><td class="nx-num">1101-002</td></tr>
                  <tr><td class="nx-td--doc">GOVERNMENT</td><td>Instituciones gobierno</td><td class="nx-num">1101-003</td></tr>
                </tbody>
              </table>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head">
              <div class="nx-card__title">Grupos de Proveedores</div>
              <div class="nx-card__actions">
                <button class="nx-btn nx-btn--ghost nx-btn--sm">+ Agregar</button>
              </div>
            </div>
            <div style="overflow:hidden;">
              <table class="nx-table">
                <thead><tr><th>Código</th><th>Descripción</th><th>Cta. Pagar</th></tr></thead>
                <tbody>
                  <tr><td class="nx-td--doc">LOCAL</td><td>Proveedores locales</td><td class="nx-num">2101-001</td></tr>
                  <tr><td class="nx-td--doc">IMPORT</td><td>Proveedores importación</td><td class="nx-num">2101-002</td></tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    }

    <!-- ── SECUENCIAS NUMÉRICAS ──────────────────────────────────── -->
    @if (activeTab() === 'sequences') {
      <div>
        <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:var(--nx-space-4);">
          <div>
            <div style="font-weight:var(--nx-weight-semibold);">Secuencias Numéricas de Documentos</div>
            <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">Define cómo se numeran automáticamente los pedidos, facturas y otros documentos.</div>
          </div>
          <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openNewSeries()">+ Nueva Serie</button>
        </div>

        @if (noSeriesSvc.loading()) {
          <div class="nx-empty"><div class="nx-spinner"></div></div>
        } @else if (noSeriesSvc.items().length === 0) {
          <div class="nx-empty">
            <p class="nx-empty__title">Sin secuencias configuradas</p>
            <p class="nx-empty__text">Crea una serie para empezar a numerar documentos.</p>
          </div>
        } @else {
          <div class="nx-card">
            <table class="nx-table">
              <thead>
                <tr>
                  <th>Código</th>
                  <th>Descripción</th>
                  <th>No. Inicial</th>
                  <th>No. Final</th>
                  <th>Último No. Usado</th>
                  <th class="nx-th--num">Incremento</th>
                  <th>Estado</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                @for (s of noSeriesSvc.items(); track s.code) {
                  <tr>
                    <td class="nx-td--doc" style="font-weight:var(--nx-weight-semibold);">{{ s.code }}</td>
                    <td>{{ s.description }}</td>
                    <td class="nx-num">{{ s.startingNo || '—' }}</td>
                    <td class="nx-num">{{ s.endingNo || '∞' }}</td>
                    <td class="nx-num" style="color:var(--nx-text-muted);">{{ s.lastNoUsed || '—' }}</td>
                    <td class="nx-td--num">{{ s.incrementByNo }}</td>
                    <td>
                      @if (s.open) {
                        <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activa</span>
                      } @else {
                        <span class="nx-badge nx-badge--outline">Cerrada</span>
                      }
                    </td>
                    <td style="display:flex;gap:var(--nx-space-2);">
                      <button class="nx-iconbtn nx-iconbtn--sm" (click)="editSeries(s)" title="Editar">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
                      </button>
                      <button class="nx-iconbtn nx-iconbtn--sm" (click)="deleteSeries(s.code)" title="Eliminar" style="color:var(--nx-red-500);">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/></svg>
                      </button>
                    </td>
                  </tr>
                }
              </tbody>
            </table>
          </div>
          <div class="nx-callout nx-callout--info" style="margin-top:var(--nx-space-3);">
            Ejemplos de formato: <code>PV-00001</code> → <code>PV-00002</code>, <code>FAC-000001</code> → <code>FAC-000002</code>. El sistema incrementa automáticamente el sufijo numérico.
          </div>
        }

        <!-- Series edit modal -->
        @if (showSeriesModal()) {
          <div class="modal-backdrop" (click)="closeSeriesModal()">
            <div class="modal-box" (click)="$event.stopPropagation()">
              <div class="modal-header">
                <h2 class="nx-page-title" style="margin:0;">{{ seriesForm.code ? 'Editar Serie: ' + seriesForm.code : 'Nueva Serie' }}</h2>
                <button class="nx-iconbtn" (click)="closeSeriesModal()">
                  <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>
              </div>
              <div class="modal-body">
                @if (seriesError()) {
                  <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ seriesError() }}</div>
                }
                <div class="form-grid">
                  <div class="nx-field">
                    <label class="nx-label">Código *</label>
                    <input class="nx-input" [(ngModel)]="seriesForm.code" [disabled]="!!editingCode" [style.opacity]="editingCode ? '.6' : '1'" placeholder="Ej: PV, FAC, COT" />
                    <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Máximo 10 caracteres, mayúsculas</span>
                  </div>
                  <div class="nx-field">
                    <label class="nx-label">Descripción *</label>
                    <input class="nx-input" [(ngModel)]="seriesForm.description" placeholder="Pedidos de Venta" />
                  </div>
                  <div class="nx-field">
                    <label class="nx-label">No. Inicial *</label>
                    <input class="nx-input nx-num" [(ngModel)]="seriesForm.startingNo" placeholder="PV-00001" />
                    <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Define el formato y relleno de ceros</span>
                  </div>
                  <div class="nx-field">
                    <label class="nx-label">No. Final</label>
                    <input class="nx-input nx-num" [(ngModel)]="seriesForm.endingNo" placeholder="PV-99999 (opcional)" />
                  </div>
                  <div class="nx-field">
                    <label class="nx-label">Incremento</label>
                    <input class="nx-input nx-num" [(ngModel)]="seriesForm.incrementByNo" type="number" min="1" placeholder="1" />
                  </div>
                  <div class="nx-field" style="display:flex;flex-direction:column;justify-content:flex-end;gap:var(--nx-space-2);">
                    <label style="display:flex;align-items:center;gap:var(--nx-space-2);cursor:pointer;">
                      <input type="checkbox" [(ngModel)]="seriesForm.defaultNos" />
                      <span style="font-size:var(--nx-text-sm);">Usar por defecto (automático)</span>
                    </label>
                    <label style="display:flex;align-items:center;gap:var(--nx-space-2);cursor:pointer;">
                      <input type="checkbox" [(ngModel)]="seriesForm.manualNos" />
                      <span style="font-size:var(--nx-text-sm);">Permitir número manual</span>
                    </label>
                  </div>
                </div>
                @if (seriesForm.startingNo) {
                  <div style="margin-top:var(--nx-space-3);padding:var(--nx-space-3);background:var(--nx-canvas);border-radius:var(--nx-radius-md);font-size:var(--nx-text-sm);">
                    <span style="color:var(--nx-text-muted);">Vista previa: </span>
                    <code style="font-weight:var(--nx-weight-semibold);">{{ seriesForm.startingNo }}</code>
                    <span style="color:var(--nx-text-muted);"> → </span>
                    <code style="font-weight:var(--nx-weight-semibold);">{{ nextNoPreview() }}</code>
                    <span style="color:var(--nx-text-muted);"> → </span>
                    <code style="font-weight:var(--nx-weight-semibold);">{{ nextNoPreview(2) }}</code>
                    …
                  </div>
                }
              </div>
              <div class="modal-footer">
                <button class="nx-btn nx-btn--ghost" (click)="closeSeriesModal()">Cancelar</button>
                <button class="nx-btn nx-btn--primary" [disabled]="seriesSaving()" (click)="saveSeries()">
                  @if (seriesSaving()) { Guardando… } @else { Guardar Serie }
                </button>
              </div>
            </div>
          </div>
        }
      </div>
    }
  `,
  styles: [`
    :host { display: block; }

    .settings-tabs {
      display: flex;
      gap: 2px;
      border-bottom: 1px solid var(--nx-border);
    }

    .settings-tab {
      padding: 8px 16px;
      font-size: var(--nx-text-sm);
      font-weight: var(--nx-weight-medium);
      color: var(--nx-text-muted);
      background: transparent;
      border: none;
      border-bottom: 2px solid transparent;
      margin-bottom: -1px;
      cursor: pointer;
      transition: color 0.15s, border-color 0.15s;
    }

    .settings-tab:hover { color: var(--nx-text-body); }

    .settings-tab.active {
      color: var(--nx-action);
      border-bottom-color: var(--nx-action);
    }

    .nx-callout {
      padding: var(--nx-space-3) var(--nx-space-4);
      border-radius: var(--nx-radius-md);
      font-size: var(--nx-text-sm);
    }
    .nx-callout--info {
      background: var(--nx-indigo-50, color-mix(in srgb, var(--nx-action) 8%, transparent));
      border: 1px solid color-mix(in srgb, var(--nx-action) 20%, transparent);
      color: var(--nx-text-body);
    }
  `]
})
export class SettingsPage implements OnInit {
  readonly activeTab = signal<SettingsTab>('company');
  readonly noSeriesSvc = inject(NoSeriesService);

  readonly tabs: { id: SettingsTab; label: string }[] = [
    { id: 'company',   label: 'Empresa' },
    { id: 'ncf',       label: 'Secuencias NCF' },
    { id: 'payment',   label: 'Pago' },
    { id: 'posting',   label: 'Contabilización' },
    { id: 'users',     label: 'Usuarios' },
    { id: 'sequences', label: 'Secuencias' },
  ];

  // ── NoSeries form state ─────────────────────────────────────────
  readonly showSeriesModal = signal(false);
  readonly seriesSaving    = signal(false);
  readonly seriesError     = signal('');
  editingCode: string | null = null;

  seriesForm: {
    code: string; description: string; defaultNos: boolean; manualNos: boolean;
    startingNo: string; endingNo: string; incrementByNo: number;
  } = { code: '', description: '', defaultNos: true, manualNos: false, startingNo: '', endingNo: '', incrementByNo: 1 };

  async ngOnInit(): Promise<void> {
    await this.noSeriesSvc.load();
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

  readonly paymentTerms = [
    { code: 'CONTADO',  desc: 'Pago al contado',   days: 0  },
    { code: '15 DIAS',  desc: 'Pago en 15 días',   days: 15 },
    { code: '30 DIAS',  desc: 'Pago en 30 días',   days: 30 },
    { code: '60 DIAS',  desc: 'Pago en 60 días',   days: 60 },
    { code: '90 DIAS',  desc: 'Pago en 90 días',   days: 90 },
    { code: 'NET 30',   desc: 'Net 30 (USD)',        days: 30 },
    { code: 'NET 45',   desc: 'Net 45 (USD)',        days: 45 },
  ];

  readonly paymentMethods = [
    { code: 'EFECTIVO',      desc: 'Efectivo' },
    { code: 'TRANSFERENCIA', desc: 'Transferencia bancaria' },
    { code: 'CHEQUE',        desc: 'Cheque' },
    { code: 'TARJETA',       desc: 'Tarjeta de crédito/débito' },
    { code: 'WIRE',          desc: 'Wire transfer (USD)' },
  ];

  readonly users = [
    { name: 'Administrador', email: 'admin@nexusbilling.do', role: 'Admin', lastAccess: 'Hoy, 9:45 AM', active: true },
    { name: 'María Ventas', email: 'mventas@empresa.com', role: 'Ventas', lastAccess: 'Ayer, 4:20 PM', active: true },
    { name: 'Carlos Almacén', email: 'calmacen@empresa.com', role: 'Inventario', lastAccess: 'Hace 3 días', active: true },
    { name: 'Ana Contable', email: 'acontable@empresa.com', role: 'Finanzas', lastAccess: 'Hace 1 semana', active: false },
  ];

  available(s: NcfSeries): number {
    return s.toNo - s.currentNo;
  }
}
