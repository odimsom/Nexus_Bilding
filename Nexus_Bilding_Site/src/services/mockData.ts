import { Client, FiscalDocument, Product, DashboardMetrics, RevenueData, User, StockTransaction } from '../types';

export const mockUser: User = {
  id: '1',
  name: 'Seller User',
  email: 'seller@nexusbilling.com',
  role: 'owner',
  createdAt: '2023-01-01T00:00:00Z',
};

export const mockClients: Client[] = [
  {
    id: '1',
    name: 'Acme Studio S.R.L.',
    email: 'billing@acmestudio.com',
    phone: '+1 (809) 555-0123',
    taxId: '131-44930-2',
    isCompany: true,
    address: 'Av. Winston Churchill #45',
    city: 'Santo Domingo',
    status: 'active',
    verifiedAt: '2023-01-15T10:00:00Z',
    notes: 'Cliente B2B recurrente.',
    createdAt: '2023-01-15T10:00:00Z',
  },
  {
    id: '2',
    name: 'Juan Pérez',
    email: 'juan.perez@email.com',
    phone: '+1 (829) 555-4567',
    taxId: '001-0022334-5',
    isCompany: false,
    address: 'Calle Sol #123',
    city: 'Santiago',
    status: 'active',
    createdAt: '2023-02-20T14:30:00Z',
  },
];

export const mockFiscalDocuments: FiscalDocument[] = [
  {
    id: '1',
    documentType: 'E31',
    eCFNumber: 'E310000000001',
    clientId: '1',
    clientName: 'Acme Studio S.R.L.',
    clientTaxId: '131-44930-2',
    issueDate: '2023-10-24T09:00:00Z',
    dueDate: '2023-11-24T09:00:00Z',
    items: [
      {
        productId: '1',
        description: 'Silla Ergonómica',
        quantity: 2,
        unitPrice: 15000,
        itbisRate: 18,
        itbisAmount: 5400,
        total: 35400,
      }
    ],
    subtotal: 30000,
    totalITBIS: 5400,
    totalDiscount: 0,
    totalAmount: 35400,
    status: 'accepted',
    securityCode: 'AB123456...',
    createdAt: '2023-10-24T09:00:00Z',
  },
  {
    id: '2',
    documentType: 'E32',
    clientId: '2',
    clientName: 'Juan Pérez',
    clientTaxId: '001-0022334-5',
    issueDate: '2023-10-22T11:00:00Z',
    items: [
      {
        description: 'Servicio de Consultoría',
        quantity: 1,
        unitPrice: 5000,
        itbisRate: 18,
        itbisAmount: 900,
        total: 5900,
      }
    ],
    subtotal: 5000,
    totalITBIS: 900,
    totalDiscount: 0,
    totalAmount: 5900,
    status: 'draft',
    createdAt: '2023-10-22T11:00:00Z',
  },
];

export const mockProducts: Product[] = [
  {
    id: '1',
    name: 'Silla Ergonómica',
    description: 'Silla de oficina con soporte lumbar.',
    sku: 'FUR-OFF-004',
    category: 'Mobiliario',
    price: 15000,
    cost: 8500,
    itbisRate: 18,
    stock: 42,
    lowStockThreshold: 10,
    status: 'active',
    createdAt: '2023-01-10T08:00:00Z',
  },
  {
    id: '2',
    name: 'Servicio Consultoría IT',
    category: 'Servicios',
    price: 3500,
    itbisRate: 18,
    status: 'active',
    createdAt: '2023-01-11T09:00:00Z',
  },
];

export const mockDashboardMetrics: DashboardMetrics = {
  totalSales: 154500,
  accountsReceivable: {
    over30Days: 12000,
    over60Days: 5000,
    over90Days: 0,
  },
  projectedITBIS: 27810,
  activeClients: 14,
  lowStockItems: 3,
};

export const mockRevenueData: RevenueData[] = [
  { month: 'Jan', revenue: 85000 },
  { month: 'Feb', revenue: 92000 },
  { month: 'Mar', revenue: 78000 },
  { month: 'Apr', revenue: 115000 },
  { month: 'May', revenue: 98000 },
  { month: 'Jun', revenue: 130000 },
];

export const mockStockTransactions: StockTransaction[] = [
  {
    id: '1',
    productId: '1',
    type: 'purchase',
    quantity: 50,
    balanceAfter: 50,
    reference: 'PO-001',
    createdAt: '2023-10-24T08:00:00Z',
  },
];
