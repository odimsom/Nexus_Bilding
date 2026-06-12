import { Component, inject, signal, computed, effect } from '@angular/core';
import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter, map, startWith } from 'rxjs/operators';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

interface NavItem { label: string; route: string; icon: string; badge?: number; }
interface NavSection { group?: string; items: NavItem[]; }
interface PaletteItem { id: string; label: string; desc: string; icon: string; route: string; }

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  template: `
    <div class="erp">

      <!-- ── SIDEBAR ─────────────────────────────────────────────── -->
      <nav class="erp-side">

        <!-- Brand -->
        <div class="erp-brand" (click)="navigate('/dashboard')" style="cursor:pointer;">
          <img src="/assets/logo-mark.svg" width="26" height="26" alt="Nexus Billing" />
          <b>Nexus<span class="l"> Billing</span></b>
        </div>

        <!-- Search trigger -->
        <div style="padding:8px 10px;">
          <button class="palette-btn" (click)="openPalette()">
            <i data-lucide="search" style="width:14px;height:14px;"></i>
            <span>Buscar…</span>
            <kbd>⌘K</kbd>
          </button>
        </div>

        <!-- Nav -->
        <div class="erp-nav">
          @for (sec of navSections; track $index) {
            @if (sec.group) {
              <div class="erp-navgroup">{{ sec.group }}</div>
            }
            @for (item of sec.items; track item.route) {
              <button
                class="erp-navitem"
                [class.active]="isActive(item.route)"
                (click)="navigate(item.route)"
              >
                <i [attr.data-lucide]="item.icon" style="width:16px;height:16px;flex:none;"></i>
                <span>{{ item.label }}</span>
                @if (item.badge) {
                  <span class="count">{{ item.badge }}</span>
                }
              </button>
            }
          }
        </div>

        <!-- User footer -->
        <div class="erp-side__foot">
          <div class="nx-avatar nx-avatar--circle nx-avatar--sm" style="flex:none;">
            {{ user()?.username?.charAt(0)?.toUpperCase() ?? '?' }}
          </div>
          <div style="flex:1;min-width:0;">
            <div class="nm">{{ user()?.username }}</div>
            <div class="rl">Administrador</div>
          </div>
          <button class="nx-iconbtn nx-iconbtn--sm" (click)="logout()" title="Cerrar sesión">
            <i data-lucide="log-out" style="width:15px;height:15px;"></i>
          </button>
        </div>
      </nav>

      <!-- ── MAIN ────────────────────────────────────────────────── -->
      <div class="erp-main">

        <!-- Topbar -->
        <header class="erp-top">
          <nav class="nx-crumbs" style="flex:1;min-width:0;">
            <span class="nx-crumb" style="cursor:pointer;" (click)="navigate('/dashboard')">Inicio</span>
            @if (pageTitle() !== 'Dashboard') {
              <span class="nx-crumbs__sep">›</span>
              <span class="nx-crumb--current">{{ pageTitle() }}</span>
            }
          </nav>
          <div class="erp-top__right">
            <button
              class="nx-iconbtn"
              (click)="toggleTheme()"
              [attr.aria-label]="theme() === 'dark' ? 'Modo claro' : 'Modo oscuro'"
            >
              <i [attr.data-lucide]="theme() === 'dark' ? 'sun' : 'moon'" style="width:17px;height:17px;"></i>
            </button>
            <button class="nx-iconbtn" aria-label="Notificaciones">
              <i data-lucide="bell" style="width:17px;height:17px;"></i>
            </button>
            <button class="nx-iconbtn" aria-label="Ayuda">
              <i data-lucide="circle-help" style="width:17px;height:17px;"></i>
            </button>
          </div>
        </header>

        <!-- Command bar -->
        <div class="erp-cmd">
          <span class="erp-cmd__title">{{ pageTitle() }}</span>
          <div class="erp-cmd__actions"></div>
        </div>

        <!-- Content -->
        <div class="erp-body">
          <div class="erp-body__inner">
            <router-outlet></router-outlet>
          </div>
        </div>
      </div>

      <!-- ── COMMAND PALETTE ─────────────────────────────────────── -->
      @if (paletteOpen()) {
        <div class="cmd-scrim" (click)="closePalette()">
          <div class="cmd-palette" (click)="$event.stopPropagation()">
            <div class="cmd-search">
              <i data-lucide="search" style="width:16px;height:16px;color:var(--slate-400);flex:none;"></i>
              <input
                type="text"
                placeholder="Buscar vistas, clientes, facturas…"
                [(ngModel)]="paletteQuery"
                (keydown)="onPaletteKey($event)"
                autofocus
              />
            </div>
            <div class="cmd-list">
              @for (item of filteredPaletteItems(); track item.id) {
                <button class="cmd-item" (click)="selectPaletteItem(item)">
                  <i [attr.data-lucide]="item.icon" style="width:16px;height:16px;color:var(--text-muted);flex:none;"></i>
                  <div>
                    <div class="cmd-item__label">{{ item.label }}</div>
                    <div class="cmd-item__desc">{{ item.desc }}</div>
                  </div>
                </button>
              }
              @if (filteredPaletteItems().length === 0) {
                <div class="cmd-empty">Sin resultados para "{{ paletteQuery }}"</div>
              }
            </div>
          </div>
        </div>
      }
    </div>
  `,
  styles: [`:host { display: contents; }`]
})
export class DashboardLayoutComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly user = this.authService.user;
  readonly paletteOpen = signal(false);
  paletteQuery = '';

  readonly theme = signal<'light' | 'dark'>(
    localStorage.getItem('nx-theme') === 'dark' ? 'dark' : 'light'
  );

  readonly currentUrl = toSignal(
    this.router.events.pipe(
      filter(e => e instanceof NavigationEnd),
      map(e => (e as NavigationEnd).url),
      startWith(this.router.url)
    ),
    { initialValue: this.router.url }
  );

  readonly pageTitle = computed(() => {
    const url = this.currentUrl();
    const titles: Record<string, string> = {
      '/dashboard': 'Dashboard',
      '/customers': 'Clientes',
      '/invoices':  'Facturas',
      '/sales':     'Órdenes de venta',
      '/inventory': 'Artículos',
      '/vendors':   'Proveedores',
      '/purchases': 'Compras',
      '/gl':        'Mayor General',
      '/journal':   'Diario',
      '/settings':  'Configuración',
    };
    const base = '/' + (url.split('/')[1] || 'dashboard');
    return titles[base] || 'Nexus Billing';
  });

  readonly navSections: NavSection[] = [
    {
      items: [{ label: 'Dashboard', route: '/dashboard', icon: 'layout-dashboard' }]
    },
    {
      group: 'Ventas',
      items: [
        { label: 'Clientes',  route: '/customers', icon: 'users' },
        { label: 'Órdenes',   route: '/sales',     icon: 'file-text' },
        { label: 'Facturas',  route: '/invoices',  icon: 'receipt', badge: 3 },
      ]
    },
    {
      group: 'Inventario',
      items: [
        { label: 'Artículos', route: '/inventory', icon: 'package' },
      ]
    },
    {
      group: 'Finanzas',
      items: [
        { label: 'Mayor General', route: '/gl',      icon: 'bar-chart-2' },
        { label: 'Diario',        route: '/journal', icon: 'book-open' },
      ]
    },
    {
      group: 'Administración',
      items: [
        { label: 'Configuración', route: '/settings', icon: 'settings' },
      ]
    }
  ];

  private readonly allPaletteItems: PaletteItem[] = [
    { id: 'dashboard', label: 'Dashboard',        desc: 'Vista general y KPIs',    icon: 'layout-dashboard', route: '/dashboard' },
    { id: 'customers', label: 'Clientes',          desc: 'Gestión de clientes',     icon: 'users',            route: '/customers' },
    { id: 'invoices',  label: 'Facturas',          desc: 'Facturas de venta',       icon: 'receipt',          route: '/invoices'  },
    { id: 'sales',     label: 'Órdenes de venta',  desc: 'Pedidos de ventas',       icon: 'file-text',        route: '/sales'     },
    { id: 'inventory', label: 'Artículos',         desc: 'Inventario y productos',  icon: 'package',          route: '/inventory' },
    { id: 'settings',  label: 'Configuración',     desc: 'Ajustes del sistema',     icon: 'settings',         route: '/settings'  },
  ];

  readonly filteredPaletteItems = computed(() => {
    const q = this.paletteQuery.toLowerCase().trim();
    if (!q) return this.allPaletteItems;
    return this.allPaletteItems.filter(
      i => i.label.toLowerCase().includes(q) || i.desc.toLowerCase().includes(q)
    );
  });

  constructor() {
    const saved = localStorage.getItem('nx-theme');
    if (saved === 'dark') {
      document.documentElement.setAttribute('data-theme', 'dark');
    }

    effect(() => {
      this.currentUrl();
      setTimeout(() => (window as any).lucide?.createIcons?.(), 0);
    });

    document.addEventListener('keydown', (e: KeyboardEvent) => {
      if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
        e.preventDefault();
        this.paletteOpen.update(v => !v);
      }
      if (e.key === 'Escape') {
        this.paletteOpen.set(false);
      }
    });
  }

  isActive(route: string): boolean {
    const url = this.currentUrl();
    if (route === '/dashboard') return url === '/dashboard' || url === '/';
    return url.startsWith(route);
  }

  navigate(route: string): void {
    this.router.navigate([route]);
  }

  toggleTheme(): void {
    this.theme.update(t => t === 'dark' ? 'light' : 'dark');
    const isDark = this.theme() === 'dark';
    document.documentElement.setAttribute('data-theme', isDark ? 'dark' : '');
    localStorage.setItem('nx-theme', isDark ? 'dark' : 'light');
  }

  openPalette(): void { this.paletteOpen.set(true); }

  closePalette(): void {
    this.paletteOpen.set(false);
    this.paletteQuery = '';
  }

  selectPaletteItem(item: PaletteItem): void {
    this.router.navigate([item.route]);
    this.closePalette();
  }

  onPaletteKey(e: KeyboardEvent): void {
    if (e.key === 'Escape') this.closePalette();
  }

  logout(): void { this.authService.logout(); }
}
