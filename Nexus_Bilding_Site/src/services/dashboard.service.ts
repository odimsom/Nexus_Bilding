import { DashboardMetrics } from '../types/dashboard/DashboardMetrics';
import { RevenueData } from '../types/dashboard/RevenueData';

// Mocks for Dashboard (Backend implementation pending)
const mockDashboardMetrics: DashboardMetrics = {
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

const mockRevenueData: RevenueData[] = [
  { month: 'Jan', revenue: 85000 },
  { month: 'Feb', revenue: 92000 },
  { month: 'Mar', revenue: 78000 },
  { month: 'Apr', revenue: 115000 },
  { month: 'May', revenue: 98000 },
  { month: 'Jun', revenue: 130000 },
];

export const dashboardService = {
    getMetrics: async () => {
      await new Promise(resolve => setTimeout(resolve, 300));
      return mockDashboardMetrics;
    },

    getRevenueData: async () => {
      await new Promise(resolve => setTimeout(resolve, 300));
      return mockRevenueData;
    },
};
