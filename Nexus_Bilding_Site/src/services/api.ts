import {
  mockUser,
  mockClients,
  mockFiscalDocuments,
  mockProducts,
  mockDashboardMetrics,
  mockRevenueData,
  mockStockTransactions,
} from './mockData';

const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

export const api = {
  auth: {
    login: async (email: string, password: string) => {
      await delay(500);
      if (email && password) {
        return { user: mockUser, token: 'mock-token-123' };
      }
      throw new Error('Invalid credentials');
    },

    register: async (name: string, email: string, password: string) => {
      await delay(500);
      return { user: { ...mockUser, name, email }, token: 'mock-token-123' };
    },
  },

  dashboard: {
    getMetrics: async () => {
      await delay(300);
      return mockDashboardMetrics;
    },

    getRevenueData: async () => {
      await delay(300);
      return mockRevenueData;
    },
  },

  clients: {
    getAll: async () => {
      await delay(300);
      return mockClients;
    },

    getById: async (id: string) => {
      await delay(300);
      return mockClients.find(c => c.id === id);
    },
  },

  // Renamed from invoices to documents to reflect FiscalDocument type
  documents: {
    getAll: async () => {
      await delay(300);
      return mockFiscalDocuments;
    },

    getById: async (id: string) => {
      await delay(300);
      return mockFiscalDocuments.find(d => d.id === id);
    },

    getRecent: async (limit: number = 5) => {
      await delay(300);
      return mockFiscalDocuments.slice(0, limit);
    },
  },

  products: {
    getAll: async () => {
      await delay(300);
      return mockProducts;
    },

    getById: async (id: string) => {
      await delay(300);
      return mockProducts.find(p => p.id === id);
    },

    getStockTransactions: async (productId: string) => {
      await delay(300);
      return mockStockTransactions.filter(t => t.productId === productId);
    },
  },
};
