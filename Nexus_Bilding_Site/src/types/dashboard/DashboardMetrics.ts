export interface DashboardMetrics {
  totalSales: number;

  accountsReceivable: {
    over30Days: number;
    over60Days: number;
    over90Days: number;
  };

  projectedITBIS: number; // ITBIS a pagar próximo mes

  activeClients: number;
  lowStockItems?: number;
}
