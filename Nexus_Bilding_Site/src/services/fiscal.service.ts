import { apiClient, responseBody, ApiResponse } from './httpClient';
import { FiscalDocument } from '../types/fiscal/FiscalDocument';

export const fiscalService = {
    getAll: async () => apiClient.get<ApiResponse<FiscalDocument[]>>('/Fiscal').then(responseBody),

    getById: async (id: string) => apiClient.get<ApiResponse<FiscalDocument>>(`/Fiscal/${id}`).then(responseBody),

    create: async (doc: any) => apiClient.post<ApiResponse<string>>('/Fiscal', doc).then(responseBody),

    getRecent: async (limit: number = 5) => {
       const data = await apiClient.get<ApiResponse<FiscalDocument[]>>('/Fiscal').then(responseBody);
       return data.slice(0, limit);
    },
};
