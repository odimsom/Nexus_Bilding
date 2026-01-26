import { apiClient, ApiResponse } from './httpClient';
import { LoginResponse } from '../types/auth';

export const authService = {
  login: async (email: string, password: string) => {
    const response = await apiClient.post<ApiResponse<LoginResponse>>('/Auth/authenticate', { email, password });
    if (response.data.succeeded) {
        localStorage.setItem('token', response.data.data.jwToken);
        localStorage.setItem('user', JSON.stringify(response.data.data));
        return { user: response.data.data, token: response.data.data.jwToken };
    }
    throw new Error(response.data.message || 'Login failed');
  },

  register: async (name: string, email: string, password: string, confirmPassword: string) => {
       const response = await apiClient.post<ApiResponse<string>>('/Auth/register', { 
           userName: name, 
           email, 
           password, 
           confirmPassword 
       });
       return response.data;
  },

  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  },

  confirmEmail: async (userId: string, code: string) => {
      const response = await apiClient.get<ApiResponse<string>>(`/Auth/confirm-email?userId=${userId}&code=${encodeURIComponent(code)}`);
      return response.data;
  }
};
