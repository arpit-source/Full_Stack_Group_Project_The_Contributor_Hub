import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth.service';
import { CartItem } from '../../models/product.model';
import { PaymentInfo } from '../../models/order.model';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  cartItems: CartItem[] = [];
  total = 0;

  paymentInfo: PaymentInfo = {
    cardNumber: '',
    cardHolder: '',
    expiryDate: '',
    cvv: ''
  };

  shippingAddress = '';
  errorMessage = '';
  isProcessing = false;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.cartService.refreshCart();
    this.cartService.getCart().subscribe(items => {
      this.cartItems = items;
      this.total = this.cartService.getTotal();
    });

    const user = this.authService.getCurrentUser();
    if (user) {
      this.shippingAddress = user.address;
    }
  }

  removeItem(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  updateQuantity(productId: number, quantity: number): void {
    this.cartService.updateQuantity(productId, quantity);
  }

  processPayment(): void {
    this.errorMessage = '';

    if (this.cartItems.length === 0) {
      this.errorMessage = 'Your cart is empty. Please add items before checkout.';
      setTimeout(() => {
        this.router.navigate(['/dashboard']);
      }, 2000);
      return;
    }

    if (!this.paymentInfo.cardNumber || !this.paymentInfo.cardHolder ||
        !this.paymentInfo.expiryDate || !this.paymentInfo.cvv) {
      this.errorMessage = 'Please fill in all payment details';
      return;
    }

    if (!this.shippingAddress.trim()) {
      this.errorMessage = 'Please provide a shipping address';
      return;
    }

    if (this.paymentInfo.cardNumber.replace(/\s/g, '').length !== 16) {
      this.errorMessage = 'Card number must be 16 digits';
      return;
    }

    if (this.paymentInfo.cvv.length !== 3) {
      this.errorMessage = 'CVV must be 3 digits';
      return;
    }

    this.isProcessing = true;

    this.orderService.createOrder(this.paymentInfo, this.shippingAddress).subscribe({
      next: (order) => {
        this.cartService.clearCart();
        this.isProcessing = false;
        alert(`Order placed successfully! Order ID: ${order.id}`);
        this.router.navigate(['/orders']);
      },
      error: () => {
        this.isProcessing = false;
        this.errorMessage = 'Payment processing failed. Please try again.';
      }
    });
  }

  formatCardNumber(): void {
    let value = this.paymentInfo.cardNumber.replace(/\s/g, '');
    let formattedValue = value.match(/.{1,4}/g)?.join(' ') || value;
    this.paymentInfo.cardNumber = formattedValue;
  }
}
