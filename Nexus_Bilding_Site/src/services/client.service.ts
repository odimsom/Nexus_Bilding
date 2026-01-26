import { apiClient, responseBody, ApiResponse } from './httpClient';
import { Client } from '../types/client/Client';

export const clientService = {
  getAll: async () => apiClient.get<ApiResponse<Client[]>>('/Client').then(responseBody),

  getById: async (id: string) => apiClient.get<ApiResponse<Client>>(`/Client/${id}`).then(responseBody),
  
  create: async (client: any) => apiClient.post<ApiResponse<string>>('/Client', client).then(responseBody),
  
  update: async (id: string, client: any) => apiClient.put<ApiResponse<string>>(`/Client/${id}`, client).then(responseBody),
  
  delete: async (id: string) => apiClient.delete<ApiResponse<string>>(`/Client/${id}`).then(responseBody),
};
