import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;
  quantity = 1;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.product = this.productService.getProductById(id);
      if (!this.product) {
        // Products may not have loaded yet, wait for them
        this.productService.getProducts().subscribe(products => {
          this.product = products.find(p => p.id === id);
          if (!this.product) {
            this.router.navigate(['/dashboard']);
          }
        });
      }
    }
  }

  increaseQuantity(): void {
    if (this.product && this.quantity < this.product.stock) {
      this.quantity++;
    }
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addToCart(): void {
    if (this.product) {
      this.cartService.addToCart(this.product.id, this.quantity);
      alert(`${this.quantity} x ${this.product.name} added to cart!`);
      this.quantity = 1;
    }
  }

  buyNow(): void {
    if (this.product) {
      this.cartService.addToCart(this.product.id, this.quantity);
      this.router.navigate(['/payment']);
    }
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }
}
