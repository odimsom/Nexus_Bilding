import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

interface NavSection {
  label: string;
  items: NavItem[];
}

interface NavItem {
  label: string;
  route: string;
  icon: string;       // Lucide icon name
  badge?: number;
}

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="nx-shell" [attr.data-theme]="theme()">

      <!-- ── SIDEBAR ──────────────────────────────────────────── -->
      <aside class="nx-sidebar" [class.is-collapsed]="sidebarCollapsed()">

        <!-- Brand -->
        <div class="nx-sidebar__brand">
          <svg class="nx-sidebar__logo" viewBox="0 0 28 28" fill="none">
            <rect width="28" height="28" rx="6" fill="#138A68"/>
            <path d="M8 20V8h4l4 8 4-8h4v12h-3v-7.5l-3.5 7.5h-3L11 12.5V20H8z" fill="white"/>
          </svg>
          <span class="nx-sidebar__name">NexusBilling</span>
        </div>

        <!-- Nav -->
        <nav class="nx-sidebar__nav">
          @for (section of navSections; track section.label) {
            <span class="nx-sidebar__section-label">{{ section.label }}</span>
            @for (item of section.items; track item.route) {
              <a
                [routerLink]="item.route"
                routerLinkActive="active"
                [routerLinkActiveOptions]="{ exact: item.route === '/dashboard' }"
                class="nx-navitem"
                [attr.title]="sidebarCollapsed() ? item.label : null"
              >
                <!-- Inline SVG icons via data attribute for Lucide compatibility -->
                <svg class="nx-navitem__icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.75" stroke-linecap="round" stroke-linejoin="round">
                  <use [attr.href]="'#icon-' + item.icon"></use>
                </svg>
                <span class="nx-navitem__label">{{ item.label }}</span>
                @if (item.badge) {
                  <span class="nx-navitem__badge">{{ item.badge }}</span>
                }
              </a>
            }
          }
        </nav>

        <!-- Footer -->
        <div class="nx-sidebar__foot">
          <button
            class="nx-navitem"
            style="width:100%; background:none; border:none; cursor:pointer; color: rgba(255,255,255,0.5);"
            (click)="logout()"
          >
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.75" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;flex:none;">
              <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/>
              <polyline points="16 17 21 12 16 7"/>
              <line x1="21" y1="12" x2="9" y2="12"/>
            </svg>
            <span class="nx-navitem__label">Cerrar sesión</span>
          </button>
        </div>
      </aside>

      <!-- ── MAIN ──────────────────────────────────────────────── -->
      <main class="nx-main">

        <!-- Top bar -->
        <header class="nx-topbar">
          <!-- Collapse toggle -->
          <button
            class="nx-iconbtn nx-iconbtn--sm"
            (click)="toggleSidebar()"
            [attr.aria-label]="sidebarCollapsed() ? 'Expandir menú' : 'Colapsar menú'"
            style="color: var(--nx-text-muted);"
          >
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:18px;height:18px;">
              <line x1="3" y1="6" x2="21" y2="6"/>
              <line x1="3" y1="12" x2="21" y2="12"/>
              <line x1="3" y1="18" x2="21" y2="18"/>
            </svg>
          </button>

          <!-- Search -->
          <div class="nx-topbar__search">
            <div class="nx-inputgroup">
              <span class="nx-adorn">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/>
                </svg>
              </span>
              <input
                id="topbar-search"
                type="search"
                class="nx-input"
                placeholder="Buscar clientes, facturas, artículos…"
                style="padding-left: 36px;"
              />
            </div>
          </div>

          <div class="nx-topbar__actions">
            <!-- Theme toggle -->
            <button
              class="nx-iconbtn"
              (click)="toggleTheme()"
              [attr.aria-label]="theme() === 'dark' ? 'Modo claro' : 'Modo oscuro'"
            >
              @if (theme() === 'dark') {
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:17px;height:17px;">
                  <circle cx="12" cy="12" r="4"/><path d="M12 2v2M12 20v2M4.93 4.93l1.41 1.41M17.66 17.66l1.41 1.41M2 12h2M20 12h2M6.34 17.66l-1.41 1.41M19.07 4.93l-1.41 1.41"/>
                </svg>
              } @else {
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:17px;height:17px;">
                  <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z"/>
                </svg>
              }
            </button>

            <!-- Notifications -->
            <button class="nx-iconbtn" aria-label="Notificaciones" style="position:relative;">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:17px;height:17px;">
                <path d="M6 8a6 6 0 0 1 12 0c0 7 3 9 3 9H3s3-2 3-9"/><path d="M10.3 21a1.94 1.94 0 0 0 3.4 0"/>
              </svg>
            </button>

            <!-- User avatar -->
            @if (user()) {
              <div class="nx-avatar nx-avatar--circle" style="cursor:default;" [attr.title]="user()?.username">
                {{ user()?.username?.charAt(0)?.toUpperCase() }}
              </div>
            }
          </div>
        </header>

        <!-- Content -->
        <section class="nx-content">
          <router-outlet></router-outlet>
        </section>

      </main>
    </div>
  `,
  styles: [`
    /* Shell reset */
    :host { display: contents; }

    /* Ensure full viewport fill */
    .nx-shell { height: 100vh; width: 100vw; overflow: hidden; }

    /* Nav icon sizing (inline SVGs) */
    .nx-navitem__icon { width: 18px; height: 18px; flex: none; opacity: 0.7; }
    .nx-navitem:hover .nx-navitem__icon,
    .nx-navitem.active .nx-navitem__icon { opacity: 1; }

    /* Search input width */
    .nx-topbar__search { flex: 0 1 360px; }

    /* Hide section labels when collapsed */
    .nx-sidebar.is-collapsed .nx-sidebar__section-label,
    .nx-sidebar.is-collapsed .nx-navitem__label,
    .nx-sidebar.is-collapsed .nx-navitem__badge,
    .nx-sidebar.is-collapsed .nx-sidebar__name { display: none; }
  `]
})
export class DashboardLayoutComponent {
  private readonly authService = inject(AuthService);

  readonly user = this.authService.user;
  readonly sidebarCollapsed = signal(false);
  readonly theme = signal<'light' | 'dark'>('light');

  readonly navSections: NavSection[] = [
    {
      label: 'Principal',
      items: [
        { label: 'Dashboard',   route: '/dashboard', icon: 'layout-dashboard' },
      ]
    },
    {
      label: 'Ventas',
      items: [
        { label: 'Clientes',    route: '/customers', icon: 'users' },
        { label: 'Órdenes',     route: '/sales',     icon: 'file-text' },
        { label: 'Facturas',    route: '/invoices',  icon: 'receipt', badge: 3 },
      ]
    },
    {
      label: 'Inventario',
      items: [
        { label: 'Artículos',   route: '/inventory', icon: 'package' },
        { label: 'Ubicaciones', route: '/locations', icon: 'map-pin' },
      ]
    },
    {
      label: 'Compras',
      items: [
        { label: 'Proveedores', route: '/vendors',   icon: 'truck' },
        { label: 'Órdenes',     route: '/purchases', icon: 'shopping-cart' },
      ]
    },
    {
      label: 'Finanzas',
      items: [
        { label: 'Mayor General', route: '/gl',       icon: 'bar-chart-2' },
        { label: 'Diario',        route: '/journal',  icon: 'book-open' },
      ]
    },
    {
      label: 'Administración',
      items: [
        { label: 'Configuración', route: '/settings', icon: 'settings' },
      ]
    }
  ];

  toggleSidebar(): void {
    this.sidebarCollapsed.update(v => !v);
  }

  toggleTheme(): void {
    this.theme.update(t => t === 'dark' ? 'light' : 'dark');
    document.documentElement.setAttribute(
      'data-theme',
      this.theme() === 'dark' ? 'dark' : ''
    );
  }

  logout(): void {
    this.authService.logout();
  }
}
