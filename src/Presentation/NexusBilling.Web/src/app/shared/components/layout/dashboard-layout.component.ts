import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="dashboard-wrapper">
      <aside class="sidebar">
        <div class="sidebar-header">
          <img src="assets/logo.svg" alt="NexusBilling" class="logo">
          <span>NexusBilling</span>
        </div>
        
        <nav class="sidebar-nav">
          <a routerLink="/dashboard" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">
            <i class="icon-dashboard"></i> Dashboard
          </a>
          <a routerLink="/sales" routerLinkActive="active">
            <i class="icon-sales"></i> Ventas
          </a>
          <a routerLink="/inventory" routerLinkActive="active">
            <i class="icon-inventory"></i> Inventario
          </a>
          <a routerLink="/finance" routerLinkActive="active">
            <i class="icon-finance"></i> Finanzas
          </a>
        </nav>

        <div class="sidebar-footer">
          <button (click)="logout()" class="btn-logout">
            <i class="icon-logout"></i> Cerrar Sesión
          </button>
        </div>
      </aside>

      <main class="main-content">
        <header class="top-bar">
          <div class="search-box">
            <input type="text" placeholder="Buscar en el sistema...">
          </div>
          <div class="user-info" *ngIf="user()">
            <span class="username">{{ user()?.username }}</span>
            <div class="user-avatar">{{ user()?.username?.charAt(0) }}</div>
          </div>
        </header>
        
        <section class="content-area">
          <router-outlet></router-outlet>
        </section>
      </main>
    </div>
  `,
  styles: [`
    .dashboard-wrapper {
      display: flex;
      height: 100vh;
      background: #0b0e14;
      color: #fff;
    }

    .sidebar {
      width: 260px;
      background: #11151c;
      border-right: 1px solid rgba(255,255,255,0.05);
      display: flex;
      flex-direction: column;
    }

    .sidebar-header {
      padding: 2rem;
      display: flex;
      align-items: center;
      gap: 1rem;
      font-weight: 700;
      font-size: 1.2rem;
    }

    .sidebar-header .logo { width: 32px; }

    .sidebar-nav {
      flex: 1;
      padding: 1rem;
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }

    .sidebar-nav a {
      padding: 0.8rem 1rem;
      color: #8b9bb4;
      text-decoration: none;
      border-radius: 8px;
      transition: all 0.3s;
      display: flex;
      align-items: center;
      gap: 0.75rem;
    }

    .sidebar-nav a:hover, .sidebar-nav a.active {
      background: rgba(255,255,255,0.05);
      color: #fff;
    }

    .sidebar-nav a.active {
      background: #007bff;
      color: #fff;
    }

    .main-content {
      flex: 1;
      display: flex;
      flex-direction: column;
      overflow: hidden;
    }

    .top-bar {
      height: 70px;
      padding: 0 2rem;
      display: flex;
      align-items: center;
      justify-content: space-between;
      border-bottom: 1px solid rgba(255,255,255,0.05);
    }

    .search-box input {
      background: #1a1f29;
      border: none;
      padding: 0.6rem 1rem;
      border-radius: 20px;
      color: #fff;
      width: 300px;
    }

    .user-info {
      display: flex;
      align-items: center;
      gap: 1rem;
    }

    .user-avatar {
      width: 35px;
      height: 35px;
      background: #28c76f;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 700;
    }

    .content-area {
      flex: 1;
      padding: 2rem;
      overflow-y: auto;
    }

    .sidebar-footer {
      padding: 1.5rem;
      border-top: 1px solid rgba(255,255,255,0.05);
    }

    .btn-logout {
      width: 100%;
      background: transparent;
      border: 1px solid rgba(220,53,69,0.3);
      color: #ff6b6b;
      padding: 0.6rem;
      border-radius: 8px;
      cursor: pointer;
      transition: all 0.3s;
    }

    .btn-logout:hover {
      background: rgba(220,53,69,0.1);
    }
  `]
})
export class DashboardLayoutComponent {
  private readonly authService = inject(AuthService);
  readonly user = this.authService.user;

  logout(): void {
    this.authService.logout();
  }
}
