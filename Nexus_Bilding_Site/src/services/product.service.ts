import { apiClient, responseBody, ApiResponse } from './httpClient';
import { Product } from '../types/product/Product';

export const productService = {
  getAll: async () => apiClient.get<ApiResponse<Product[]>>('/Product').then(responseBody),

  getById: async (id: string) => apiClient.get<ApiResponse<Product>>(`/Product/${id}`).then(responseBody),
  
  create: async (product: any) => apiClient.post<ApiResponse<string>>('/Product', product).then(responseBody),
};
