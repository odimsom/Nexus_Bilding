import { Injectable, signal } from '@angular/core';
import { Item, ItemFilter, ItemSortField } from '../domain/item.model';

@Injectable({ providedIn: 'root' })
export class ItemService {

  private readonly _items = signal<Item[]>(MOCK_ITEMS);

  getAll(): Item[] {
    return this._items();
  }

  getByNo(no: string): Item | undefined {
    return this._items().find(i => i.no === no);
  }

  filter(f: ItemFilter, sortField: ItemSortField = 'no', sortAsc = true): Item[] {
    let result = this._items();

    if (f.search) {
      const q = f.search.toLowerCase();
      result = result.filter(i =>
        i.no.toLowerCase().includes(q) ||
        i.description.toLowerCase().includes(q) ||
        i.description2?.toLowerCase().includes(q)
      );
    }
    if (f.blocked !== undefined && f.blocked !== null) {
      result = result.filter(i => i.blocked === f.blocked);
    }
    if (f.type) result = result.filter(i => i.type === f.type);
    if (f.itemCategoryCode) result = result.filter(i => i.itemCategoryCode === f.itemCategoryCode);

    return [...result].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (sortField) {
        case 'no':          av = a.no; bv = b.no; break;
        case 'description': av = a.description; bv = b.description; break;
        case 'unitPrice':   av = a.unitPrice; bv = b.unitPrice; break;
        case 'unitCost':    av = a.unitCost; bv = b.unitCost; break;
        case 'inventory':   av = a.inventory ?? 0; bv = b.inventory ?? 0; break;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });
  }

  categories(): string[] {
    return [...new Set(this._items().map(i => i.itemCategoryCode).filter(Boolean) as string[])].sort();
  }
}

/* ─── MOCK DATA ──────────────────────────────────────────────────────── */
const MOCK_ITEMS: Item[] = [
  { no: 'ART-0001', description: 'Bloque de Concreto 6"', baseUnitOfMeasure: 'UND', unitPrice: 28.50, unitCost: 18.00, blocked: false, type: 'Inventory', itemCategoryCode: 'CONSTRUCCION', inventory: 15000, qtyOnSalesOrder: 500, qtyOnPurchOrder: 0 },
  { no: 'ART-0002', description: 'Bloque de Concreto 8"', baseUnitOfMeasure: 'UND', unitPrice: 35.00, unitCost: 22.00, blocked: false, type: 'Inventory', itemCategoryCode: 'CONSTRUCCION', inventory: 8500, qtyOnSalesOrder: 1200, qtyOnPurchOrder: 2000 },
  { no: 'ART-0012', description: 'Cemento Portland 42.5 kg', baseUnitOfMeasure: 'SAC', unitPrice: 420.00, unitCost: 310.00, blocked: false, type: 'Inventory', itemCategoryCode: 'CONSTRUCCION', inventory: 2300, qtyOnSalesOrder: 200, qtyOnPurchOrder: 500 },
  { no: 'ART-0045', description: 'Varilla de Hierro 3/8"', description2: 'Quintal 100 libras', baseUnitOfMeasure: 'QQ', unitPrice: 875.00, unitCost: 680.00, blocked: false, type: 'Inventory', itemCategoryCode: 'ACERO', inventory: 850, qtyOnSalesOrder: 50, qtyOnPurchOrder: 100 },
  { no: 'ART-0046', description: 'Varilla de Hierro 1/2"', description2: 'Quintal 100 libras', baseUnitOfMeasure: 'QQ', unitPrice: 920.00, unitCost: 720.00, blocked: false, type: 'Inventory', itemCategoryCode: 'ACERO', inventory: 620, qtyOnSalesOrder: 80, qtyOnPurchOrder: 0 },
  { no: 'ART-0089', description: 'Aceite Motor 20W50 1L', baseUnitOfMeasure: 'UND', unitPrice: 385.00, unitCost: 290.00, blocked: false, type: 'Inventory', itemCategoryCode: 'LUBRICANTES', inventory: 480, qtyOnSalesOrder: 120, qtyOnPurchOrder: 200 },
  { no: 'ART-0090', description: 'Filtro Aceite Universal', baseUnitOfMeasure: 'UND', unitPrice: 280.00, unitCost: 195.00, blocked: false, type: 'Inventory', itemCategoryCode: 'AUTOMOTRIZ', inventory: 320, qtyOnSalesOrder: 60, qtyOnPurchOrder: 0 },
  { no: 'ART-0120', description: 'Cable Eléctrico AWG 12 (100m)', baseUnitOfMeasure: 'ROL', unitPrice: 2850.00, unitCost: 2100.00, blocked: false, type: 'Inventory', itemCategoryCode: 'ELECTRICO', inventory: 145, qtyOnSalesOrder: 20, qtyOnPurchOrder: 50 },
  { no: 'ART-0121', description: 'Tomacorriente Doble Polarizado', baseUnitOfMeasure: 'UND', unitPrice: 185.00, unitCost: 120.00, blocked: false, type: 'Inventory', itemCategoryCode: 'ELECTRICO', inventory: 850, qtyOnSalesOrder: 0, qtyOnPurchOrder: 0 },
  { no: 'ART-0200', description: 'Pintura Acrílica Blanca 1 galón', baseUnitOfMeasure: 'GAL', unitPrice: 650.00, unitCost: 480.00, blocked: false, type: 'Inventory', itemCategoryCode: 'PINTURA', inventory: 380, qtyOnSalesOrder: 40, qtyOnPurchOrder: 100 },
  { no: 'ART-0201', description: 'Pintura Acrílica Colores 1 galón', baseUnitOfMeasure: 'GAL', unitPrice: 720.00, unitCost: 530.00, blocked: false, type: 'Inventory', itemCategoryCode: 'PINTURA', inventory: 210, qtyOnSalesOrder: 0, qtyOnPurchOrder: 0 },
  { no: 'ART-0300', description: 'Tubo PVC Sanitario 4" x 6m', baseUnitOfMeasure: 'UND', unitPrice: 480.00, unitCost: 355.00, blocked: false, type: 'Inventory', itemCategoryCode: 'PLOMERIA', inventory: 290, qtyOnSalesOrder: 30, qtyOnPurchOrder: 60 },
  { no: 'SRV-0001', description: 'Servicio de Instalación Eléctrica', baseUnitOfMeasure: 'HRS', unitPrice: 1200.00, unitCost: 800.00, blocked: false, type: 'Service', itemCategoryCode: 'SERVICIOS', inventory: 0 },
  { no: 'SRV-0002', description: 'Servicio de Consultoría Técnica', baseUnitOfMeasure: 'HRS', unitPrice: 2500.00, unitCost: 0, blocked: false, type: 'Service', itemCategoryCode: 'SERVICIOS', inventory: 0 },
  { no: 'ART-9999', description: 'Artículo Descontinuado', baseUnitOfMeasure: 'UND', unitPrice: 0, unitCost: 0, blocked: true, type: 'Inventory', itemCategoryCode: 'CONSTRUCCION', inventory: 5 },
];
