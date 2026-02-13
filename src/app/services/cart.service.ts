import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../models/product.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/cart`;
  private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);

  constructor(private http: HttpClient) {}

  refreshCart(): void {
    this.http.get<CartItem[]>(this.apiUrl).subscribe(items => {
      this.cartItemsSubject.next(items);
    });
  }

  getCart(): Observable<CartItem[]> {
    return this.cartItemsSubject.asObservable();
  }

  addToCart(productId: number, quantity: number = 1): void {
    this.http.post<CartItem[]>(this.apiUrl, { productId, quantity }).subscribe(items => {
      this.cartItemsSubject.next(items);
    });
  }

  removeFromCart(productId: number): void {
    this.http.delete<CartItem[]>(`${this.apiUrl}/${productId}`).subscribe(items => {
      this.cartItemsSubject.next(items);
    });
  }

  updateQuantity(productId: number, quantity: number): void {
    this.http.put<CartItem[]>(`${this.apiUrl}/${productId}`, { quantity }).subscribe(items => {
      this.cartItemsSubject.next(items);
    });
  }

  clearCart(): void {
    this.http.delete<CartItem[]>(this.apiUrl).subscribe(items => {
      this.cartItemsSubject.next(items);
    });
  }

  getTotal(): number {
    return this.cartItemsSubject.value.reduce(
      (total, item) => total + (item.product.price * item.quantity), 0
    );
  }

  getItemCount(): number {
    return this.cartItemsSubject.value.reduce(
      (count, item) => count + item.quantity, 0
    );
  }
}
