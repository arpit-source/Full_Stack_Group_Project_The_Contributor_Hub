import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterData } from '../../models/user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerData: RegisterData = {
    email: '',
    password: '',
    name: '',
    phone: '',
    address: ''
  };
  confirmPassword = '';
  errorMessage = '';
  successMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.registerData.email || !this.registerData.password || !this.registerData.name ||
        !this.registerData.phone || !this.registerData.address) {
      this.errorMessage = 'All fields are required';
      return;
    }

    if (this.registerData.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    if (this.registerData.password.length < 6) {
      this.errorMessage = 'Password must be at least 6 characters long';
      return;
    }

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(this.registerData.email)) {
      this.errorMessage = 'Please enter a valid email address';
      return;
    }

    this.authService.register(this.registerData).subscribe(success => {
      if (success) {
        this.successMessage = 'Registration successful! Redirecting to login...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      } else {
        this.errorMessage = 'Email already exists';
      }
    });
  }
}
