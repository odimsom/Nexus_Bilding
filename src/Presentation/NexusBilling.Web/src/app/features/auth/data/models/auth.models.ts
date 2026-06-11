export interface LoginRequest {
  email: string;
  password: string;
}

export interface TokenResponse {
  token: string;
  refreshToken: string;
  expiresAt: string;
  username: string;
  email: string;
  tenantId: string;
}

export interface CurrentUserDto {
  id: string;
  username: string;
  email: string;
  tenantId: string;
  isActive: boolean;
}
