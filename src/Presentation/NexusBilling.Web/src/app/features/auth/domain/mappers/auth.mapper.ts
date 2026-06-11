import { LoginResponse } from '../../data/models/auth.models';
import { AuthUser } from '../entities/auth-user.entity';

export class AuthMapper {
  static toEntity(response: LoginResponse): AuthUser {
    return {
      username: response.username,
      email: response.email,
      tenantId: response.tenantId,
      roles: [] // Mapear desde claims si fuera necesario
    };
  }
}
