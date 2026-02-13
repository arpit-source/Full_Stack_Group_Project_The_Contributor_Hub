import { Component } from '@angular/core';
import { Router, RouterOutlet, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';
import { CartService } from './services/cart.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'E-Commerce App';
  cartItemCount = 0;

  constructor(
    public authService: AuthService,
    private cartService: CartService,
    public router: Router
  ) {
    this.cartService.getCart().subscribe(items => {
      this.cartItemCount = items.reduce((count, item) => count + item.quantity, 0);
    });

    if (this.authService.isLoggedIn()) {
      this.cartService.refreshCart();
    }
  }

  navigateToCart(): void {
    this.router.navigate(['/payment']);
  }

  logout(): void {
    this.authService.logout();
    this.cartItemCount = 0;
  }
}
