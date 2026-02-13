export interface User {
  id: number;
  email: string;
  password?: string;
  name: string;
  phone: string;
  address: string;
  createdAt: Date;
}

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  email: string;
  password: string;
  name: string;
  phone: string;
  address: string;
}
