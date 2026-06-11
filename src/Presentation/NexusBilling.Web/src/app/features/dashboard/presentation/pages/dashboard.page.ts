import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

interface KpiCard {
  label: string;
  value: string;
  delta: string;
  deltaDir: 'up' | 'down' | 'flat';
  sub: string;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <!-- Breadcrumb -->
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-6);">
      <span class="nx-crumb--current">Dashboard</span>
    </nav>

    <!-- Page header -->
    <div class="nx-page-header" style="margin-bottom:var(--nx-space-6);">
      <div>
        <h1 class="nx-page-title">Panel Principal</h1>
        <p class="nx-page-subtitle">Resumen ejecutivo del sistema</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:15px;height:15px;">
            <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/><polyline points="7 10 12 15 17 10"/><line x1="12" y1="15" x2="12" y2="3"/>
          </svg>
          Exportar
        </button>
        <button class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:15px;height:15px;">
            <line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/>
          </svg>
          Nueva factura
        </button>
      </div>
    </div>

    <!-- KPI Row -->
    <div class="kpi-grid" style="display:grid; grid-template-columns:repeat(auto-fit,minmax(200px,1fr)); gap:var(--nx-space-4); margin-bottom:var(--nx-space-6);">
      @for (kpi of kpis; track kpi.label) {
        <div class="nx-stat">
          <span class="nx-stat__label">{{ kpi.label }}</span>
          <span class="nx-stat__value nx-num">{{ kpi.value }}</span>
          <span class="nx-stat__delta" [class]="'nx-stat__delta--' + kpi.deltaDir">
            @if (kpi.deltaDir === 'up') {
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="18 15 12 9 6 15"/></svg>
            } @else if (kpi.deltaDir === 'down') {
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><polyline points="6 9 12 15 18 9"/></svg>
            }
            {{ kpi.delta }}
          </span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">{{ kpi.sub }}</span>
        </div>
      }
    </div>

    <!-- Content row -->
    <div style="display:grid; grid-template-columns:1fr 340px; gap:var(--nx-space-4); align-items:start;">

      <!-- Recent Invoices -->
      <div class="nx-card">
        <div class="nx-card__head">
          <div>
            <div class="nx-card__title">Facturas Recientes</div>
            <div class="nx-card__subtitle">Últimas transacciones de venta</div>
          </div>
          <div class="nx-card__actions">
            <button class="nx-btn nx-btn--ghost nx-btn--sm">Ver todas</button>
          </div>
        </div>
        <div style="overflow:auto;">
          <table class="nx-table">
            <thead>
              <tr>
                <th>No. Documento</th>
                <th>Cliente</th>
                <th>Fecha</th>
                <th>Estado</th>
                <th class="nx-th--num">Importe</th>
              </tr>
            </thead>
            <tbody>
              @for (inv of invoices; track inv.no) {
                <tr>
                  <td class="nx-td--doc">{{ inv.no }}</td>
                  <td>{{ inv.customer }}</td>
                  <td style="color:var(--nx-text-muted); font-size:var(--nx-text-sm);">{{ inv.date }}</td>
                  <td>
                    <span class="nx-badge" [class]="'nx-badge--' + inv.statusClass">
                      <span class="nx-badge__dot"></span>
                      {{ inv.status }}
                    </span>
                  </td>
                  <td class="nx-td--num">
                    <span class="nx-amount" [class]="inv.amount < 0 ? 'nx-amount--negative' : ''">
                      RD$ {{ inv.amount | number:'1.2-2' }}
                    </span>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      </div>

      <!-- Quick stats sidebar card -->
      <div class="nx-card">
        <div class="nx-card__head">
          <div class="nx-card__title">Actividades</div>
        </div>
        <div class="nx-card__body" style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
          @for (act of activities; track act.label) {
            <div style="display:flex;align-items:center;gap:var(--nx-space-3);">
              <div class="nx-avatar nx-avatar--sm" [style.background]="act.color" style="color:#fff;">
                {{ act.initials }}
              </div>
              <div style="flex:1;min-width:0;">
                <div style="font-size:var(--nx-text-sm);font-weight:var(--nx-weight-medium);color:var(--nx-text-strong);white-space:nowrap;overflow:hidden;text-overflow:ellipsis;">{{ act.label }}</div>
                <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ act.time }}</div>
              </div>
              <span class="nx-badge" [class]="'nx-badge--' + act.badgeClass">{{ act.badge }}</span>
            </div>
          }
        </div>
      </div>

    </div>
  `
})
export class DashboardPage {
  readonly kpis: KpiCard[] = [
    { label: 'Ventas del Mes',        value: 'RD$4,218,500',   delta: '+12.4%', deltaDir: 'up',   sub: 'vs mes anterior' },
    { label: 'Facturas Abiertas',     value: '38',              delta: '+5',     deltaDir: 'down', sub: 'pendientes de cobro' },
    { label: 'Clientes Activos',      value: '1,204',           delta: '+23',    deltaDir: 'up',   sub: 'este mes' },
    { label: 'Artículos en Inventario', value: '8,742',         delta: '0',      deltaDir: 'flat', sub: 'unidades totales' },
  ];

  readonly invoices = [
    { no: 'INV-2025-0041', customer: 'Altagracia Comercial S.R.L.', date: '11 Jun 2025', status: 'Publicada',  statusClass: 'success', amount: 148500   },
    { no: 'INV-2025-0040', customer: 'Distribuidora Los Alcarrizos', date: '10 Jun 2025', status: 'Abierta',   statusClass: 'info',    amount: 75200    },
    { no: 'INV-2025-0039', customer: 'Ferretería El Progreso',       date: '09 Jun 2025', status: 'Vencida',   statusClass: 'danger',  amount: 22800    },
    { no: 'INV-2025-0038', customer: 'Supermercado Bravo',           date: '08 Jun 2025', status: 'Pendiente', statusClass: 'warn',    amount: 310750   },
    { no: 'INV-2025-0037', customer: 'Grupo Estrella',               date: '07 Jun 2025', status: 'Publicada', statusClass: 'success', amount: 890000   },
  ];

  readonly activities = [
    { initials: 'AC', label: 'Altagracia Comercial — Cobro', time: 'Hace 2 min',  badge: 'Pagada',   badgeClass: 'success', color: '#138A68' },
    { initials: 'DL', label: 'Distribuidora Los Alcarrizos',  time: 'Hace 18 min', badge: 'Abierta',  badgeClass: 'info',    color: '#2C63D6' },
    { initials: 'FP', label: 'Ferretería El Progreso',         time: 'Hace 1 h',   badge: 'Vencida',  badgeClass: 'danger',  color: '#CE3F35' },
    { initials: 'SB', label: 'Supermercado Bravo — Orden',    time: 'Hace 3 h',   badge: 'Pendiente',badgeClass: 'warn',    color: '#CF9412' },
  ];
}
