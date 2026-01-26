import { apiClient, responseBody, ApiResponse } from './httpClient';
import { StockTransaction } from '../types/stock/StockTransaction';

export const stockService = {
    getByProductId: async (productId: string) => {
       const allStocks = await apiClient.get<ApiResponse<StockTransaction[]>>('/Stock').then(responseBody);
       return allStocks.filter(s => s.productId === productId);
    },
    
    getAll: async () => apiClient.get<ApiResponse<StockTransaction[]>>('/Stock').then(responseBody),
};
