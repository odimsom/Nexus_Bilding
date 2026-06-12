import { Component, inject, signal, computed, effect } from '@angular/core';
import { RouterOutlet, Router, NavigationEnd } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter, map, startWith } from 'rxjs/operators';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';

interface NavLeaf  { label: string; route: string; icon: string; }
interface NavGroup { group: string; icon: string; children: NavLeaf[]; }
type NavEntry = { kind: 'leaf'; item: NavLeaf } | { kind: 'group'; item: NavGroup };

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
          <img src="/logo-mark.svg" width="26" height="26" alt="Nexus Billing" />
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

          <!-- Dashboard (solo ítem) -->
          <button
            class="erp-navitem"
            [class.active]="isActive('/dashboard')"
            (click)="navigate('/dashboard')"
          >
            <i data-lucide="layout-dashboard" style="width:16px;height:16px;flex:none;"></i>
            <span>Dashboard</span>
          </button>

          @for (entry of navEntries; track $index) {
            @if (entry.kind === 'group') {
              <!-- Dropdown group -->
              <div class="erp-navgroup-wrap">
                <button
                  class="erp-navitem erp-navitem--group"
                  [class.contains-active]="groupActive(entry.item)"
                  [class.open]="openGroups().has(entry.item.group)"
                  (click)="toggleGroup(entry.item.group)"
                >
                  <i [attr.data-lucide]="entry.item.icon" style="width:16px;height:16px;flex:none;"></i>
                  <span>{{ entry.item.group }}</span>
                  <i data-lucide="chevron-right" class="erp-navitem__chevron" style="width:14px;height:14px;margin-left:auto;flex:none;"></i>
                </button>
                @if (openGroups().has(entry.item.group)) {
                  <div class="erp-subnav">
                    @for (child of entry.item.children; track child.route) {
                      <button
                        class="erp-navitem erp-navitem--child"
                        [class.active]="isActive(child.route)"
                        (click)="navigate(child.route)"
                      >
                        <i [attr.data-lucide]="child.icon" style="width:14px;height:14px;flex:none;"></i>
                        <span>{{ child.label }}</span>
                      </button>
                    }
                  </div>
                }
              </div>
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

        <!-- Content (sin erp-cmd redundante) -->
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
  styles: [`
    :host { display: contents; }

    .erp-navgroup-wrap { display: flex; flex-direction: column; }

    .erp-navitem--group .erp-navitem__chevron {
      transition: transform 140ms ease;
    }
    .erp-navitem--group.open .erp-navitem__chevron {
      transform: rotate(90deg);
    }
    /* Group headers stay neutral when a child is active — only leaf items get .active */

    .erp-subnav {
      display: flex;
      flex-direction: column;
      padding-left: 12px;
      border-left: 1px solid var(--nx-border);
      margin-left: 20px;
      margin-bottom: 2px;
    }

    .erp-navitem--child {
      font-size: 13px;
      padding: 5px 10px;
      gap: 8px;
    }
  `]
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

  readonly openGroups = signal<Set<string>>(new Set(['Ventas', 'Inventario', 'Compras', 'Servicio', 'Finanzas', 'Administración']));

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
      '/services':  'Órdenes de Servicio',
    };
    const base = '/' + (url.split('/')[1] || 'dashboard');
    return titles[base] || 'Nexus Billing';
  });

  readonly navEntries: NavEntry[] = [
    {
      kind: 'group',
      item: {
        group: 'Ventas',
        icon: 'shopping-cart',
        children: [
          { label: 'Clientes',  route: '/customers', icon: 'users' },
          { label: 'Cotizaciones', route: '/quotes', icon: 'file-clock' },
          { label: 'Órdenes',   route: '/sales',     icon: 'file-text' },
          { label: 'Facturas',  route: '/invoices',  icon: 'receipt' },
        ]
      }
    },
    {
      kind: 'group',
      item: {
        group: 'Inventario',
        icon: 'package',
        children: [
          { label: 'Artículos', route: '/inventory', icon: 'boxes' },
        ]
      }
    },
    {
      kind: 'group',
      item: {
        group: 'Compras',
        icon: 'truck',
        children: [
          { label: 'Proveedores', route: '/vendors', icon: 'users-2' },
          { label: 'Órdenes',     route: '/purchases', icon: 'file-text' },
          { label: 'Facturas',    route: '/purch-invoices', icon: 'receipt' },
        ]
      }
    },
    {
      kind: 'group',
      item: {
        group: 'Servicio',
        icon: 'wrench',
        children: [
          { label: 'Órdenes de Servicio', route: '/services', icon: 'clipboard-list' },
        ]
      }
    },
    {
      kind: 'group',
      item: {
        group: 'Finanzas',
        icon: 'landmark',
        children: [
          { label: 'Mayor General', route: '/gl',      icon: 'bar-chart-2' },
          { label: 'Diario',        route: '/journal', icon: 'book-open' },
        ]
      }
    },
    {
      kind: 'group',
      item: {
        group: 'Administración',
        icon: 'settings-2',
        children: [
          { label: 'Configuración', route: '/settings', icon: 'settings' },
        ]
      }
    }
  ];

  private readonly allPaletteItems: PaletteItem[] = [
    { id: 'dashboard', label: 'Dashboard',        desc: 'Vista general y KPIs',    icon: 'layout-dashboard', route: '/dashboard' },
    { id: 'customers', label: 'Clientes',          desc: 'Gestión de clientes',     icon: 'users',            route: '/customers' },
    { id: 'invoices',  label: 'Facturas',          desc: 'Facturas de venta',       icon: 'receipt',          route: '/invoices'  },
    { id: 'sales',     label: 'Órdenes de venta',  desc: 'Pedidos de ventas',       icon: 'file-text',        route: '/sales'     },
    { id: 'inventory', label: 'Artículos',         desc: 'Inventario y productos',  icon: 'package',          route: '/inventory' },
    { id: 'vendors',   label: 'Proveedores',       desc: 'Gestión de suplidores',   icon: 'users-2',          route: '/vendors'   },
    { id: 'services',  label: 'Órdenes Servicio',  desc: 'Órdenes de servicio',     icon: 'clipboard-list',   route: '/services'  },
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

    // Auto-expand group that contains the current route on load
    effect(() => {
      const url = this.currentUrl();
      for (const entry of this.navEntries) {
        if (entry.kind === 'group') {
          const match = entry.item.children.some(c => url.startsWith(c.route));
          if (match) {
            this.openGroups.update(s => { const n = new Set(s); n.add(entry.item.group); return n; });
          }
        }
      }
    }, { allowSignalWrites: true });

    document.addEventListener('keydown', (e: KeyboardEvent) => {
      if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
        e.preventDefault();
        this.paletteOpen.update(v => !v);
      }
      if (e.key === 'Escape') this.paletteOpen.set(false);
    });
  }

  toggleGroup(group: string): void {
    this.openGroups.update(s => {
      const n = new Set(s);
      n.has(group) ? n.delete(group) : n.add(group);
      return n;
    });
  }

  groupActive(item: NavGroup): boolean {
    const url = this.currentUrl();
    return item.children.some(c => url.startsWith(c.route));
  }

  isActive(route: string): boolean {
    const url = this.currentUrl();
    if (route === '/dashboard') return url === '/dashboard' || url === '/';
    return url.startsWith(route);
  }

  navigate(route: string): void { this.router.navigate([route]); }

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
