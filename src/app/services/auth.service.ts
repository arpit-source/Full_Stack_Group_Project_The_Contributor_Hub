import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, map, catchError, of } from 'rxjs';
import { User, LoginCredentials, RegisterData } from '../models/user.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient, private router: Router) {}

  register(data: RegisterData): Observable<boolean> {
    return this.http.post(`${this.apiUrl}/register`, data).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

  login(credentials: LoginCredentials): Observable<boolean> {
    return this.http.post<User>(`${this.apiUrl}/login`, credentials).pipe(
      map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        return true;
      }),
      catchError(() => of(false))
    );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('currentUser');
  }

  getCurrentUser(): User | null {
    const userStr = localStorage.getItem('currentUser');
    return userStr ? JSON.parse(userStr) : null;
  }
}
