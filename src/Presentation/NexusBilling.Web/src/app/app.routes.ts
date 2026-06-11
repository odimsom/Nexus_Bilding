import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/presentation/pages/login/login.page';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginPage },
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
      {
        path: 'sales',
        loadComponent: () =>
          import('./features/sales/presentation/pages/sales-order-list/sales-order-list.page').then(
            m => m.SalesOrderListPage
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
      /* ── RUTA CATCH-ALL dentro del shell ────────────────── */
      { path: '**', redirectTo: 'dashboard' }
    ]
  },
  { path: '**', redirectTo: '' }
];
