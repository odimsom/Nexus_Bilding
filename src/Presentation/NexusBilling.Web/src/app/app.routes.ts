import { Routes } from '@angular/router';
import { LoginPage } from './features/auth/presentation/pages/login/login.page';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginPage },
  { 
    path: 'dashboard', 
    loadComponent: () => import('./shared/components/layout/dashboard-layout.component').then(m => m.DashboardLayoutComponent),
    canActivate: [authGuard],
    children: [
      { path: '', loadComponent: () => import('./features/dashboard/presentation/pages/dashboard.page').then(m => m.DashboardPage) }
    ]
  },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard' }
];
