import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-purchase-order-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
      <a routerLink="/dashboard">Dashboard</a>
      <span class="nx-crumbs__sep">›</span>
      <span class="nx-crumb--current">Pedidos de Compra</span>
    </nav>

    <div class="nx-page-header" style="margin-bottom:var(--nx-space-5);">
      <div>
        <h1 class="nx-page-title">Pedidos de Compra</h1>
        <p class="nx-page-subtitle">Gestiona tus órdenes de abastecimiento y entradas de almacén</p>
      </div>
      <div class="nx-page-actions">
        <button class="nx-btn nx-btn--primary">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:16px;height:16px;"><line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/></svg>
          Nuevo Pedido
        </button>
      </div>
    </div>

    <div class="nx-card">
       <div class="nx-empty" style="padding:var(--nx-space-8);">
          <div class="nx-empty__icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z"/><polyline points="7.5 4.21 12 6.81 16.5 4.21"/><polyline points="7.5 19.79 12 17.19 16.5 19.79"/><polyline points="3.27 6.96 12 12.01 20.73 6.96"/><line x1="12" y1="22.08" x2="12" y2="12"/></svg>
          </div>
          <p class="nx-empty__title">Módulo de Compras en preparación</p>
          <p class="nx-empty__text">Estamos habilitando la gestión de órdenes de compra. Pronto podrás abastecer tu inventario desde aquí.</p>
       </div>
    </div>
  `
})
export class PurchaseOrderListPage {}
