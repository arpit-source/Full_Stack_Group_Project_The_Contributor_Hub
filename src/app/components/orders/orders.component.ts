import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { Order, OrderStatus } from '../../models/order.model';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent implements OnInit, OnDestroy {
  orders: Order[] = [];
  selectedOrder: Order | null = null;
  private refreshInterval: any;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadOrders();
    this.refreshInterval = setInterval(() => {
      this.loadOrders();
    }, 5000);
  }

  ngOnDestroy(): void {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
    }
  }

  loadOrders(): void {
    this.orderService.getUserOrders().subscribe(orders => {
      this.orders = orders;
      if (this.selectedOrder) {
        const updated = orders.find(o => o.id === this.selectedOrder!.id);
        if (updated) {
          this.selectedOrder = updated;
        }
      }
    });
  }

  viewOrderDetails(order: Order): void {
    this.selectedOrder = order;
  }

  closeDetails(): void {
    this.selectedOrder = null;
  }

  getStatusClass(status: string): string {
    switch (status) {
      case OrderStatus.PENDING: return 'status-pending';
      case OrderStatus.PROCESSING: return 'status-processing';
      case OrderStatus.SHIPPED: return 'status-shipped';
      case OrderStatus.DELIVERED: return 'status-delivered';
      case OrderStatus.CANCELLED: return 'status-cancelled';
      default: return '';
    }
  }

  formatDate(date: string | Date): string {
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  }
}
