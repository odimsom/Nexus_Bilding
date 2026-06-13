import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ItemService, ItemFormData, LedgerEntry } from '../../../data/item.service';
import { Item } from '../../../domain/item.model';

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    @if (loading()) {
      <div class="nx-empty" style="min-height:300px;">
        <div class="nx-spinner"></div>
        <p class="nx-empty__text" style="margin-top:var(--nx-space-3);">Cargando producto…</p>
      </div>
    } @else if (item()) {
      <nav class="nx-crumbs" style="margin-bottom:var(--nx-space-4);">
        <a routerLink="/dashboard">Dashboard</a>
        <span class="nx-crumbs__sep">›</span>
        <a routerLink="/inventory">Productos</a>
        <span class="nx-crumbs__sep">›</span>
        <span class="nx-crumb--current">{{ item()!.description }}</span>
      </nav>

      <div class="nx-page-header">
        <div class="nx-avatar nx-avatar--lg" style="background:var(--nx-slate-100);color:var(--nx-slate-600);border-radius:var(--nx-radius-md);flex-shrink:0;">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.75" stroke-linecap="round" stroke-linejoin="round" style="width:22px;height:22px;">
            <path d="m7.5 4.27 9 5.15"/><path d="M21 8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16Z"/><path d="m3.3 7 8.7 5 8.7-5"/><path d="M12 22V12"/>
          </svg>
        </div>
        <div style="flex:1;min-width:0;">
          <h1 class="nx-page-title">{{ item()!.description }}</h1>
          @if (item()!.description2) {
            <p style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);margin-top:2px;">{{ item()!.description2 }}</p>
          }
          <div style="display:flex;align-items:center;gap:var(--nx-space-3);margin-top:var(--nx-space-1);flex-wrap:wrap;">
            <span class="nx-eyebrow">{{ item()!.no }}</span>
            <span class="nx-badge" [class]="typeBadge(item()!.type ?? '')">{{ typeLabel(item()!.type ?? '') }}</span>
            @if (item()!.itemCategoryCode) {
              <span class="nx-badge nx-badge--neutral">{{ item()!.itemCategoryCode }}</span>
            }
            @if (item()!.blocked) {
              <span class="nx-badge nx-badge--danger"><span class="nx-badge__dot"></span>Bloqueado</span>
            } @else {
              <span class="nx-badge nx-badge--success"><span class="nx-badge__dot"></span>Activo</span>
            }
          </div>
        </div>
        <div class="nx-page-actions">
          <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="openEdit()">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" style="width:14px;height:14px;"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/></svg>
            Editar
          </button>
          <button class="nx-btn nx-btn--primary nx-btn--sm" (click)="openAdjust()">Ajustar Inventario</button>
        </div>
      </div>

      <!-- KPI tiles -->
      <div class="kpi-row">
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Precio de Venta</div>
          <div class="nx-kpi-tile__value">{{ item()!.unitPrice | currency:'DOP':'symbol':'1.2-2' }}</div>
          <div class="nx-kpi-tile__sub">Precio lista</div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Costo Unitario</div>
          <div class="nx-kpi-tile__value">{{ item()!.unitCost | currency:'DOP':'symbol':'1.2-2' }}</div>
          <div class="nx-kpi-tile__sub">Último costo directo: {{ item()!.lastDirectCost ?? 0 | currency:'DOP':'symbol':'1.2-2' }}</div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Margen Bruto</div>
          <div class="nx-kpi-tile__value" [style.color]="margen() > 0 ? 'var(--nx-emerald-600)' : 'var(--nx-red-600)'">
            {{ margen() | number:'1.1-1' }}%
          </div>
          <div class="nx-kpi-tile__sub">{{ (item()!.unitPrice - item()!.unitCost) | currency:'DOP':'symbol':'1.2-2' }} por unidad</div>
        </div>
        <div class="nx-kpi-tile">
          <div class="nx-kpi-tile__label">Inventario Actual</div>
          <div class="nx-kpi-tile__value" [style.color]="(item()!.inventory ?? 0) === 0 ? 'var(--nx-red-600)' : 'inherit'">
            {{ (item()!.inventory ?? 0) | number:'1.0-0' }}
          </div>
          <div class="nx-kpi-tile__sub">{{ item()!.baseUnitOfMeasure }}</div>
        </div>
      </div>

      <div style="display:grid;grid-template-columns:1fr 300px;gap:var(--nx-space-4);align-items:start;">
        <div style="display:flex;flex-direction:column;gap:var(--nx-space-4);">

          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Información General</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">No. Producto</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.no }}</dd>
                <dt class="nx-kv__k">Descripción</dt>
                <dd class="nx-kv__v" style="font-weight:var(--nx-weight-medium);">{{ item()!.description }}</dd>
                @if (item()!.description2) {
                  <dt class="nx-kv__k">Descripción 2</dt>
                  <dd class="nx-kv__v">{{ item()!.description2 }}</dd>
                }
                <dt class="nx-kv__k">Tipo</dt>
                <dd class="nx-kv__v">{{ typeLabel(item()!.type ?? '') }}</dd>
                <dt class="nx-kv__k">Categoría</dt>
                <dd class="nx-kv__v">{{ item()!.itemCategoryCode || '—' }}</dd>
                <dt class="nx-kv__k">U/M Base</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.baseUnitOfMeasure }}</dd>
                @if (item()!.vendorNo) {
                  <dt class="nx-kv__k">Proveedor</dt>
                  <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.vendorNo }}</dd>
                  <dt class="nx-kv__k">No. Art. Proveedor</dt>
                  <dd class="nx-kv__v">{{ item()!.vendorItemNo || '—' }}</dd>
                }
              </dl>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Precios y Costos</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Precio de Venta</dt>
                <dd class="nx-kv__v nx-kv__v--mono" style="font-weight:var(--nx-weight-semibold);">
                  {{ item()!.unitPrice | currency:'DOP':'symbol':'1.2-2' }}
                </dd>
                <dt class="nx-kv__k">Costo Unitario</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.unitCost | currency:'DOP':'symbol':'1.2-2' }}</dd>
                <dt class="nx-kv__k">Costo Estándar</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.standardCost ?? 0 | currency:'DOP':'symbol':'1.2-2' }}</dd>
                <dt class="nx-kv__k">Último Costo Directo</dt>
                <dd class="nx-kv__v nx-kv__v--mono">{{ item()!.lastDirectCost ?? 0 | currency:'DOP':'symbol':'1.2-2' }}</dd>
                <dt class="nx-kv__k">Margen Bruto</dt>
                <dd class="nx-kv__v nx-kv__v--mono" [style.color]="margen() > 0 ? 'var(--nx-emerald-600)' : 'var(--nx-red-600)'">
                  {{ margen() | number:'1.1-1' }}%
                </dd>
              </dl>
            </div>
          </div>

          <div class="nx-card">
            <div class="nx-card__head"><div class="nx-card__title">Contabilización</div></div>
            <div class="nx-card__body">
              <dl class="nx-kv nx-kv--ruled nx-kv--2col">
                <dt class="nx-kv__k">Gr. Contab. Inventario</dt>
                <dd class="nx-kv__v">{{ item()!.inventoryPostingGroup || '—' }}</dd>
                <dt class="nx-kv__k">Gr. Contab. Gral. Prod.</dt>
                <dd class="nx-kv__v">{{ item()!.genProdPostingGroup || '—' }}</dd>
                <dt class="nx-kv__k">Gr. Contab. IVA Prod.</dt>
                <dd class="nx-kv__v">{{ item()!.vatProdPostingGroup || '—' }}</dd>
              </dl>
            </div>
          </div>

        </div>

        <!-- FactBox -->
        <div class="nx-factbox">
          <div class="nx-factbox__head">
            <div class="nx-factbox__eyebrow">Producto</div>
            <div class="nx-factbox__title">{{ item()!.no }}</div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Disponibilidad</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">En stock</span>
                <strong [style.color]="item()!.inventory === 0 ? 'var(--nx-money-negative)' : 'var(--nx-money-positive)'">
                  {{ item()!.inventory | number:'1.0-0' }} {{ item()!.baseUnitOfMeasure }}
                </strong>
              </div>
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">En Órdenes Venta</span>
                <strong>0</strong>
              </div>
              <div style="display:flex;justify-content:space-between;font-size:var(--nx-text-sm);">
                <span style="color:var(--nx-text-muted);">En Órdenes Compra</span>
                <strong>0</strong>
              </div>
            </div>
          </div>
          <div class="nx-factbox__section">
            <div class="nx-factbox__sectionlabel">Acciones Rápidas</div>
            <div style="display:flex;flex-direction:column;gap:var(--nx-space-2);">
              <button class="nx-btn nx-btn--secondary nx-btn--sm nx-btn--block" (click)="openEdit()">Editar Producto</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block" (click)="openLedger()">Diario de Producto</button>
              <button class="nx-btn nx-btn--subtle nx-btn--sm nx-btn--block">Movimientos de Producto</button>
              @if (item()!.blocked) {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { ✓ Desbloquear }
                </button>
              } @else {
                <button class="nx-btn nx-btn--ghost nx-btn--sm nx-btn--block" style="color:var(--nx-red-500);" [disabled]="actionLoading()" (click)="toggleBlock()">
                  @if (actionLoading()) { Procesando… } @else { Bloquear Producto }
                </button>
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
        <p class="nx-empty__title">Producto no encontrado</p>
        <a routerLink="/inventory" class="nx-btn nx-btn--secondary nx-btn--sm">Volver a Productos</a>
      </div>
    }

    <!-- Edit Modal -->
    @if (showEdit()) {
      <div class="modal-backdrop" (click)="closeEdit()">
        <div class="modal-box" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Editar: {{ item()!.no }}</h2>
            <button class="nx-iconbtn" (click)="closeEdit()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (editError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ editError() }}</div>
            }
            <div class="form-grid">
              <div class="nx-field">
                <label class="nx-label">No. Producto</label>
                <input class="nx-input" [value]="form.no" disabled style="opacity:.6;" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Tipo</label>
                <select class="nx-select" [(ngModel)]="form.type">
                  <option value="Inventory">Inventario</option>
                  <option value="Service">Servicio</option>
                  <option value="Non-Inventory">No Inventario</option>
                </select>
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Descripción *</label>
                <input class="nx-input" [(ngModel)]="form.description" />
              </div>
              <div class="nx-field" style="grid-column:1/-1;">
                <label class="nx-label">Descripción 2</label>
                <input class="nx-input" [(ngModel)]="form.description2" />
              </div>
              <div class="nx-field">
                <label class="nx-label">U/M Base</label>
                <input class="nx-input" [(ngModel)]="form.baseUnitOfMeasure" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Categoría</label>
                <input class="nx-input" [(ngModel)]="form.itemCategoryCode" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Precio de Venta (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.unitPrice" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Costo Unitario (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.unitCost" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Costo Estándar (DOP)</label>
                <input class="nx-input" type="number" [(ngModel)]="form.standardCost" />
              </div>
              <div class="nx-field">
                <label class="nx-label">No. Proveedor</label>
                <input class="nx-input" [(ngModel)]="form.vendorNo" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Gr. Contab. Inventario</label>
                <select class="nx-select" [(ngModel)]="form.inventoryPostingGroup">
                  <option value="">—</option><option>FINISHED</option><option>RAW MAT</option><option>RESALE</option>
                </select>
              </div>
              <div class="nx-field">
                <label class="nx-label">Gr. Contab. Gral. Prod.</label>
                <select class="nx-select" [(ngModel)]="form.genProdPostingGroup">
                  <option value="">—</option><option>RETAIL</option><option>SERVICES</option><option>WHOLESALE</option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeEdit()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="saving()" (click)="saveEdit()">
              @if (saving()) { Guardando… } @else { Guardar Cambios }
            </button>
          </div>
        </div>
      </div>
    }

    <!-- ── AJUSTE DE INVENTARIO (EXCEPCIONAL) ──────────────────────────────────── -->
    @if (showAdjust()) {
      <div class="modal-backdrop" (click)="closeAdjust()">
        <div class="modal-box" style="max-width:480px;" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h2 class="nx-page-title" style="margin:0;">Ajuste Excepcional de Inventario</h2>
            <button class="nx-iconbtn" (click)="closeAdjust()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (adjustError()) {
              <div class="nx-callout nx-callout--danger" style="margin-bottom:var(--nx-space-3);">{{ adjustError() }}</div>
            }
            <div class="nx-callout nx-callout--warn" style="margin-bottom:var(--nx-space-4);">
              <strong>Aviso de proceso:</strong> Las entradas normales de stock deben realizarse a través de un <strong>Pedido de Compra</strong>. Usa esta opción únicamente para ajustes físicos, mermas o adquisiciones atípicas.
            </div>
            <div class="nx-callout nx-callout--info" style="margin-bottom:var(--nx-space-4);">
              Disponibilidad actual: <strong>{{ item()!.inventory }} {{ item()!.baseUnitOfMeasure }}</strong>
            </div>
            <div class="form-grid" style="grid-template-columns:1fr;">
              <div class="nx-field">
                <label class="nx-label">Cantidad a Ajustar *</label>
                <input class="nx-input nx-num" type="number" [(ngModel)]="adjustForm.quantity"
                  placeholder="Positivo para entrada, negativo para salida" />
                <span style="font-size:var(--nx-text-xs);color:var(--nx-text-muted);margin-top:4px;">
                  Proyección de stock: <strong>{{ item()!.inventory + (adjustForm.quantity || 0) }} {{ item()!.baseUnitOfMeasure }}</strong>
                </span>
              </div>
              <div class="nx-field">
                <label class="nx-label">No. Documento Origen (Secuencia Automática)</label>
                <input class="nx-input" [value]="adjustForm.documentNo || '(Se generará en el diario)'" disabled style="font-family:var(--nx-font-mono);" />
              </div>
              <div class="nx-field">
                <label class="nx-label">Motivo del Ajuste *</label>
                <select class="nx-select" [(ngModel)]="adjustForm.description">
                  <option value="">Selecciona el motivo…</option>
                  <option value="Ajuste por Conteo Físico">Ajuste por Conteo Físico</option>
                  <option value="Merma o Daño">Merma o Daño</option>
                  <option value="Entrada Atípica / Promocional">Entrada Atípica / Promocional</option>
                  <option value="Consumo Interno">Consumo Interno (No facturable)</option>
                </select>
              </div>
            </div>
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeAdjust()">Cancelar</button>
            <button class="nx-btn nx-btn--primary" [disabled]="adjustSaving() || !adjustForm.description" (click)="saveAdjust()">
              @if (adjustSaving()) { Registrando… } @else { Confirmar Ajuste }
            </button>
          </div>
        </div>
      </div>
    }

    <!-- ── DIARIO DE ARTÍCULO ────────────────────────────────────── -->
    @if (showLedger()) {
      <div class="modal-backdrop" (click)="closeLedger()">
        <div class="modal-box" style="max-width:860px;" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <div>
              <h2 class="nx-page-title" style="margin:0;">Movimientos de Producto</h2>
              <div style="font-size:var(--nx-text-sm);color:var(--nx-text-muted);">{{ item()?.no }} — {{ item()?.description }}</div>
            </div>
            <button class="nx-iconbtn" (click)="closeLedger()">
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
            </button>
          </div>
          <div class="modal-body">
            @if (ledgerLoading()) {
              <div class="nx-empty"><div class="nx-spinner"></div></div>
            } @else if (ledgerEntries().length === 0) {
              <div class="nx-empty">
                <p class="nx-empty__title">Sin movimientos</p>
                <p class="nx-empty__text">Este producto aún no tiene entradas en el diario.</p>
              </div>
            } @else {
              <table class="nx-table">
                <thead>
                  <tr>
                    <th>No.</th>
                    <th>Fecha</th>
                    <th>Tipo</th>
                    <th>Documento</th>
                    <th>Descripción</th>
                    <th>U/M</th>
                    <th class="nx-th--num">Cantidad</th>
                    <th class="nx-th--num">Restante</th>
                  </tr>
                </thead>
                <tbody>
                  @for (e of ledgerEntries(); track e.entryNo) {
                    <tr>
                      <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ e.entryNo }}</td>
                      <td style="color:var(--nx-text-muted);font-size:var(--nx-text-sm);">{{ e.postingDate }}</td>
                      <td>
                        <span class="nx-badge" [class]="e.positive ? 'nx-badge--success' : 'nx-badge--warn'">
                          {{ e.entryTypeLabel }}
                        </span>
                      </td>
                      <td class="nx-td--doc">{{ e.documentNo || '—' }}</td>
                      <td>{{ e.description }}</td>
                      <td style="color:var(--nx-text-muted);">{{ e.unitOfMeasureCode }}</td>
                      <td class="nx-td--num nx-num" [style.color]="e.positive ? 'var(--nx-green-600)' : 'var(--nx-red-500)'" style="font-weight:var(--nx-weight-semibold);">
                        {{ e.positive ? '+' : '' }}{{ e.quantity | number:'1.0-2' }}
                      </td>
                      <td class="nx-td--num nx-num" style="color:var(--nx-text-muted);">{{ e.remainingQuantity | number:'1.0-2' }}</td>
                    </tr>
                  }
                </tbody>
              </table>
              <div style="margin-top:var(--nx-space-3);font-size:var(--nx-text-sm);color:var(--nx-text-muted);">
                Mostrando {{ ledgerEntries().length }} de {{ ledgerTotal() }} movimientos
              </div>
            }
          </div>
          <div class="modal-footer">
            <button class="nx-btn nx-btn--ghost" (click)="closeLedger()">Cerrar</button>
            <button class="nx-btn nx-btn--secondary nx-btn--sm" (click)="openAdjust(); closeLedger()">Nuevo Ajuste</button>
          </div>
        </div>
      </div>
    }
  `,
  styles: [`
    :host { display: block; }
    .nx-spinner { width:32px;height:32px;border:3px solid var(--nx-border);border-top-color:var(--nx-action);border-radius:50%;animation:spin 0.8s linear infinite;margin:0 auto; }
    @keyframes spin { to { transform:rotate(360deg); } }

    .kpi-row { display:grid;grid-template-columns:repeat(4,1fr);gap:var(--nx-space-3);margin-bottom:var(--nx-space-4); }
    .nx-kpi-tile { background:var(--nx-surface);border:1px solid var(--nx-border);border-radius:var(--nx-radius-lg);padding:var(--nx-space-4);display:flex;flex-direction:column;gap:2px; }
    .nx-kpi-tile__label { font-size:var(--nx-text-xs);color:var(--nx-text-muted);text-transform:uppercase;letter-spacing:.06em; }
    .nx-kpi-tile__value { font-size:1.4rem;font-weight:var(--nx-weight-semibold);font-variant-numeric:tabular-nums;line-height:1.2; }
    .nx-kpi-tile__sub { font-size:var(--nx-text-xs);color:var(--nx-text-muted); }
    .nx-kv--2col { display:grid;grid-template-columns:max-content 1fr; }

    .modal-backdrop { position:fixed;inset:0;background:rgba(0,0,0,.45);display:flex;align-items:center;justify-content:center;z-index:1000; }
    .modal-box { background:var(--nx-surface);border-radius:var(--nx-radius-xl);width:min(720px,96vw);max-height:90vh;display:flex;flex-direction:column;box-shadow:var(--nx-shadow-xl); }
    .modal-header { display:flex;align-items:center;justify-content:space-between;padding:var(--nx-space-5) var(--nx-space-6);border-bottom:1px solid var(--nx-border); }
    .modal-body { overflow-y:auto;padding:var(--nx-space-5) var(--nx-space-6);flex:1; }
    .modal-footer { padding:var(--nx-space-4) var(--nx-space-6);border-top:1px solid var(--nx-border);display:flex;justify-content:flex-end;gap:var(--nx-space-3); }
    .form-grid { display:grid;grid-template-columns:1fr 1fr;gap:var(--nx-space-4); }
    .nx-field { display:flex;flex-direction:column;gap:var(--nx-space-1); }

    @media (max-width:900px) { .kpi-row { grid-template-columns:1fr 1fr; } }
    @media (max-width:600px) { .kpi-row { grid-template-columns:1fr; } .form-grid { grid-template-columns:1fr; } }
  `]
})
export class ItemCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly svc = inject(ItemService);

  item = signal<Item | null>(null);
  loading = signal(true);
  actionLoading = signal(false);
  showEdit = signal(false);
  saving = signal(false);
  editError = signal<string | null>(null);
  form: ItemFormData = {} as ItemFormData;

  // Ajuste de inventario
  showAdjust   = signal(false);
  adjustSaving = signal(false);
  adjustError  = signal('');
  adjustForm   = { quantity: 0, documentNo: '', description: '' };

  // Diario de producto
  showLedger    = signal(false);
  ledgerLoading = signal(false);
  ledgerEntries = signal<LedgerEntry[]>([]);
  ledgerTotal   = signal(0);

  margen(): number {
    const i = this.item();
    if (!i || i.unitPrice <= 0) return 0;
    return (i.unitPrice - i.unitCost) / i.unitPrice * 100;
  }

  typeLabel(t: string): string {
    return { Inventory: 'Inventario', Service: 'Servicio', 'Non-Inventory': 'No Inventario' }[t] ?? t ?? '—';
  }

  typeBadge(t: string): string {
    if (t === 'Service') return 'nx-badge nx-badge--info';
    if (t === 'Non-Inventory') return 'nx-badge nx-badge--warn';
    return 'nx-badge nx-badge--outline';
  }

  async ngOnInit(): Promise<void> {
    const no = this.route.snapshot.paramMap.get('no') ?? '';
    this.loading.set(true);
    const item = await this.svc.getByNo(no);
    this.item.set(item);
    this.loading.set(false);
  }

  openEdit(): void {
    const i = this.item()!;
    this.form = {
      no: i.no, description: i.description, description2: i.description2 ?? '',
      baseUnitOfMeasure: i.baseUnitOfMeasure, unitPrice: i.unitPrice,
      unitCost: i.unitCost, standardCost: i.standardCost ?? 0,
      type: i.type ?? 'Inventory', itemCategoryCode: i.itemCategoryCode ?? '',
      inventoryPostingGroup: i.inventoryPostingGroup ?? '',
      genProdPostingGroup: i.genProdPostingGroup ?? '',
      vatProdPostingGroup: i.vatProdPostingGroup ?? '',
      vendorNo: i.vendorNo ?? '', vendorItemNo: i.vendorItemNo ?? ''
    };
    this.editError.set(null);
    this.showEdit.set(true);
  }

  closeEdit(): void { this.showEdit.set(false); }

  async toggleBlock(): Promise<void> {
    const i = this.item();
    if (!i) return;
    this.actionLoading.set(true);
    try {
      if (i.blocked) await this.svc.unblock(i.no);
      else await this.svc.block(i.no);
      const updated = await this.svc.getByNo(i.no);
      this.item.set(updated);
    } catch (e: any) {
      alert(e?.error?.error?.message ?? 'Error al cambiar el estado del producto.');
    } finally {
      this.actionLoading.set(false);
    }
  }

  openAdjust(): void {
    this.adjustForm = { quantity: 0, documentNo: '', description: '' };
    this.adjustError.set('');
    this.showAdjust.set(true);
  }

  closeAdjust(): void { this.showAdjust.set(false); }

  async saveAdjust(): Promise<void> {
    if (!this.adjustForm.quantity) { this.adjustError.set('La cantidad es obligatoria y no puede ser cero.'); return; }
    this.adjustSaving.set(true);
    this.adjustError.set('');
    try {
      await this.svc.adjustInventory(
        this.item()!.no,
        this.adjustForm.quantity,
        this.adjustForm.documentNo,
        this.adjustForm.description || 'Ajuste manual'
      );
      const updated = await this.svc.getByNo(this.item()!.no);
      this.item.set(updated);
      this.showAdjust.set(false);
    } catch (e: any) {
      this.adjustError.set(e?.error?.error?.message ?? 'Error al registrar el ajuste.');
    } finally {
      this.adjustSaving.set(false);
    }
  }

  async openLedger(): Promise<void> {
    this.showLedger.set(true);
    this.ledgerLoading.set(true);
    try {
      const result = await this.svc.getLedger(this.item()!.no);
      this.ledgerEntries.set(result.items);
      this.ledgerTotal.set(result.totalCount);
    } catch {
      this.ledgerEntries.set([]);
    } finally {
      this.ledgerLoading.set(false);
    }
  }

  closeLedger(): void { this.showLedger.set(false); }

  async saveEdit(): Promise<void> {
    if (!this.form.description?.trim()) {
      this.editError.set('La descripción es obligatoria.');
      return;
    }
    this.saving.set(true);
    this.editError.set(null);
    try {
      await this.svc.update(this.item()!.no, this.form);
      const updated = await this.svc.getByNo(this.item()!.no);
      this.item.set(updated);
      this.closeEdit();
    } catch (e: any) {
      this.editError.set(e?.error?.error?.message ?? 'Error al guardar los cambios.');
    } finally {
      this.saving.set(false);
    }
  }
}
