# ENTITY-RELATIONSHIP MODEL
## E-Commerce Application

**Course:** Full Stack Application Development  
**Assignment:** App Design - E-Commerce App  
**Date:** February 7, 2026

---

## Table of Contents

1. [Introduction](#1-introduction)
2. [Entity Definitions](#2-entity-definitions)
3. [ER Diagram](#3-er-diagram)
4. [Entity Descriptions](#4-entity-descriptions)
5. [Relationships](#5-relationships)
6. [Constraints](#6-constraints)
7. [Data Dictionary](#7-data-dictionary)

---

## 1. Introduction

### 1.1 Purpose

This document describes the Entity-Relationship (ER) model for the E-Commerce Application. It captures the data requirements from the business domain, identifies constraints related to data items, and describes the relationships between entities.

### 1.2 Scope

The ER model covers all data entities required for:
- User management and authentication
- Product catalog management
- Shopping cart functionality
- Order processing and history
- Payment information

---

## 2. Entity Definitions

The E-Commerce Application consists of the following main entities:

1. **USER**: Registered users of the e-commerce platform
2. **PRODUCT**: Items available for purchase
3. **CART_ITEM**: Items added to a user's shopping cart
4. **ORDER**: Completed purchase transactions
5. **ORDER_ITEM**: Individual products within an order
6. **PAYMENT_INFO**: Payment details for orders

---

## 3. ER Diagram

```
┌─────────────────────────┐
│        USER             │
├─────────────────────────┤
│ PK: id                  │
│     email               │
│     password            │
│     name                │
│     phone               │
│     address             │
│     createdAt           │
└─────────────────────────┘
            │
            │ 1
            │
            │ Places
            │
            │ N
            ▼
┌─────────────────────────┐
│        ORDER            │
├─────────────────────────┤
│ PK: id                  │
│ FK: userId              │
│     totalAmount         │
│     status              │
│     paymentMethod       │
│     shippingAddress     │
│     orderDate           │
│     deliveryDate        │
└─────────────────────────┘
            │
            │ 1
            │
            │ Contains
            │
            │ N
            ▼
┌─────────────────────────┐
│     ORDER_ITEM          │
├─────────────────────────┤
│ PK: orderId + productId │
│ FK: orderId             │
│ FK: productId           │
│     quantity            │
│     priceAtPurchase     │
└─────────────────────────┘
            │
            │ N
            │
            │ References
            │
            │ 1
            ▼
┌─────────────────────────┐
│       PRODUCT           │
├─────────────────────────┤
│ PK: id                  │
│     name                │
│     description         │
│     price               │
│     category            │
│     imageUrl            │
│     stock               │
│     rating              │
│     reviews             │
└─────────────────────────┘
            ▲
            │ 1
            │
            │ Contains
            │
            │ N
            │
┌─────────────────────────┐
│      CART_ITEM          │
├─────────────────────────┤
│ PK: userId + productId  │
│ FK: userId              │
│ FK: productId           │
│     quantity            │
└─────────────────────────┘
            │
            │ N
            │
            │ Belongs To
            │
            │ 1
            ▼
        [USER]
```

---

## 4. Entity Descriptions

### 4.1 USER Entity

**Description**: Represents registered users of the e-commerce platform.

**Attributes**:

| Attribute  | Type     | Description                    | Constraints                    |
|------------|----------|--------------------------------|--------------------------------|
| id         | string   | Unique user identifier         | PRIMARY KEY, NOT NULL, UNIQUE  |
| email      | string   | User's email address           | NOT NULL, UNIQUE, VALID EMAIL  |
| password   | string   | Encrypted password             | NOT NULL, MIN LENGTH 6         |
| name       | string   | User's full name               | NOT NULL                       |
| phone      | string   | Contact phone number           | NOT NULL                       |
| address    | string   | Shipping address               | NOT NULL                       |
| createdAt  | Date     | Account creation timestamp     | NOT NULL                       |

**Business Rules**:
- Email must be unique across all users
- Password must be at least 6 characters
- All fields are mandatory

---

### 4.2 PRODUCT Entity

**Description**: Represents items available for purchase in the e-commerce catalog.

**Attributes**:

| Attribute   | Type     | Description                    | Constraints                    |
|-------------|----------|--------------------------------|--------------------------------|
| id          | string   | Unique product identifier      | PRIMARY KEY, NOT NULL, UNIQUE  |
| name        | string   | Product name                   | NOT NULL                       |
| description | string   | Detailed product description   | NOT NULL                       |
| price       | number   | Product price in dollars       | NOT NULL, > 0                  |
| category    | string   | Product category               | NOT NULL                       |
| imageUrl    | string   | Product image URL              | NOT NULL                       |
| stock       | number   | Available quantity             | NOT NULL, >= 0                 |
| rating      | number   | Average customer rating        | 0-5 scale                      |
| reviews     | number   | Number of reviews              | >= 0                           |

**Business Rules**:
- Price must be greater than zero
- Stock cannot be negative
- Rating must be between 0 and 5

---

### 4.3 CART_ITEM Entity

**Description**: Represents items added to a user's shopping cart (temporary storage before purchase).

**Attributes**:

| Attribute  | Type     | Description                    | Constraints                    |
|------------|----------|--------------------------------|--------------------------------|
| userId     | string   | Reference to user              | FOREIGN KEY (USER), NOT NULL   |
| productId  | string   | Reference to product           | FOREIGN KEY (PRODUCT), NOT NULL|
| quantity   | number   | Quantity of product            | NOT NULL, > 0                  |

**Composite Primary Key**: (userId, productId)

**Business Rules**:
- A user can have multiple cart items
- Each product can appear only once per user's cart
- Quantity must be at least 1
- Cart items are temporary and cleared after order placement

---

### 4.4 ORDER Entity

**Description**: Represents a completed purchase transaction.

**Attributes**:

| Attribute        | Type     | Description                    | Constraints                    |
|------------------|----------|--------------------------------|--------------------------------|
| id               | string   | Unique order identifier        | PRIMARY KEY, NOT NULL, UNIQUE  |
| userId           | string   | Reference to user              | FOREIGN KEY (USER), NOT NULL   |
| totalAmount      | number   | Total order value              | NOT NULL, > 0                  |
| status           | enum     | Order status                   | NOT NULL, ENUM values          |
| paymentMethod    | string   | Payment method description     | NOT NULL                       |
| shippingAddress  | string   | Delivery address               | NOT NULL                       |
| orderDate        | Date     | Order placement timestamp      | NOT NULL                       |
| deliveryDate     | Date     | Delivery timestamp (optional)  | NULL allowed                   |

**Status Enum Values**:
- PENDING
- PROCESSING
- SHIPPED
- DELIVERED
- CANCELLED

**Business Rules**:
- Each order belongs to exactly one user
- Total amount must be greater than zero
- Order date is automatically set on creation
- Delivery date is set when status becomes DELIVERED

---

### 4.5 ORDER_ITEM Entity

**Description**: Represents individual products within an order (join table with additional attributes).

**Attributes**:

| Attribute        | Type     | Description                    | Constraints                    |
|------------------|----------|--------------------------------|--------------------------------|
| orderId          | string   | Reference to order             | FOREIGN KEY (ORDER), NOT NULL  |
| productId        | string   | Reference to product           | FOREIGN KEY (PRODUCT), NOT NULL|
| quantity         | number   | Quantity purchased             | NOT NULL, > 0                  |
| priceAtPurchase  | number   | Price at time of purchase      | NOT NULL, > 0                  |

**Composite Primary Key**: (orderId, productId)

**Business Rules**:
- Stores the price at the time of purchase (historical data)
- Multiple order items can belong to one order
- Each product can appear only once per order

---

## 5. Relationships

### 5.1 USER ↔ ORDER (One-to-Many)

**Relationship Name**: Places  
**Cardinality**: 1:N  
**Description**: A user can place multiple orders, but each order belongs to exactly one user.

**Constraints**:
- Mandatory participation from ORDER (each order must have a user)
- Optional participation from USER (a user may have zero orders)

---

### 5.2 USER ↔ CART_ITEM (One-to-Many)

**Relationship Name**: Has  
**Cardinality**: 1:N  
**Description**: A user can have multiple items in their cart, but each cart item belongs to exactly one user.

**Constraints**:
- Mandatory participation from CART_ITEM (each cart item must belong to a user)
- Optional participation from USER (a user may have an empty cart)

---

### 5.3 PRODUCT ↔ CART_ITEM (One-to-Many)

**Relationship Name**: Contains  
**Cardinality**: 1:N  
**Description**: A product can be in multiple users' carts, but each cart item references exactly one product.

**Constraints**:
- Mandatory participation from CART_ITEM (each cart item must reference a product)
- Optional participation from PRODUCT (a product may not be in any cart)

---

### 5.4 ORDER ↔ ORDER_ITEM (One-to-Many)

**Relationship Name**: Contains  
**Cardinality**: 1:N  
**Description**: An order contains multiple order items, but each order item belongs to exactly one order.

**Constraints**:
- Mandatory participation from ORDER_ITEM (each order item must belong to an order)
- Mandatory participation from ORDER (each order must have at least one item)

---

### 5.5 PRODUCT ↔ ORDER_ITEM (One-to-Many)

**Relationship Name**: References  
**Cardinality**: 1:N  
**Description**: A product can be referenced in multiple order items, but each order item references exactly one product.

**Constraints**:
- Mandatory participation from ORDER_ITEM (each order item must reference a product)
- Optional participation from PRODUCT (a product may never be ordered)

---

## 6. Constraints

### 6.1 Primary Key Constraints

- **USER**: `id` must be unique and not null
- **PRODUCT**: `id` must be unique and not null
- **ORDER**: `id` must be unique and not null
- **CART_ITEM**: Composite key (`userId`, `productId`) must be unique
- **ORDER_ITEM**: Composite key (`orderId`, `productId`) must be unique

### 6.2 Foreign Key Constraints

- **ORDER.userId** → **USER.id**
- **CART_ITEM.userId** → **USER.id**
- **CART_ITEM.productId** → **PRODUCT.id**
- **ORDER_ITEM.orderId** → **ORDER.id**
- **ORDER_ITEM.productId** → **PRODUCT.id**

### 6.3 Domain Constraints

- **Email Format**: Must match pattern `[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}`
- **Password Length**: Minimum 6 characters
- **Price Values**: Must be greater than 0
- **Stock Values**: Cannot be negative (>= 0)
- **Rating Values**: Must be between 0.0 and 5.0
- **Quantity Values**: Must be greater than 0

### 6.4 Referential Integrity Constraints

- Deleting a USER should cascade delete their CART_ITEMS and ORDERS
- Deleting a PRODUCT should prevent deletion if it exists in any ORDER_ITEM (historical data)
- Deleting an ORDER should cascade delete all associated ORDER_ITEMS

### 6.5 Business Rule Constraints

- A user cannot have duplicate products in their cart (enforced by composite PK)
- An order cannot have duplicate products (enforced by composite PK)
- Order total must equal the sum of (quantity × priceAtPurchase) for all order items
- Cart items are cleared after successful order placement

---

## 7. Data Dictionary

### 7.1 Data Types

| Type    | Description                          | Example                    |
|---------|--------------------------------------|----------------------------|
| string  | Variable-length text                 | "John Doe", "ABC123"       |
| number  | Numeric value (integer or decimal)   | 299.99, 5, 100             |
| Date    | Timestamp with date and time         | 2026-02-07T10:30:00Z       |
| enum    | Enumerated type with fixed values    | PENDING, PROCESSING        |
| boolean | True or false value                  | true, false                |

### 7.2 Common Fields

**ID Generation**: IDs are generated using timestamp-based unique identifiers  
**Date Format**: ISO 8601 format (YYYY-MM-DDTHH:mm:ss.sssZ)  
**Currency**: All prices in USD  
**Image URLs**: Absolute URLs to external image hosting

---

## 8. Implementation Notes

### 8.1 Storage Mechanism

The current implementation uses **Browser LocalStorage** for data persistence:

- **users**: Array of User objects
- **currentUser**: Currently logged-in user (session)
- **shopping_cart**: Array of CartItem objects
- **orders**: Array of Order objects

### 8.2 Data Normalization

The ER model follows **Third Normal Form (3NF)**:

1. **First Normal Form (1NF)**: All attributes are atomic (no multi-valued attributes)
2. **Second Normal Form (2NF)**: All non-key attributes fully depend on the primary key
3. **Third Normal Form (3NF)**: No transitive dependencies exist

### 8.3 Denormalization

For performance optimization, the following denormalization is applied:

- **ORDER_ITEM.priceAtPurchase**: Stores product price to maintain historical accuracy
- **PRODUCT.rating** and **PRODUCT.reviews**: Aggregated values stored for quick access

---

## 9. Sample Data

### 9.1 Sample User

```typescript
{
  id: "demo-user-001",
  email: "demo@example.com",
  password: "demo123",
  name: "Demo User",
  phone: "+1234567890",
  address: "123 Demo Street, Demo City, DC 12345",
  createdAt: "2026-02-07T10:00:00.000Z"
}
```

### 9.2 Sample Product

```typescript
{
  id: "1",
  name: "Wireless Headphones",
  description: "Premium wireless headphones with noise cancellation",
  price: 299.99,
  category: "Electronics",
  imageUrl: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e",
  stock: 50,
  rating: 4.5,
  reviews: 234
}
```

### 9.3 Sample Order

```typescript
{
  id: "ORD-ABC123",
  userId: "demo-user-001",
  totalAmount: 299.99,
  status: "DELIVERED",
  paymentMethod: "Card ending in 1111",
  shippingAddress: "123 Demo Street, Demo City, DC 12345",
  orderDate: "2026-02-01T14:30:00.000Z",
  deliveryDate: "2026-02-05T16:00:00.000Z"
}
```

---

## 10. Conclusion

This ER model provides a comprehensive data structure for the E-Commerce Application with:

- **Clear entity definitions** with well-defined attributes
- **Normalized relationships** reducing data redundancy
- **Strong constraints** ensuring data integrity
- **Business rule enforcement** through database constraints
- **Scalable design** supporting future enhancements

The model successfully captures all data requirements from the business domain and provides a solid foundation for the application's data layer.

---

**Document Version**: 1.0  
**Last Updated**: February 7, 2026
