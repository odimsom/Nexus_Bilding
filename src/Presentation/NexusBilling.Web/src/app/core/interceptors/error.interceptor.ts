import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if ([401, 403].includes(error.status)) {
        // En caso de 401 el JwtInterceptor ya intentó el refresh.
        // Si llega aquí es que falló el refresh o es 403.
        authService.logout();
      }

      const errorMessage = error.error?.error || error.statusText;
      console.error('API Error:', errorMessage);
      return throwError(() => error);
    })
  );
};
