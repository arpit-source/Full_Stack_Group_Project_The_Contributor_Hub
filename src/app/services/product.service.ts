import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;
  private products: Product[] = [];
  private productsSubject = new BehaviorSubject<Product[]>([]);

  constructor(private http: HttpClient) {
    this.loadProducts();
  }

  private loadProducts(): void {
    this.http.get<Product[]>(this.apiUrl).subscribe(products => {
      this.products = products;
      this.productsSubject.next(products);
    });
  }

  getProducts(): Observable<Product[]> {
    return this.productsSubject.asObservable();
  }

  getProductById(id: number): Product | undefined {
    return this.products.find(p => p.id === id);
  }

  searchProducts(query: string): Product[] {
    const lowerQuery = query.toLowerCase();
    return this.products.filter(p =>
      p.name.toLowerCase().includes(lowerQuery) ||
      p.description.toLowerCase().includes(lowerQuery) ||
      p.category.toLowerCase().includes(lowerQuery)
    );
  }

  getProductsByCategory(category: string): Product[] {
    return this.products.filter(p => p.category === category);
  }

  getCategories(): string[] {
    return Array.from(new Set(this.products.map(p => p.category)));
  }
}
