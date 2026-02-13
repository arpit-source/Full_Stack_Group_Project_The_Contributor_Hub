export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  category: string;
  imageUrl: string;
  stock: number;
  rating: number;
  reviews: number;
}

export interface CartItem {
  product: Product;
  quantity: number;
}
