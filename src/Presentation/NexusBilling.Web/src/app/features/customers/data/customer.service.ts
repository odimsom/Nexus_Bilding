import { Injectable, signal, computed } from '@angular/core';
import { Customer, CustomerFilter, CustomerSortField } from '../domain/customer.model';

/** Mock data service — replace with HttpClient calls once API endpoints exist */
@Injectable({ providedIn: 'root' })
export class CustomerService {

  private readonly _customers = signal<Customer[]>(MOCK_CUSTOMERS);
  private readonly _loading = signal(false);
  private readonly _error = signal<string | null>(null);

  readonly loading = this._loading.asReadonly();
  readonly error = this._error.asReadonly();

  getAll(): Customer[] {
    return this._customers();
  }

  getByNo(no: string): Customer | undefined {
    return this._customers().find(c => c.no === no);
  }

  filter(f: CustomerFilter, sortField: CustomerSortField = 'name', sortAsc = true): Customer[] {
    let result = this._customers();

    if (f.search) {
      const q = f.search.toLowerCase();
      result = result.filter(c =>
        c.no.toLowerCase().includes(q) ||
        c.name.toLowerCase().includes(q) ||
        c.city?.toLowerCase().includes(q) ||
        c.contact?.toLowerCase().includes(q)
      );
    }
    if (f.blocked !== undefined && f.blocked !== null) {
      result = result.filter(c => c.blocked === f.blocked);
    }
    if (f.city) result = result.filter(c => c.city === f.city);
    if (f.salespersonCode) result = result.filter(c => c.salespersonCode === f.salespersonCode);

    result = [...result].sort((a, b) => {
      let av: string | number = '';
      let bv: string | number = '';
      switch (sortField) {
        case 'no':         av = a.no; bv = b.no; break;
        case 'name':       av = a.name; bv = b.name; break;
        case 'city':       av = a.city ?? ''; bv = b.city ?? ''; break;
        case 'balance':    av = a.balance ?? 0; bv = b.balance ?? 0; break;
        case 'balanceDue': av = a.balanceDue ?? 0; bv = b.balanceDue ?? 0; break;
      }
      if (typeof av === 'string') return sortAsc ? av.localeCompare(bv as string) : (bv as string).localeCompare(av);
      return sortAsc ? av - (bv as number) : (bv as number) - av;
    });

    return result;
  }

  /** Unique cities for filter dropdown */
  cities(): string[] {
    return [...new Set(this._customers().map(c => c.city).filter(Boolean) as string[])].sort();
  }
}

/* ─── MOCK DATA ─────────────────────────────────────────────────────────── */
const MOCK_CUSTOMERS: Customer[] = [
  {
    no: 'C-00001',
    name: 'Altagracia Comercial S.R.L.',
    address: 'Av. Winston Churchill 1099',
    city: 'Santo Domingo',
    contact: 'Juana Pérez',
    blocked: false,
    phoneNo: '809-555-1001',
    email: 'jperez@altagracia.com.do',
    creditLimit: 500000,
    balance: 148500,
    balanceDue: 0,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '30 DIAS',
    salespersonCode: 'RMT',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00002',
    name: 'Distribuidora Los Alcarrizos',
    address: 'C/ Principal #45, Los Alcarrizos',
    city: 'Santo Domingo Oeste',
    contact: 'Carlos Méndez',
    blocked: false,
    phoneNo: '829-555-2020',
    email: 'cmendez@distralcarrizos.com',
    creditLimit: 250000,
    balance: 75200,
    balanceDue: 75200,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '15 DIAS',
    salespersonCode: 'RMT',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00003',
    name: 'Ferretería El Progreso',
    address: 'C/ Duarte #12',
    city: 'Santiago',
    contact: 'Ana Rodríguez',
    blocked: false,
    phoneNo: '809-555-3300',
    email: 'arodriguez@elprogreso.com',
    creditLimit: 150000,
    balance: 22800,
    balanceDue: 22800,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: 'CONTADO',
    salespersonCode: 'MAV',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00004',
    name: 'Supermercado Bravo',
    address: 'Autopista 6 de Noviembre Km 9',
    city: 'Santo Domingo',
    contact: 'Luis Bravo',
    blocked: false,
    phoneNo: '849-555-4040',
    email: 'lbravo@supermercadobravo.com',
    creditLimit: 1000000,
    balance: 310750,
    balanceDue: 100000,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '30 DIAS',
    salespersonCode: 'RMT',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00005',
    name: 'Grupo Estrella',
    address: 'Av. Tiradentes #14',
    city: 'Santo Domingo',
    contact: 'María Estrella',
    blocked: false,
    phoneNo: '809-555-5050',
    email: 'mestrella@grupoestrella.com.do',
    creditLimit: 2000000,
    balance: 890000,
    balanceDue: 0,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '60 DIAS',
    salespersonCode: 'MAV',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00006',
    name: 'Importadora Caribbean Tech',
    address: 'Zona Franca Industrial, Itabo',
    city: 'Haina',
    contact: 'Roberto Jiménez',
    blocked: false,
    phoneNo: '809-555-6060',
    email: 'rjimenez@caribbeantech.com',
    creditLimit: 500000,
    balance: 45000,
    balanceDue: 0,
    customerPostingGroup: 'FOREIGN',
    paymentTermsCode: 'NET 45',
    salespersonCode: 'MAV',
    currencyCode: 'USD',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00007',
    name: 'Farmacia El Alivio',
    address: 'C/ Sánchez #89',
    city: 'La Romana',
    contact: 'Elena Vargas',
    blocked: true,
    phoneNo: '809-555-7070',
    email: 'evargas@elalivio.com',
    creditLimit: 50000,
    balance: 18200,
    balanceDue: 18200,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '30 DIAS',
    salespersonCode: 'RMT',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00008',
    name: 'Constructora NovaBuild S.A.',
    address: 'Av. 27 de Febrero #324, Torre Empresarial',
    city: 'Santo Domingo',
    contact: 'Andrés Nova',
    blocked: false,
    phoneNo: '829-555-8080',
    email: 'anova@novabuild.do',
    creditLimit: 3000000,
    balance: 1250000,
    balanceDue: 250000,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: '90 DIAS',
    salespersonCode: 'MAV',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00009',
    name: 'Gasolinera San Miguel',
    address: 'Autopista Duarte Km 14',
    city: 'Bonao',
    contact: 'Francisco Torres',
    blocked: false,
    phoneNo: '809-555-9090',
    email: 'ftorres@sanmiguel.com',
    creditLimit: 80000,
    balance: 12500,
    balanceDue: 0,
    customerPostingGroup: 'DOMESTIC',
    paymentTermsCode: 'CONTADO',
    salespersonCode: 'RMT',
    currencyCode: '',
    countryRegionCode: 'DO',
  },
  {
    no: 'C-00010',
    name: 'Hotel Bahía Samana',
    address: 'Malecón Principal s/n',
    city: 'Samaná',
    contact: 'Isabela Castro',
    blocked: false,
    phoneNo: '809-555-1010',
    email: 'icastro@bahiasamana.com',
    creditLimit: 400000,
    balance: 95000,
    balanceDue: 0,
    customerPostingGroup: 'FOREIGN',
    paymentTermsCode: 'NET 30',
    salespersonCode: 'MAV',
    currencyCode: 'USD',
    countryRegionCode: 'DO',
  },
];
