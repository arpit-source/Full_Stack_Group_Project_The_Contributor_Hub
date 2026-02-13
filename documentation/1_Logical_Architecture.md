# LOGICAL ARCHITECTURE DOCUMENT
## E-Commerce Application

**Course:** Full Stack Application Development  
**Assignment:** App Design - E-Commerce App  
**Date:** February 7, 2026

---

## Table of Contents
1. [Introduction](#1-introduction)
2. [Architectural Pattern](#2-architectural-pattern)
3. [System Layers](#3-system-layers)
4. [Component Organization](#4-component-organization)
5. [Package Diagram](#5-package-diagram)
6. [Layer Interactions](#6-layer-interactions)
7. [Design Principles](#7-design-principles)

---

## 1. Introduction

This document describes the logical architecture of the E-Commerce Application built using Angular framework. The architecture follows a layered approach with clear separation of concerns, ensuring maintainability, scalability, and testability.

### 1.1 Purpose
The purpose of this document is to:
- Define the large-scale organization of software components
- Identify subsystems and layers
- Describe responsibilities of each layer
- Document interactions between components

### 1.2 Scope
This architecture document covers the client-side Angular application structure, including presentation, business logic, and data management layers.

---

## 2. Architectural Pattern

The application follows a **Layered Architecture Pattern** with the following characteristics:

- **Three-Tier Architecture**: Presentation, Business Logic, and Data layers
- **Component-Based Architecture**: Modular, reusable Angular components
- **Service-Oriented Design**: Separation of business logic into services
- **Reactive Programming**: Observable-based state management using RxJS

---

## 3. System Layers

### 3.1 Presentation Layer (UI Layer)

**Responsibility**: Handle user interactions and display data

**Components**:
- **RegisterComponent**: User registration interface
- **LoginComponent**: User authentication interface
- **DashboardComponent**: Product catalog with search and filtering
- **ProductDetailComponent**: Detailed product information view
- **PaymentComponent**: Checkout and payment processing
- **OrdersComponent**: Order history and details

**Characteristics**:
- Standalone Angular components
- Template-driven and reactive forms
- Responsive design using CSS
- User input validation
- Navigation routing

**Technologies**:
- Angular 17+ (Standalone Components)
- TypeScript
- HTML5
- CSS3
- Angular Forms Module
- Angular Router

---

### 3.2 Business Logic Layer (Service Layer)

**Responsibility**: Implement core business functionality and application logic

**Services**:

#### 3.2.1 AuthService
- User registration and validation
- User authentication (login/logout)
- Session management
- User data retrieval
- Demo user initialization

#### 3.2.2 ProductService
- Product catalog management
- Product search functionality
- Category filtering
- Product details retrieval
- Observable-based product stream

#### 3.2.3 CartService
- Shopping cart management
- Add/remove items from cart
- Quantity updates
- Cart total calculation
- Persistent cart storage

#### 3.2.4 OrderService
- Order creation and processing
- Order history retrieval
- Order status management
- Order details retrieval

**Characteristics**:
- Injectable services with `@Injectable` decorator
- Singleton pattern (providedIn: 'root')
- Separation from UI components
- Reusable across multiple components
- Observable pattern for reactive data flow

---

### 3.3 Data Layer (Data Access Layer)

**Responsibility**: Handle data persistence and retrieval

**Implementation**:
- Browser LocalStorage API for data persistence
- JSON serialization/deserialization
- Data models and interfaces

**Data Stores**:
- `users`: User account information
- `currentUser`: Active session data
- `shopping_cart`: Cart items
- `orders`: Order history

**Characteristics**:
- Client-side data storage
- No external database required
- Data persists across browser sessions
- Encapsulated within services

---

### 3.4 Cross-Cutting Concerns

#### 3.4.1 Guards
- **AuthGuard**: Route protection for authenticated pages
- Implements CanActivateFn interface
- Redirects unauthenticated users to login

#### 3.4.2 Models/Interfaces
- **User Model**: User account structure
- **Product Model**: Product information structure
- **Order Model**: Order and payment information
- **CartItem Model**: Shopping cart item structure

#### 3.4.3 Routing
- Angular Router configuration
- Lazy loading support
- Route guards integration
- Navigation flow management

---

## 4. Component Organization

### 4.1 Directory Structure

```
src/app/
├── components/           # Presentation Layer
│   ├── register/
│   ├── login/
│   ├── dashboard/
│   ├── product-detail/
│   ├── payment/
│   └── orders/
├── services/            # Business Logic Layer
│   ├── auth.service.ts
│   ├── product.service.ts
│   ├── cart.service.ts
│   └── order.service.ts
├── models/              # Data Models
│   ├── user.model.ts
│   ├── product.model.ts
│   └── order.model.ts
├── guards/              # Route Guards
│   └── auth.guard.ts
├── app.component.ts     # Root Component
├── app.routes.ts        # Route Configuration
└── app.config.ts        # App Configuration
```

### 4.2 Component Hierarchy

```
AppComponent (Root)
├── RouterOutlet
│   ├── LoginComponent
│   ├── RegisterComponent
│   ├── DashboardComponent (Auth Protected)
│   ├── ProductDetailComponent (Auth Protected)
│   ├── PaymentComponent (Auth Protected)
│   └── OrdersComponent (Auth Protected)
```

---

## 5. Package Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                        │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  UI Components (Standalone Angular Components)      │  │
│  │  • RegisterComponent  • LoginComponent              │  │
│  │  • DashboardComponent • ProductDetailComponent      │  │
│  │  • PaymentComponent   • OrdersComponent             │  │
│  └──────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                            ↓ ↑
                    (Uses Services)
                            ↓ ↑
┌─────────────────────────────────────────────────────────────┐
│                  BUSINESS LOGIC LAYER                        │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Services (Injectable Services)                      │  │
│  │  • AuthService      • ProductService                 │  │
│  │  • CartService      • OrderService                   │  │
│  └──────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
                            ↓ ↑
                    (Uses Models & Data)
                            ↓ ↑
┌─────────────────────────────────────────────────────────────┐
│                      DATA LAYER                              │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  Data Models & Storage                               │  │
│  │  • User Model       • Product Model                  │  │
│  │  • Order Model      • LocalStorage                   │  │
│  └──────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│               CROSS-CUTTING CONCERNS                         │
│  • AuthGuard (Route Protection)                             │
│  • Router (Navigation)                                       │
│  • RxJS (Reactive Programming)                              │
└─────────────────────────────────────────────────────────────┘
```

---

## 6. Layer Interactions

### 6.1 User Authentication Flow

```
User Input (Login Form)
        ↓
LoginComponent (Presentation)
        ↓
AuthService.login() (Business Logic)
        ↓
LocalStorage.getItem('users') (Data)
        ↓
Validate Credentials
        ↓
LocalStorage.setItem('currentUser') (Data)
        ↓
Router.navigate('/dashboard') (Navigation)
```

### 6.2 Product Browsing Flow

```
User Opens Dashboard
        ↓
DashboardComponent (Presentation)
        ↓
ProductService.getProducts() (Business Logic)
        ↓
Observable<Product[]> (Reactive Data)
        ↓
Display Products in Grid
        ↓
User Searches/Filters
        ↓
ProductService.searchProducts() (Business Logic)
        ↓
Update Filtered Results
```

### 6.3 Order Placement Flow

```
User Initiates Checkout
        ↓
PaymentComponent (Presentation)
        ↓
CartService.getCart() (Business Logic)
        ↓
Display Cart Items
        ↓
User Enters Payment Info
        ↓
OrderService.createOrder() (Business Logic)
        ↓
LocalStorage.setItem('orders') (Data)
        ↓
CartService.clearCart() (Business Logic)
        ↓
Router.navigate('/orders') (Navigation)
```

---

## 7. Design Principles

### 7.1 Separation of Concerns (SoC)
- **UI Logic**: Isolated in component classes
- **Business Logic**: Centralized in services
- **Data Access**: Encapsulated within services
- **Styling**: Separated in component CSS files

### 7.2 Single Responsibility Principle (SRP)
- Each component has one primary responsibility
- Each service manages a specific domain
- Models define data structure only

### 7.3 Dependency Injection
- Services injected into components
- Promotes loose coupling
- Enables testability and mocking

### 7.4 Reactive Programming
- Observable streams for data flow
- Asynchronous data handling
- Automatic UI updates on data changes

### 7.5 Modularity
- Standalone components
- Reusable services
- Clear interfaces between layers

### 7.6 Encapsulation
- Private methods in services
- Public API for components
- Data hiding in services

### 7.7 DRY (Don't Repeat Yourself)
- Shared services across components
- Reusable models and interfaces
- Common styling patterns

---

## 8. Conclusion

This logical architecture provides a solid foundation for the E-Commerce Application with:
- **Clear layer separation** ensuring maintainability
- **Service-based business logic** promoting reusability
- **Component-based UI** enabling modularity
- **Reactive data flow** providing responsiveness
- **Client-side persistence** simplifying deployment

The architecture follows Angular best practices and industry-standard design patterns, making the application scalable, testable, and easy to maintain.

---

**Document Version**: 1.0  
**Last Updated**: February 7, 2026
