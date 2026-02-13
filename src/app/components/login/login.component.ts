import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { LoginCredentials } from '../../models/user.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  credentials: LoginCredentials = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/dashboard']);
    }
  }

  onSubmit(): void {
    this.errorMessage = '';

    if (!this.credentials.email || !this.credentials.password) {
      this.errorMessage = 'Please enter both email and password';
      return;
    }

    this.authService.login(this.credentials).subscribe(success => {
      if (success) {
        this.cartService.refreshCart();
        this.router.navigate(['/dashboard']);
      } else {
        this.errorMessage = 'Invalid email or password';
      }
    });
  }
}
