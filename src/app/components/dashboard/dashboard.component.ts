import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  categories: string[] = [];
  searchQuery = '';
  selectedCategory = '';

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe(products => {
      this.products = products;
      this.filteredProducts = products;
      this.categories = [...new Set(products.map(p => p.category))];
    });
  }

  onSearch(): void {
    this.applyFilters();
  }

  onCategoryChange(): void {
    this.applyFilters();
  }

  private applyFilters(): void {
    let results = this.products;

    if (this.searchQuery.trim()) {
      results = this.productService.searchProducts(this.searchQuery);
    }

    if (this.selectedCategory) {
      results = results.filter(p => p.category === this.selectedCategory);
    }

    this.filteredProducts = results;
  }

  addToCart(product: Product): void {
    this.cartService.addToCart(product.id, 1);
    alert(`${product.name} added to cart!`);
  }

  buyNow(product: Product): void {
    this.cartService.addToCart(product.id, 1);
    this.router.navigate(['/payment']);
  }

  viewProduct(productId: number): void {
    this.router.navigate(['/product', productId]);
  }

  clearFilters(): void {
    this.searchQuery = '';
    this.selectedCategory = '';
    this.filteredProducts = this.products;
  }
}
