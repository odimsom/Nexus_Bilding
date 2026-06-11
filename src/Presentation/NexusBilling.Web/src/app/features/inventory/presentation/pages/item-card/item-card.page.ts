import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ItemService } from '../../data/item.service';
import { Item } from '../../domain/item.model';

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    @if (item()) {
      <!-- Breadcrumb -->
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/inventory">Artículos</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ item()!.no }}</span>
      </nav>

      <!-- Header -->
      <div class="nx-page-header">
        <div class="nx-avatar nx-avatar--lg" style="background:var(--nx-slate-100);color:var(--nx-slate-600);border-radius:var(--nx-radius-md);">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.75" stroke-linecap="round" stroke-linejoin="round" style="width:22px;height:22px;">
            <path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/>
          </svg>
        </div>
        <div>
          <h1 class="nx-page-title">{{ item()!.description }}</h1>
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);">
            <span class="nx-eyebrow">{{ item()!.no }}</span>
            @if (item()!.blocked) {
              <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
            } @else {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
            }
            @if (item()!.type) {
              <span class="nx-badge nx-badge--outline">{{ typeLabel(item()!.type) }}</span>
            }
            @if (item()!.itemCategoryCode) {
              <span class="nx-badge nx-badge--outline">{{ item()!.itemCategoryCode }}</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm">Editar</button>
          <button class="nx-btn nx-btn--primary nx-btn--sm">Ajustar Inventario</button>
        </div>
      </div>

      <!-- KPI tiles -->
      <div style="display:grid;grid-template-columns:repeat(auto-fit,minmax(160px,1fr));gap:var(--nx-space-4);margin-bottom:var(--nx-space-6);">
        <div class="nx-stat">
          <span class="nx-stat__label">Precio Venta</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">RD$ {{ item()!.unitPrice | number:'1.2-2' }}</span>
        </div>
        <div class="nx-stat">
          <span class="nx-stat__label">Costo Unitario</span>
          <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">RD$ {{ item()!.unitCost | number:'1.2-2' }}</span>
        </div>
        @if (item()!.type !== 'Service') {
          <div class="nx-stat">
            <span class="nx-stat__label">Inventario</span>
            <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);" [class.nx-amount--negative]="(item()!.inventory ?? 0) === 0">
              {{ item()!.inventory ?? 0 | number:'1.0-0' }} {{ item()!.baseUnitOfMeasure }}
            </span>
          </div>
          <div class="nx-stat">
            <span class="nx-stat__label">En Órdenes de Venta</span>
            <span class="nx-stat__value nx-num" style="font-size:var(--nx-text-2xl);">{{ item()!.qtyOnSalesOrder ?? 0 }}</span>
          </div>
        }
      </div>

      <!-- Content grid -->
      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">

        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <!-- General -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Información General</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled">
                <dt class="nx-kv__k">No. Artículo</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.no }}</dd>

                <dt class="nx-kv__k">Descripción</dt>
                <dd class="nx-kv__v">{{ item()!.description }}</dd>

                @if (item()!.description2) {
                  <dt class="nx-kv__k">Descripción 2</dt>
                  <dd class="nx-kv__v">{{ item()!.description2 }}</dd>
                }

                <dt class="nx-kv__k">Tipo</dt>
                <dd class="nx-kv__v">{{ typeLabel(item()!.type) }}</dd>

                <dt class="nx-kv__k">U/M Base</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.baseUnitOfMeasure }}</dd>

                <dt class="nx-kv__k">Categoría</dt>
                <dd class="nx-kv__v">{{ item()!.itemCategoryCode || '—' }}</dd>

                <dt class="nx-kv__k">Grupo Contab. Inv.</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.inventoryPostingGroup || '—' }}</dd>

                <dt class="nx-kv__k">Grupo Contab. Gen. Prod.</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.genProdPostingGroup || '—' }}</dd>
              </dl>
            </div>
          </div>

          <!-- Pricing -->
          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Precios y Costos</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled">
                <dt class="nx-kv__k">Precio de Venta Unitario</dt>
                <dd class="nx-kv__v nx-kv__v--mono">RD$ {{ item()!.unitPrice | number:'1.2-2' }}</dd>

                <dt class="nx-kv__k">Costo Unitario</dt>
                <dd class="nx-kv__v nx-kv__v--mono">RD$ {{ item()!.unitCost | number:'1.2-2' }}</dd>

                @if (item()!.standardCost != null) {
                  <dt class="nx-kv__k">Costo Estándar</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">RD$ {{ item()!.standardCost | number:'1.2-2' }}</dd>
                }

                @if (item()!.lastDirectCost != null) {
                  <dt class="nx-kv__k">Último Costo Directo</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">RD$ {{ item()!.lastDirectCost | number:'1.2-2' }}</dd>
                }

                <dt class="nx-kv__k">Margen</dt>
                <dd class="nx-kv__v nx-kv__v--mono" style="color:var(--nx-money-positive);">
                  @if (item()!.unitPrice > 0) {
                    {{ ((item()!.unitPrice - item()!.unitCost) / item()!.unitPrice * 100) | number:'1.1-1' }}%
                  } @else { — }
                </dd>
              </dl>
            </div>
          </div>

        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Artículo</div>
            <div class="nx-factbox__title">{{ item()!.no }}</div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Disponibilidad</div>
            <dl class="nx-kv">
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">En stock</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);" [class.nx-amount--negative]="(item()!.inventory ?? 0) === 0">
                {{ (item()!.inventory ?? 0) | number:'1.0-0' }}
              </dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">En ód. venta</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ item()!.qtyOnSalesOrder ?? 0 }}</dd>
              <dt class="nx-kv__k" style="font-size:var(--nx-text-xs);">En ód. compra</dt>
              <dd class="nx-kv__v nx-kv__v--mono" style="font-size:var(--nx-text-sm);">{{ item()!.qtyOnPurchOrder ?? 0 }}</dd>
            </dl>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Acciones Rápidas</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block">Diario de Artículo</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block">Movimientos de Artículo</button>
              @if (item()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block">Desbloquear</button>
              } @else {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" style="color:var(--nx-red-500);">Bloquear</button>
              }
            </div>
          </div>
        </div>

      </div>
    } @else {
      <div class="nx-empty">
        <div class="nx-empty__icon">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>
        </div>
        <p class="nx-empty__title">Artículo no encontrado</p>
        <a routerLink="/inventory" class="nx-btn nx-btn--secondary nx-btn--sm">Volver a Artículos</a>
      </div>
    }
  `,
  styles: [':host { display: block; }']
})
export class ItemCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(ItemService);

  item = signal<Item | undefined>(undefined);

  ngOnInit(): void {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.item.set(this.svc.getByNo(no));
  }

  typeLabel(t?: string): string {
    return { Inventory: 'Inventario', Service: 'Servicio', 'Non-Inventory': 'No Inventario' }[t ?? ''] ?? (t ?? '—');
  }
}
