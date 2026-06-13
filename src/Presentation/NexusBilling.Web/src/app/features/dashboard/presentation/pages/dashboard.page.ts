import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ChartData, ChartOptions } from 'chart.js';
import { DashboardService } from '../../data/dashboard.service';
import { NxChartComponent } from '../../../../shared/components/chart/nx-chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, NxChartComponent],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-6);">
      <span class="nx-crumb--current">Dashboard</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-6);">
      <div>
        <h1 class="nx-page-title">Panel Principal</h1>
        <p class="nx-page-subtitle">Resumen ejecutivo en tiempo real</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="reload()" [disabled]="svc.loading()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:15px;height:15px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
          Actualizar
        </button>
        <a routerLink="/sales" class="nx-btn nx-btn--primary nx-btn--sm">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:15px;height:15px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nueva Venta
        </a>
      </div>
    </div>

    @if (svc.loading()) {
      <div class="nx-empty"><div class="nx-spinner"></div></div>
    } @else if (!svc.stats()) {
      <div class="nx-empty">
        <p class="nx-empty__title">Sin datos disponibles</p>
        <p class="nx-empty__text">Verifica la conexión con el servidor.</p>
      </div>
    } @else {

      <!-- KPI Row -->
      <div class="kpi-grid" style="margin-bottom:var(--nx-space-5);">
        <a routerLink="/customers" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Clientes</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.totalCustomers }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Total registrados</span>
        </a>
        <a routerLink="/vendors" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Proveedores</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.totalVendors }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Total registrados</span>
        </a>
        <a routerLink="/inventory" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Productos</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.totalItems }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">En catálogo</span>
        </a>
        <a routerLink="/sales" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Ventas Abiertas</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.openOrders }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Pendientes</span>
        </a>
        <a routerLink="/purchasing" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Compras Abiertas</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.openPurchaseOrders }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Pendientes</span>
        </a>
        <div class="nx-stat">
          <span class="nx-stat__label">Ventas Este Mes</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">
            {{ svc.stats()!.totalSalesThisMonth | currency:'DOP':'symbol':'1.2-2' }}
          </span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Incluye ITBIS</span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Compras Este Mes</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">
            {{ svc.stats()!.totalPurchasesThisMonth | currency:'DOP':'symbol':'1.2-2' }}
          </span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Incluye ITBIS</span>
        </div>
      </div>

      <!-- Main layout -->
      <div style="display:grid; grid-template-columns:1fr 260px; gap:var(--nx-space-4); align-items:start;">

        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">
          <!-- Charts Row -->
          <div style="display:grid; grid-template-columns:2fr 1fr; gap:var(--nx-space-4);">
            <!-- Chart: ventas por mes -->
            <div class="nx-card">
            <div class="nx-card__head">
              <div>
                <div class="nx-card__title">Ventas por Mes</div>
                <div class="nx-card__subtitle">Últimos 12 meses · incluye ITBIS</div>
              </div>
            </div>
            <div style="padding:var(--nx-space-4) var(--nx-space-4) var(--nx-space-2);height:224px;">
              @if (salesChartData().labels!.length > 0) {
                <nx-chart type="bar" [data]="salesChartData()" [options]="salesChartOptions" style="height:200px;display:block;" />
              } @else {
                <div class="nx-empty" style="height:160px;">
                  <p class="nx-empty__text">Sin ventas en los últimos 12 meses</p>
                </div>
              }
            </div>
          </div>

          <!-- Chart: Estado de órdenes -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div>
                <div class="nx-card__title">Estado Órdenes</div>
                <div class="nx-card__subtitle">Distribución activa</div>
              </div>
            </div>
            <div style="padding:var(--nx-space-4);height:224px;display:flex;justify-content:center;">
              <nx-chart type="doughnut" [data]="statusChartData()" [options]="statusChartOptions" style="height:200px;width:200px;display:block;" />
            </div>
          </div>
        </div>

          <!-- Órdenes recientes -->
          <div class="nx-card">
            <div class="nx-card__head">
              <div>
                <div class="nx-card__title">Órdenes Recientes</div>
                <div class="nx-card__subtitle">Últimas {{ svc.stats()!.recentOrders.length }} transacciones</div>
              </div>
              <a routerLink="/sales" class="nx-btn nx-btn--ghost nx-btn--sm">Ver todas</a>
            </div>
            @if (svc.stats()!.recentOrders.length === 0) {
              <div class="nx-empty" style="padding:var(--nx-space-6);">
                <p class="nx-empty__text">Aún no hay órdenes.</p>
              </div>
            } @else {
              <div style="overflow:auto;">
                <table class="nx-table">
                  <thead>
                    <tr>
                      <th>No.</th><th>Cliente</th><th>Fecha</th><th>Estado</th>
                      <th class="nx-th--num">Total</th>
                    </tr>
                  </thead>
                  <tbody>
                    @for (o of svc.stats()!.recentOrders; track o.no) {
                      <tr style="cursor:pointer;" [routerLink]="['/sales', o.no]">
                        <td class="nx-td--doc">{{ o.no }}</td>
                        <td style="font-weight:var(--nx-weight-medium);max-width:160px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ o.customerName }}</td>
                        <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ o.postingDate }}</td>
                        <td>
                          <span class="nx-badge" [class]="statusClass(o.status)">
                            <span class="nx-badge__dot"></span>{{ statusLabel(o.status) }}
                          </span>
                        </td>
                        <td class="nx-td--num nx-num" style="font-weight:var(--nx-weight-semibold);">
                          {{ o.amountIncludingVat | number:'1.2-2' }}
                        </td>
                      </tr>
                    }
                  </tbody>
                </table>
              </div>
            }
          </div>
        </div>

        <!-- Quick nav sidebar -->
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-3);">
          <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);text-transform:uppercase;letter-spacing:.05em;font-weight:var(--nx-weight-semibold);">Acceso rápido</div>
          @for (link of quickLinks; track link.route) {
            <a [routerLink]="link.route" class="nx-card" style="display:flex;align-items:center;gap:var(--nx-space-3);text-decoration:none;padding:var(--nx-space-3);">
              <div [style.background]="link.bg" [style.color]="link.color" style="width:36px;height:36px;border-radius:var(--nx-radius-md);display:flex;align-items:center;justify-content:center;flex:none;">
                @switch (link.icon) {
                  @case ('users') { <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M22 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg> }
                  @case ('package') { <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><line x1="16.5" y1="9.4" x2="7.5" y2="4.21"/><path d="M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z"/><polyline points="3.27 6.96 12 12.01 20.73 6.96"/><line x1="12" y1="22.08" x2="12" y2="12"/></svg> }
                  @case ('file-text') { <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/><line x1="16" y1="13" x2="8" y2="13"/><line x1="16" y1="17" x2="8" y2="17"/><polyline points="10 9 9 9 8 9"/></svg> }
                  @case ('receipt') { <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><path d="M4 2v20l2-1 2 1 2-1 2 1 2-1 2 1 2-1 2 1V2l-2 1-2-1-2 1-2-1-2 1-2-1-2 1Z"/><path d="M16 14h-8"/><path d="M16 10h-8"/></svg> }
                  @case ('settings') { <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><circle cx="12" cy="12" r="3"/><path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1 0 2.83 2 2 0 0 1-2.83 0l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-2 2 2 2 0 0 1-2-2v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83 0 2 2 0 0 1 0-2.83l.06-.06a1.65 1.65 0 0 0 .33-1.82 1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1-2-2 2 2 0 0 1 2-2h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 0-2.83 2 2 0 0 1 2.83 0l.06.06a1.65 1.65 0 0 0 1.82.33H9a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 2-2 2 2 0 0 1 2 2v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 0 2 2 0 0 1 0 2.83l-.06.06a1.65 1.65 0 0 0-.33 1.82V9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 2 2 2 2 0 0 1-2 2h-.09a1.65 1.65 0 0 0-1.51 1z"/></svg> }
                }
              </div>
              <div>
                <div style="font-weight:var(--nx-weight-medium);font-size:var(--nx-text-sm);">{{ link.label }}</div>
                <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">{{ link.desc }}</div>
              </div>
            </a>
          }
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .kpi-grid { display:grid; grid-template-columns:repeat(auto-fit,minmax(160px,1fr)); gap:var(--nx-space-3); }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    a.nx-stat:hover, a.nx-card:hover { box-shadow: 0 0 0 2px var(--nx-action); }
  `]
})
export class DashboardPage implements OnInit {
  readonly svc = inject(DashboardService);

  readonly quickLinks = [
    { route: '/customers',         label: 'Clientes',           desc: 'Gestiona tu cartera',     icon: 'users',     bg: 'var(--nx-blue-50)',    color: 'var(--nx-blue-600)'   },
    { route: '/inventory',         label: 'Inventario',         desc: 'Productos y existencias', icon: 'package',   bg: 'var(--nx-green-50)',   color: 'var(--nx-green-600)'  },
    { route: '/sales',             label: 'Órdenes de Venta',   desc: 'Pedidos de venta',        icon: 'file-text', bg: 'var(--nx-amber-50)',   color: 'var(--nx-amber-600)'  },
    { route: '/invoices',          label: 'Facturas de Venta',  desc: 'Facturas emitidas',       icon: 'receipt',   bg: 'var(--nx-indigo-50)',  color: 'var(--nx-indigo-600)' },
    { route: '/purchases',         label: 'Órdenes de Compra',  desc: 'Pedidos a proveedores',   icon: 'file-text', bg: 'var(--nx-orange-50)',  color: 'var(--nx-orange-600)' },
    { route: '/purchase-invoices', label: 'Facturas de Compra', desc: 'Facturas recibidas',      icon: 'receipt',   bg: 'var(--nx-teal-50)',    color: 'var(--nx-teal-600)'   },
    { route: '/settings',          label: 'Config.',            desc: 'Empresa y secuencias',    icon: 'settings',  bg: 'var(--nx-canvas-alt)', color: 'var(--nx-text-muted)' },
  ];

  readonly salesChartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: { display: false },
      tooltip: {
        callbacks: {
          label: (ctx) => ` DOP ${(ctx.raw as number).toLocaleString('es-DO', { minimumFractionDigits: 2 })}`
        }
      }
    },
    scales: {
      x: { grid: { display: false }, ticks: { font: { size: 11 } } },
      y: {
        grid: { color: 'rgba(0,0,0,0.05)' },
        ticks: {
          font: { size: 10 },
          callback: (v) => `${(Number(v) / 1000).toFixed(0)}k`
        }
      }
    }
  };

  readonly salesChartData = computed<ChartData>(() => {
    const stats = this.svc.stats();
    if (!stats?.monthlySales?.length) return { labels: [], datasets: [] };
    const monthNames: Record<string, string> = {
      '01':'Ene','02':'Feb','03':'Mar','04':'Abr','05':'May','06':'Jun',
      '07':'Jul','08':'Ago','09':'Sep','10':'Oct','11':'Nov','12':'Dic'
    };
    return {
      labels: stats.monthlySales.map(m => {
        const [, mo] = m.month.split('-');
        return monthNames[mo] ?? mo;
      }),
      datasets: [{
        data: stats.monthlySales.map(m => m.total),
        backgroundColor: 'rgba(12, 113, 86, 0.75)',
        borderColor: '#0C7156',
        borderWidth: 1,
        borderRadius: 4,
        hoverBackgroundColor: '#0C7156'
      }]
    };
  });

  readonly statusChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    cutout: '65%',
    plugins: {
      legend: { position: 'bottom', labels: { usePointStyle: true, padding: 20, font: { size: 11 } } },
      tooltip: {
        callbacks: {
          label: (ctx: any) => ` ${ctx.label}: ${ctx.raw} órdenes`
        }
      }
    }
  };

  readonly statusChartData = computed<ChartData>(() => {
    const stats = this.svc.stats();
    if (!stats?.recentOrders) return { labels: [], datasets: [] };
    
    let open = 0, released = 0, closed = 0;
    stats.recentOrders.forEach(o => {
      if (o.status === 'Open') open++;
      else if (o.status === 'Released') released++;
      else closed++;
    });

    open = stats.openOrders > open ? stats.openOrders : open;

    return {
      labels: ['Abiertas', 'Liberadas', 'Cerradas'],
      datasets: [{
        data: [open, released, closed],
        backgroundColor: ['#3B82F6', '#10B981', '#94A3B8'],
        borderWidth: 0,
        hoverOffset: 4
      }]
    };
  });

  async ngOnInit(): Promise<void> { await this.svc.load(); }
  async reload(): Promise<void> { await this.svc.load(); }

  statusClass(s: string): string {
    return { Open:'nx-badge--info', Released:'nx-badge--success', Closed:'nx-badge--outline' }[s] ?? 'nx-badge--outline';
  }
  statusLabel(s: string): string {
    return { Open:'Abierta', Released:'Liberada', Closed:'Cerrada' }[s] ?? s;
  }
}
