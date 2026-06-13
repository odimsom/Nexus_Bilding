import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';
import { PurchaseInvoiceDetail } from '../../../domain/purchase-invoice.model';

@Component({
  selector: 'app-purchase-invoice-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="h-full flex flex-col p-6 animate-fade-in max-w-5xl mx-auto w-full">
      
      <!-- Skeleton Loading -->
      <div *ngIf="loading()" class="animate-pulse space-y-6">
        <div class="h-8 bg-neutral-800 rounded w-1/4"></div>
        <div class="h-40 bg-neutral-900 rounded-xl border border-neutral-800"></div>
        <div class="h-64 bg-neutral-900 rounded-xl border border-neutral-800"></div>
      </div>

      <!-- Content -->
      <ng-container *ngIf="!loading() && invoice()">
        <div class="flex items-center justify-between mb-6">
          <div class="flex items-center gap-4">
            <button routerLink="/purchase-invoices" class="w-10 h-10 flex items-center justify-center rounded-xl bg-neutral-900 border border-neutral-800 text-neutral-400 hover:text-white hover:bg-neutral-800 transition-colors">
              <i class="fi fi-rr-arrow-left"></i>
            </button>
            <div>
              <div class="flex items-center gap-3 mb-1">
                <h1 class="text-3xl font-bold text-white">Factura de Compra {{ invoice()?.no }}</h1>
                <span class="px-2.5 py-1 bg-blue-500/10 text-blue-400 border border-blue-500/20 rounded-full text-xs font-medium">Registrada</span>
              </div>
              <p class="text-neutral-400">Proveedor: <span class="text-neutral-300">{{ invoice()?.payToName }}</span></p>
            </div>
          </div>
          
          <div class="flex items-center gap-3">
            <button class="px-4 py-2 bg-neutral-800 text-white rounded-lg hover:bg-neutral-700 transition-colors border border-neutral-700 font-medium text-sm flex items-center gap-2">
              <i class="fi fi-rr-print"></i> Imprimir
            </button>
          </div>
        </div>

        <div class="grid grid-cols-3 gap-6 mb-6">
          <div class="bg-neutral-900 border border-neutral-800 rounded-xl p-5 shadow-sm">
            <h3 class="text-sm font-medium text-neutral-400 uppercase tracking-wider mb-4">Información del Proveedor</h3>
            <div class="space-y-3 text-sm">
              <div class="flex flex-col">
                <span class="text-neutral-500">Nº Proveedor</span>
                <span class="text-neutral-200 font-medium">{{ invoice()?.buyFromVendorNo }}</span>
              </div>
              <div class="flex flex-col">
                <span class="text-neutral-500">Nombre</span>
                <span class="text-neutral-200 font-medium">{{ invoice()?.payToName }}</span>
              </div>
            </div>
          </div>

          <div class="bg-neutral-900 border border-neutral-800 rounded-xl p-5 shadow-sm">
            <h3 class="text-sm font-medium text-neutral-400 uppercase tracking-wider mb-4">Detalles de Factura</h3>
            <div class="space-y-3 text-sm">
              <div class="flex flex-col">
                <span class="text-neutral-500">Fecha de Registro</span>
                <span class="text-neutral-200 font-medium">{{ invoice()?.postingDate | date:'longDate' }}</span>
              </div>
              <div class="flex flex-col">
                <span class="text-neutral-500">Términos de Pago</span>
                <span class="text-neutral-200 font-medium">{{ invoice()?.paymentTermsCode || 'N/A' }}</span>
              </div>
            </div>
          </div>

          <div class="bg-gradient-to-br from-teal-900/40 to-blue-900/40 border border-teal-800/30 rounded-xl p-5 shadow-sm flex flex-col justify-center relative overflow-hidden">
            <div class="absolute -right-6 -bottom-6 opacity-10">
              <i class="fi fi-rr-sack-dollar text-8xl"></i>
            </div>
            <h3 class="text-sm font-medium text-teal-400/80 uppercase tracking-wider mb-2">Total Factura</h3>
            <div class="text-4xl font-bold text-white mb-1">
              {{ invoice()?.amountIncludingVat | currency }}
            </div>
            <div class="text-sm text-teal-300/70">
              Subtotal: {{ invoice()?.amount | currency }}
            </div>
          </div>
        </div>

        <div class="bg-neutral-900 border border-neutral-800 rounded-xl overflow-hidden flex-1 shadow-sm">
          <div class="p-4 border-b border-neutral-800 bg-neutral-900/50">
            <h2 class="font-medium text-white flex items-center gap-2">
              <i class="fi fi-rr-list text-teal-500"></i>
              Líneas de Factura
            </h2>
          </div>
          
          <div class="overflow-x-auto">
            <table class="w-full text-left text-sm whitespace-nowrap">
              <thead class="bg-neutral-950 text-neutral-400">
                <tr>
                  <th class="px-6 py-3 font-medium">Descripción</th>
                  <th class="px-6 py-3 font-medium text-right">Cantidad</th>
                  <th class="px-6 py-3 font-medium text-right">Precio Unitario</th>
                  <th class="px-6 py-3 font-medium text-right">Monto</th>
                  <th class="px-6 py-3 font-medium text-right">Impuesto</th>
                  <th class="px-6 py-3 font-medium text-right">Total</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-neutral-800/50">
                <tr *ngFor="let line of invoice()?.lines" class="hover:bg-neutral-800/30 transition-colors">
                  <td class="px-6 py-4 text-neutral-200">{{ line.description }}</td>
                  <td class="px-6 py-4 text-right text-neutral-300">{{ line.quantity }} <span class="text-neutral-500 text-xs ml-1">{{ line.unitOfMeasureCode }}</span></td>
                  <td class="px-6 py-4 text-right text-neutral-300">{{ line.unitCost | currency }}</td>
                  <td class="px-6 py-4 text-right text-neutral-300">{{ line.amount | currency }}</td>
                  <td class="px-6 py-4 text-right text-neutral-400 text-xs">{{ line.vat | currency }}</td>
                  <td class="px-6 py-4 text-right font-medium text-teal-400">{{ line.amountIncludingVat | currency }}</td>
                </tr>
                <tr *ngIf="!invoice()?.lines?.length">
                  <td colspan="6" class="px-6 py-8 text-center text-neutral-500 italic">No hay líneas en esta factura.</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </ng-container>
      
      <!-- Error State -->
      <div *ngIf="!loading() && !invoice()" class="flex-1 flex flex-col items-center justify-center text-center">
        <div class="w-20 h-20 bg-red-500/10 text-red-500 rounded-full flex items-center justify-center mb-4">
          <i class="fi fi-rr-cross-circle text-3xl"></i>
        </div>
        <h2 class="text-xl font-bold text-white mb-2">Factura no encontrada</h2>
        <p class="text-neutral-400 mb-6 max-w-md">La factura de compra que estás buscando no existe o no tienes permisos para verla.</p>
        <button routerLink="/purchase-invoices" class="px-6 py-2.5 bg-neutral-800 text-white rounded-lg hover:bg-neutral-700 transition-colors font-medium">
          Volver a facturas
        </button>
      </div>

    </div>
  `
})
export class PurchaseInvoiceCardPage implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly service = inject(PurchaseInvoiceService);

  readonly invoice = signal<PurchaseInvoiceDetail | null>(null);
  readonly loading = signal(true);

  async ngOnInit() {
    const no = this.route.snapshot.paramMap.get('no');
    if (no) {
      this.invoice.set(await this.service.getByNo(no));
    }
    this.loading.set(false);
  }
}
