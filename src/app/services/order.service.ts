import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, PaymentInfo } from '../models/order.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = `${environment.apiUrl}/orders`;

  constructor(private http: HttpClient) {}

  createOrder(paymentInfo: PaymentInfo, shippingAddress: string): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, { paymentInfo, shippingAddress });
  }

  getUserOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }

  getOrderById(orderId: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`);
  }
}
