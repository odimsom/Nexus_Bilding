import { signal, computed, Injectable } from '@angular/core';
import { AuthUser } from '../domain/entities/auth-user.entity';

@Injectable({
  providedIn: 'root'
})
export class AuthState {
  private readonly _user = signal<AuthUser | null>(null);
  private readonly _token = signal<string | null>(null);
  private readonly _refreshToken = signal<string | null>(null);
  private readonly _isLoading = signal<boolean>(false);
  private readonly _error = signal<string | null>(null);

  // Selectors
  readonly user = computed(() => this._user());
  readonly isAuthenticated = computed(() => !!this._token());
  readonly isLoading = computed(() => this._isLoading());
  readonly error = computed(() => this._error());
  readonly token = computed(() => this._token());
  readonly refreshToken = computed(() => this._refreshToken());

  // Actions
  setUser(user: AuthUser | null) {
    this._user.set(user);
  }

  setTokens(token: string | null, refreshToken: string | null) {
    this._token.set(token);
    this._refreshToken.set(refreshToken);
    if (token) {
      localStorage.setItem('accessToken', token);
      localStorage.setItem('refreshToken', refreshToken || '');
    } else {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
    }
  }

  setLoading(isLoading: boolean) {
    this._isLoading.set(isLoading);
  }

  setError(error: string | null) {
    this._error.set(error);
  }

  clear() {
    this._user.set(null);
    this._token.set(null);
    this._refreshToken.set(null);
    this._error.set(null);
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  initialize() {
    const token = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');
    if (token && refreshToken) {
      this._token.set(token);
      this._refreshToken.set(refreshToken);
    }
  }
}
