export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  refreshToken: string;
  expiresAt: string;
  username: string;
  email: string;
  tenantId: string;
}

export interface RefreshRequest {
  token: string;
  refreshToken: string;
}

export interface CurrentUserDto {
  id: string;
  username: string;
  email: string;
  tenantId: string;
  isActive: boolean;
}
