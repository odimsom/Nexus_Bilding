import { Component, inject, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../data/dashboard.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-6);">
      <span class="nx-crumb--current">Dashboard</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-6);">
      <div>
        <h1 class="nx-page-title">Panel Principal</h1>
        <p class="nx-page-subtitle">Resumen ejecutivo — datos en tiempo real</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="reload()" [disabled]="svc.loading()">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:15px;height:15px;"><polyline points="23 4 23 10 17 10"/><path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"/></svg>
          Actualizar
        </button>
        <a routerLink="/customers" class="nx-btn nx-btn--primary nx-btn--sm">
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
      <div class="kpi-grid" style="display:grid; grid-template-columns:repeat(auto-fit,minmax(200px,1fr)); gap:var(--nx-space-4); margin-bottom:var(--nx-space-6);">

        <a routerLink="/customers" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Clientes</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.totalCustomers }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Total registrados</span>
        </a>

        <a routerLink="/inventory" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Artículos</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.totalItems }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">En catálogo</span>
        </a>

        <a routerLink="/sales/orders" class="nx-stat" style="text-decoration:none;cursor:pointer;">
          <span class="nx-stat__label">Órdenes Abiertas</span>
          <span class="nx-stat__value nx-num">{{ svc.stats()!.openOrders }}</span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Pendientes de procesar</span>
        </a>

        <div class="nx-stat">
          <span class="nx-stat__label">Ventas Este Mes</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">
            {{ svc.stats()!.totalSalesThisMonth | currency:'DOP':'symbol':'1.2-2' }}
          </span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Incluye ITBIS</span>
        </div>

        <div class="nx-stat">
          <span class="nx-stat__label">Ventas Totales</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-xl);">
            {{ svc.stats()!.totalSalesAllTime | currency:'DOP':'symbol':'1.2-2' }}
          </span>
          <span class="nx-eyebrow" style="margin-top:var(--nx-space-1);">Histórico acumulado</span>
        </div>
      </div>

      <!-- Quick access + Recent orders -->
      <div style="display:grid; grid-template-columns:1fr 300px; gap:var(--nx-space-4); align-items:start; flex-wrap:wrap;">

        <!-- Recent orders -->
        <div class="nx-card">
          <div class="nx-card__head">
            <div>
              <div class="nx-card__title">Órdenes Recientes</div>
              <div class="nx-card__subtitle">Últimas {{ svc.stats()!.recentOrders.length }} transacciones</div>
            </div>
            <a routerLink="/sales/orders" class="nx-btn nx-btn--ghost nx-btn--sm">Ver todas</a>
          </div>
          @if (svc.stats()!.recentOrders.length === 0) {
            <div class="nx-empty" style="padding:var(--nx-space-6);">
              <p class="nx-empty__text">Aún no hay órdenes. Crea una desde la ficha de un cliente.</p>
            </div>
          } @else {
            <div style="overflow:auto;">
              <table class="nx-table">
                <thead>
                  <tr>
                    <th>No.</th>
                    <th>Cliente</th>
                    <th>Fecha</th>
                    <th>Estado</th>
                    <th class="nx-th--num">Total</th>
                  </tr>
                </thead>
                <tbody>
                  @for (o of svc.stats()!.recentOrders; track o.no) {
                    <tr>
                      <td class="nx-td--doc">{{ o.no }}</td>
                      <td style="font-weight:var(--nx-weight-medium);max-width:180px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;">{{ o.customerName }}</td>
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

        <!-- Quick nav -->
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-3);">
          <div class="nx-card__title" style="margin-bottom:var(--nx-space-1);font-size:var(--nx-text-sm);color:var(--nx-text-muted);text-transform:uppercase;letter-spacing:.05em;">Acceso rápido</div>

          <a routerLink="/customers" class="nx-card" style="display:flex;align-items:center;gap:var(--nx-space-3);text-decoration:none;padding:var(--nx-space-3);">
            <div style="width:36px;height:36px;border-radius:var(--nx-radius-md);background:var(--nx-blue-50);display:flex;align-items:center;justify-content:center;color:var(--nx-blue-600);">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>
            </div>
            <div>
              <div style="font-weight:var(--nx-weight-medium);font-size:var(--nx-text-sm);">Clientes</div>
              <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Gestiona tu cartera</div>
            </div>
          </a>

          <a routerLink="/inventory" class="nx-card" style="display:flex;align-items:center;gap:var(--nx-space-3);text-decoration:none;padding:var(--nx-space-3);">
            <div style="width:36px;height:36px;border-radius:var(--nx-radius-md);background:var(--nx-green-50);display:flex;align-items:center;justify-content:center;color:var(--nx-green-600);">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><rect x="2" y="3" width="20" height="14" rx="2"/><line x1="8" y1="21" x2="16" y2="21"/><line x1="12" y1="17" x2="12" y2="21"/></svg>
            </div>
            <div>
              <div style="font-weight:var(--nx-weight-medium);font-size:var(--nx-text-sm);">Inventario</div>
              <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Artículos y existencias</div>
            </div>
          </a>

          <a routerLink="/sales/orders" class="nx-card" style="display:flex;align-items:center;gap:var(--nx-space-3);text-decoration:none;padding:var(--nx-space-3);">
            <div style="width:36px;height:36px;border-radius:var(--nx-radius-md);background:var(--nx-amber-50);display:flex;align-items:center;justify-content:center;color:var(--nx-amber-600);">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/><polyline points="14 2 14 8 20 8"/><line x1="16" y1="13" x2="8" y2="13"/><line x1="16" y1="17" x2="8" y2="17"/><polyline points="10 9 9 9 8 9"/></svg>
            </div>
            <div>
              <div style="font-weight:var(--nx-weight-medium);font-size:var(--nx-text-sm);">Órdenes de Venta</div>
              <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Pedidos y cotizaciones</div>
            </div>
          </a>

          <a routerLink="/settings" class="nx-card" style="display:flex;align-items:center;gap:var(--nx-space-3);text-decoration:none;padding:var(--nx-space-3);">
            <div style="width:36px;height:36px;border-radius:var(--nx-radius-md);background:var(--nx-canvas-alt);display:flex;align-items:center;justify-content:center;color:var(--nx-text-muted);">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;"><circle cx="12" cy="12" r="3"/><path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1-2.83 2.83l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-4 0v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83-2.83l.06-.06A1.65 1.65 0 0 0 4.68 15a1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1 0-4h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 2.83-2.83l.06.06A1.65 1.65 0 0 0 9 4.68a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 4 0v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 2.83l-.06.06A1.65 1.65 0 0 0 19.4 9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 0 4h-.09a1.65 1.65 0 0 0-1.51 1z"/></svg>
            </div>
            <div>
              <div style="font-weight:var(--nx-weight-medium);font-size:var(--nx-text-sm);">Configuración</div>
              <div style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);">Empresa y secuencias</div>
            </div>
          </a>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:4rem auto; }
    @keyframes spin { to { transform:rotate(360deg); } }
    .nx-stat { transition: box-shadow 0.15s; }
    .nx-stat:hover { box-shadow: 0 0 0 2px var(--nx-action); }
    a.nx-card:hover { box-shadow: 0 0 0 2px var(--nx-action); }
  `]
})
export class DashboardPage implements OnInit {
  readonly svc = inject(DashboardService);

  async ngOnInit(): Promise<void> { await this.svc.load(); }
  async reload(): Promise<void> { await this.svc.load(); }

  statusClass(s: string): string {
    return { Open: 'nx-badge--info', Released: 'nx-badge--success', Closed: 'nx-badge--outline' }[s] ?? 'nx-badge--outline';
  }

  statusLabel(s: string): string {
    return { Open: 'Abierta', Released: 'Liberada', Closed: 'Cerrada' }[s] ?? s;
  }
}
