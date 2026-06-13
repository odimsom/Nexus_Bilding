import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/presentation/pages/login/login.page';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () =>
      import('./features/landing/presentation/pages/landing.page').then(
        m => m.LandingPage
      )
  },
  { path: 'login', component: LoginPage },
  {
    path: 'forgot-password',
    loadComponent: () =>
      import('./features/auth/presentation/pages/forgot-password/forgot-password.page').then(
        m => m.ForgotPasswordPage
      )
  },
  {
    path: '',
    loadComponent: () =>
      import('./shared/components/layout/dashboard-layout.component').then(
        m => m.DashboardLayoutComponent
      ),
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/presentation/pages/dashboard.page').then(
            m => m.DashboardPage
          )
      },
      /* ── VENTAS ─────────────────────────────────────────── */
      {
        path: 'customers',
        loadComponent: () =>
          import('./features/customers/presentation/pages/customer-list/customer-list.page').then(
            m => m.CustomerListPage
          )
      },
      {
        path: 'customers/:no',
        loadComponent: () =>
          import('./features/customers/presentation/pages/customer-card/customer-card.page').then(
            m => m.CustomerCardPage
          )
      },
      /* ── COMPRAS ────────────────────────────────────────── */
      {
        path: 'vendors',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/vendor-list/vendor-list.page').then(
            m => m.VendorListPage
          )
      },
      {
        path: 'vendors/:no',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/vendor-card/vendor-card.page').then(
            m => m.VendorCardPage
          )
      },
      {
        path: 'purchases',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/purchase-order-list/purchase-order-list.page').then(
            m => m.PurchaseOrderListPage
          )
      },
      {
        path: 'purchases/:no',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/purchase-order-card/purchase-order-card.page').then(
            m => m.PurchaseOrderCardPage
          )
      },
      {
        path: 'purchase-invoices',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/purchase-invoice-list/purchase-invoice-list.page').then(
            m => m.PurchaseInvoiceListPage
          )
      },
      {
        path: 'purchase-invoices/:no',
        loadComponent: () =>
          import('./features/purchasing/presentation/pages/purchase-invoice-card/purchase-invoice-card.page').then(
            m => m.PurchaseInvoiceCardPage
          )
      },
      {
        path: 'sales',
        loadComponent: () =>
          import('./features/sales/presentation/pages/sales-order-list/sales-order-list.page').then(
            m => m.SalesOrderListPage
          )
      },
      {
        path: 'sales/:no',
        loadComponent: () =>
          import('./features/sales/presentation/pages/sales-order-card/sales-order-card.page').then(
            m => m.SalesOrderCardPage
          )
      },
      {
        path: 'invoices',
        loadComponent: () =>
          import('./features/sales/presentation/pages/invoice-list/invoice-list.page').then(
            m => m.InvoiceListPage
          )
      },
      {
        path: 'invoices/:no',
        loadComponent: () =>
          import('./features/sales/presentation/pages/invoice-card/invoice-card.page').then(
            m => m.InvoiceCardPage
          )
      },
      /* ── INVENTARIO ─────────────────────────────────────── */
      {
        path: 'inventory',
        loadComponent: () =>
          import('./features/inventory/presentation/pages/item-list/item-list.page').then(
            m => m.ItemListPage
          )
      },
      {
        path: 'inventory/:no',
        loadComponent: () =>
          import('./features/inventory/presentation/pages/item-card/item-card.page').then(
            m => m.ItemCardPage
          )
      },
      /* ── SERVICIO ───────────────────────────────────────── */
      {
        path: 'services',
        loadComponent: () =>
          import('./features/services/presentation/pages/service-order-list/service-order-list.page').then(
            m => m.ServiceOrderListPage
          )
      },
      {
        path: 'services/:no',
        loadComponent: () =>
          import('./features/services/presentation/pages/service-order-card/service-order-card.page').then(
            m => m.ServiceOrderCardPage
          )
      },
      /* ── CONFIGURACIÓN Y SEGURIDAD ──────────────────────── */
      {
        path: 'settings',
        loadComponent: () =>
          import('./features/settings/presentation/pages/settings.page').then(
            m => m.SettingsPage
          )
      },
      {
        path: 'users',
        loadComponent: () =>
          import('./features/security/presentation/pages/user-list/user-list.page').then(
            m => m.UserListPage
          )
      },
      /* ── RUTA CATCH-ALL dentro del shell ────────────────── */
      { path: '**', redirectTo: 'dashboard' }
    ]
  },
  { path: '**', redirectTo: '' }
];
