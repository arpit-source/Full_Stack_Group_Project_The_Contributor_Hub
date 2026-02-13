import { CartItem } from './product.model';

export interface Order {
  id: number;
  userId: number;
  items: CartItem[];
  totalAmount: number;
  status: string;
  paymentMethod: string;
  shippingAddress: string;
  orderDate: string;
  deliveryDate?: string;
}

export enum OrderStatus {
  PENDING = 'Pending',
  PROCESSING = 'Processing',
  SHIPPED = 'Shipped',
  DELIVERED = 'Delivered',
  CANCELLED = 'Cancelled'
}

export interface PaymentInfo {
  cardNumber: string;
  cardHolder: string;
  expiryDate: string;
  cvv: string;
}
