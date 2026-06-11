import { inject, Injectable } from '@angular/core';
import { AuthRepository } from '../../features/auth/data/repositories/auth.repository';
import { AuthState } from '../../features/auth/presentation/state/auth.state';
import { LoginRequest } from '../../features/auth/data/models/auth.models';
import { tap, catchError, of, Observable } from 'rxjs';
import { AuthMapper } from '../../features/auth/domain/mappers/auth.mapper';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly repository = inject(AuthRepository);
  private readonly state = inject(AuthState);
  private readonly router = inject(Router);

  readonly user = this.state.user;
  readonly isAuthenticated = this.state.isAuthenticated;
  readonly isLoading = this.state.isLoading;
  readonly error = this.state.error;

  login(request: LoginRequest): Observable<any> {
    this.state.setLoading(true);
    return this.repository.login(request).pipe(
      tap(response => {
        const user = AuthMapper.toEntity(response);
        this.state.setUser(user);
        this.state.setTokens(response.token, response.refreshToken);
        this.state.setLoading(false);
        this.router.navigate(['/dashboard']);
      }),
      catchError(err => {
        this.state.setError(err.error?.error || 'Login failed');
        this.state.setLoading(false);
        return of(null);
      })
    );
  }

  logout(): void {
    const refreshToken = this.state.refreshToken();
    if (refreshToken) {
      this.repository.logout(refreshToken).subscribe();
    }
    this.state.clear();
    this.router.navigate(['/login']);
  }

  refresh(): Observable<any> {
    const token = this.state.token();
    const refreshToken = this.state.refreshToken();

    if (!token || !refreshToken) {
      this.logout();
      return of(null);
    }

    return this.repository.refresh({ token, refreshToken }).pipe(
      tap(response => {
        this.state.setTokens(response.token, response.refreshToken);
      }),
      catchError(() => {
        this.logout();
        return of(null);
      })
    );
  }
}
