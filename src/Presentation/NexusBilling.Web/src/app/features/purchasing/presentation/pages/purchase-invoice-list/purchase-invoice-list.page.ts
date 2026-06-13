import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PurchaseInvoiceService } from '../../../data/purchase-invoice.service';

@Component({
  selector: 'app-purchase-invoice-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="h-full flex flex-col p-6 animate-fade-in">
      <div class="flex justify-between items-center mb-6">
        <div>
          <h1 class="text-3xl font-bold bg-clip-text text-transparent bg-gradient-to-r from-teal-400 to-blue-500">
            Facturas de Compra
          </h1>
          <p class="text-neutral-400 mt-1">Historial de facturas recibidas de proveedores.</p>
        </div>
      </div>

      <div class="bg-neutral-900 border border-neutral-800 rounded-xl overflow-hidden flex-1 flex flex-col shadow-xl">
        <div class="p-4 border-b border-neutral-800 flex justify-between items-center bg-neutral-900/50">
          <div class="relative w-72">
            <i class="fi fi-rr-search absolute left-3 top-1/2 -translate-y-1/2 text-neutral-500"></i>
            <input type="text" placeholder="Buscar facturas..." 
                   class="w-full bg-neutral-950 border border-neutral-800 rounded-lg pl-10 pr-4 py-2 text-sm focus:outline-none focus:border-teal-500 focus:ring-1 focus:ring-teal-500 transition-all text-neutral-200">
          </div>
          <div class="text-sm text-neutral-400">
            {{ service.totalItems() }} facturas en total
          </div>
        </div>

        <div class="flex-1 overflow-auto">
          <table class="w-full text-left text-sm whitespace-nowrap">
            <thead class="bg-neutral-950/50 text-neutral-400 sticky top-0 z-10 backdrop-blur-sm">
              <tr>
                <th class="px-6 py-4 font-medium">Nº Factura</th>
                <th class="px-6 py-4 font-medium">Proveedor</th>
                <th class="px-6 py-4 font-medium">Nombre Proveedor</th>
                <th class="px-6 py-4 font-medium">Fecha Emisión</th>
                <th class="px-6 py-4 font-medium">Estado</th>
                <th class="px-6 py-4 font-medium text-right">Importe Total</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-neutral-800/50">
              <tr *ngIf="service.loading()">
                <td colspan="6" class="px-6 py-8 text-center text-neutral-500">
                  <i class="fi fi-rr-spinner animate-spin text-2xl mb-2 inline-block"></i>
                  <p>Cargando facturas...</p>
                </td>
              </tr>
              
              <tr *ngIf="!service.loading() && service.items().length === 0">
                <td colspan="6" class="px-6 py-12 text-center text-neutral-500">
                  <i class="fi fi-rr-document text-4xl mb-3 inline-block opacity-50"></i>
                  <p class="text-lg">No hay facturas registradas</p>
                  <p class="text-sm mt-1">Las órdenes de compra registradas aparecerán aquí.</p>
                </td>
              </tr>

              <tr *ngFor="let item of service.items()" 
                  [routerLink]="['/purchase-invoices', item.no]"
                  class="hover:bg-neutral-800/30 cursor-pointer transition-colors group">
                <td class="px-6 py-4 font-medium text-teal-400 group-hover:text-teal-300">
                  {{ item.no }}
                </td>
                <td class="px-6 py-4 text-neutral-300">{{ item.buyFromVendorNo }}</td>
                <td class="px-6 py-4 text-neutral-300">{{ item.payToName }}</td>
                <td class="px-6 py-4 text-neutral-400">{{ item.postingDate | date:'dd/MM/yyyy' }}</td>
                <td class="px-6 py-4">
                  <span class="px-2.5 py-1 bg-blue-500/10 text-blue-400 border border-blue-500/20 rounded-full text-xs font-medium">
                    Registrada
                  </span>
                </td>
                <td class="px-6 py-4 text-right font-medium text-neutral-200">
                  {{ item.amountIncludingVat | currency }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `
})
export class PurchaseInvoiceListPage implements OnInit {
  service = inject(PurchaseInvoiceService);

  ngOnInit() {
    this.service.load();
  }
}
