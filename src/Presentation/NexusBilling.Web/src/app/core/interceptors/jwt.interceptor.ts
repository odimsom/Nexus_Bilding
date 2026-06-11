import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthState } from '../../features/auth/presentation/state/auth.state';
import { catchError, switchMap, throwError, Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  const state = inject(AuthState);
  const authService = inject(AuthService);
  const token = state.token();

  let authReq = req;
  if (token) {
    authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && token) {
        return authService.refresh().pipe(
          switchMap(response => {
            if (response) {
              return next(req.clone({
                setHeaders: { Authorization: `Bearer ${response.token}` }
              }));
            }
            return throwError(() => error);
          })
        );
      }
      return throwError(() => error);
    })
  );
};
