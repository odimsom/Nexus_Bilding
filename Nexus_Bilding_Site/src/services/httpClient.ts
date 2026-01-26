import axios, { AxiosResponse } from 'axios';

const API_URL = 'http://localhost:5231/api/v1';

export const apiClient = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export interface ApiResponse<T> {
  succeeded: boolean;
  message?: string;
  errors?: string[];
  data: T;
}

export const responseBody = <T>(response: AxiosResponse<ApiResponse<T>>) => response.data.data;
