import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ApiEnvelope<T> {
  success: boolean;
  data: T;
  error?: { code: string; message: string; details?: { field: string; message: string }[] };
  meta: { requestId: string; timestamp: string };
}

export interface PagedData<T> {
  items: T[];
  pagination: { page: number; pageSize: number; totalItems: number; totalPages: number };
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  private readonly http = inject(HttpClient);
  readonly base = `${environment.apiUrl}/v1`;

  get<T>(path: string, params?: Record<string, string | number | boolean | undefined>): Observable<T> {
    let httpParams = new HttpParams();
    if (params) {
      for (const [k, v] of Object.entries(params)) {
        if (v !== undefined && v !== null && v !== '') {
          httpParams = httpParams.set(k, String(v));
        }
      }
    }
    return this.http.get<ApiEnvelope<T>>(`${this.base}/${path}`, { params: httpParams }).pipe(
      map(r => r.data)
    );
  }

  post<T>(path: string, body: unknown): Observable<T> {
    return this.http.post<ApiEnvelope<T>>(`${this.base}/${path}`, body).pipe(
      map(r => r.data)
    );
  }

  put<T>(path: string, body: unknown): Observable<T> {
    return this.http.put<ApiEnvelope<T>>(`${this.base}/${path}`, body).pipe(
      map(r => r.data)
    );
  }

  patch<T>(path: string, body?: unknown): Observable<T> {
    return this.http.patch<ApiEnvelope<T>>(`${this.base}/${path}`, body ?? {}).pipe(
      map(r => r.data)
    );
  }

  delete<T>(path: string): Observable<T> {
    return this.http.delete<ApiEnvelope<T>>(`${this.base}/${path}`).pipe(
      map(r => r.data)
    );
  }
}
