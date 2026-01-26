import { apiClient, responseBody, ApiResponse } from './httpClient';

export const paymentService = {
      getAll: async () => apiClient.get<ApiResponse<any[]>>('/Payment').then(responseBody),
      create: async (payment: any) => apiClient.post<ApiResponse<string>>('/Payment', payment).then(responseBody),
};
