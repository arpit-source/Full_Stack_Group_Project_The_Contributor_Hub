# 🛒 E-Commerce Application

A complete full-stack e-commerce web application built with Angular 17+, featuring user authentication, product catalog, shopping cart, payment processing, and order management.

![Angular](https://img.shields.io/badge/Angular-17+-DD0031?style=for-the-badge&logo=angular)
![TypeScript](https://img.shields.io/badge/TypeScript-5.0+-3178C6?style=for-the-badge&logo=typescript)
![License](https://img.shields.io/badge/License-Academic-green?style=for-the-badge)

---

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Screenshots](#screenshots)
- [Technology Stack](#technology-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Documentation](#documentation)
- [Demo Account](#demo-account)
- [Contributing](#contributing)

---

## 📖 About

This E-Commerce Application is developed as part of the **Full Stack Application Development** course assignment. It demonstrates a comprehensive understanding of modern web application development, software architecture, and database design.

### Assignment Details
- **Course**: Full Stack Application Development
- **Weightage**: 20%
- **Framework**: Angular
- **Architecture**: Layered (3-tier)

---

## ✨ Features

### 🔐 User Authentication
- **User Registration** with validation
- **Secure Login/Logout** functionality
- **Session Management** using LocalStorage
- **Protected Routes** with Auth Guards

### 🛍️ Product Management
- **Product Catalog** with grid layout
- **Real-time Search** functionality
- **Category Filtering** (Electronics, Sports, Home, Accessories)
- **Product Details** page with images and descriptions
- **Stock Availability** indicators
- **Product Ratings** and reviews count

### 🛒 Shopping Cart
- **Add/Remove Items** from cart
- **Update Quantities** with +/- buttons
- **Real-time Total** calculation
- **Persistent Cart** across sessions
- **Cart Badge** showing item count

### 💳 Payment & Checkout
- **Dummy Payment Gateway** for testing
- **Order Summary** with itemized listing
- **Shipping Address** management
- **Payment Information** form
- **Order Confirmation** with unique Order ID

### 📦 Order Management
- **Order History** with all past orders
- **Order Status** tracking (Pending, Processing, Shipped, Delivered)
- **Detailed Order View** in modal
- **Item Breakdown** with quantities and prices

---

## 📸 Screenshots

### Login Screen
![Login Screen](https://via.placeholder.com/800x400/667eea/ffffff?text=Login+Screen)

### Product Dashboard
![Dashboard](https://via.placeholder.com/800x400/667eea/ffffff?text=Product+Dashboard)

### Product Details
![Product Details](https://via.placeholder.com/800x400/667eea/ffffff?text=Product+Details)

### Shopping Cart & Checkout
![Checkout](https://via.placeholder.com/800x400/667eea/ffffff?text=Checkout+Page)

### Orders History
![Orders](https://via.placeholder.com/800x400/667eea/ffffff?text=Orders+History)

---

## 🛠️ Technology Stack

### Frontend
- **Framework**: Angular 17+ (Standalone Components)
- **Language**: TypeScript 5.0+
- **Styling**: CSS3
- **State Management**: RxJS Observables

### Libraries & Tools
- **Angular Router**: Client-side routing
- **Angular Forms**: Form handling and validation
- **RxJS**: Reactive programming

### Data Storage
- **LocalStorage**: Client-side persistence
- **JSON**: Data serialization

---

## 🚀 Installation

### Prerequisites

Ensure you have the following installed:
- **Node.js** (v18.x or higher) - [Download](https://nodejs.org/)
- **npm** (v9.x or higher)
- **Angular CLI** (v17.x or higher)

```bash
# Install Angular CLI globally
npm install -g @angular/cli
```

### Setup Steps

1. **Clone or Navigate to Project Directory**
   ```bash
   cd "d:\BITS WILP\Sem 2\Full-stack Application Development\Assignment\Test\ecommerce-app"
   ```

2. **Install Dependencies**
   ```bash
   npm install
   ```

3. **Start Development Server**
   ```bash
   ng serve
   ```

4. **Open in Browser**
   - Navigate to: `http://localhost:4200`
   - The app will automatically reload on file changes

### Build for Production

```bash
ng build --configuration production
```

Build artifacts will be in the `dist/ecommerce-app/` directory.

---

## 📱 Usage

### Quick Start with Demo Account

1. **Access the Application**
   - Open `http://localhost:4200` in your browser
   
2. **Login with Demo Account**
   - Email: `demo@example.com`
   - Password: `demo123`

3. **Explore Features**
   - Browse products on the dashboard
   - Search and filter products
   - View product details
   - Add items to cart
   - Complete checkout process
   - View order history

### Creating New Account

1. Click **"Register here"** on login page
2. Fill in the registration form:
   - Full Name
   - Email Address
   - Phone Number
   - Address
   - Password (min 6 characters)
   - Confirm Password
3. Click **"Register"**
4. Login with your new credentials

### Shopping Workflow

```
Login → Browse Products → Search/Filter → View Details → 
Add to Cart → Review Cart → Checkout → Enter Payment Info → 
Place Order → View Order History
```

---

## 📂 Project Structure

```
ecommerce-app/
├── src/
│   ├── app/
│   │   ├── components/          # UI Components
│   │   │   ├── register/
│   │   │   ├── login/
│   │   │   ├── dashboard/
│   │   │   ├── product-detail/
│   │   │   ├── payment/
│   │   │   └── orders/
│   │   ├── services/            # Business Logic
│   │   │   ├── auth.service.ts
│   │   │   ├── product.service.ts
│   │   │   ├── cart.service.ts
│   │   │   └── order.service.ts
│   │   ├── models/              # Data Models
│   │   │   ├── user.model.ts
│   │   │   ├── product.model.ts
│   │   │   └── order.model.ts
│   │   ├── guards/              # Route Guards
│   │   │   └── auth.guard.ts
│   │   ├── app.component.ts     # Root Component
│   │   ├── app.routes.ts        # Routes Configuration
│   │   └── app.config.ts        # App Configuration
│   ├── styles.css               # Global Styles
│   └── index.html               # HTML Entry Point
├── documentation/               # Project Documentation
│   ├── 1_Logical_Architecture.md
│   ├── 2_ER_Model.md
│   └── 3_Complete_Documentation.md
├── angular.json                 # Angular Configuration
├── package.json                 # Dependencies
├── tsconfig.json               # TypeScript Config
└── README.md                    # This File
```

---

## 📚 Documentation

Comprehensive documentation is available in the `documentation/` folder:

### 1. Logical Architecture Document
📄 **[1_Logical_Architecture.md](./documentation/1_Logical_Architecture.md)**
- System layers and responsibilities
- Component organization
- Package diagrams
- Layer interactions
- Design principles

### 2. ER Model Document
📄 **[2_ER_Model.md](./documentation/2_ER_Model.md)**
- Entity definitions
- ER diagrams
- Relationships and constraints
- Data dictionary
- Sample data

### 3. Complete Documentation
📄 **[3_Complete_Documentation.md](./documentation/3_Complete_Documentation.md)**
- Executive summary
- Feature list
- User guide
- Testing documentation
- Future enhancements

---

## 🔑 Demo Account

For quick testing, use the pre-configured demo account:

```
Email: demo@example.com
Password: demo123
```

### Test Payment Details

When checking out, use these dummy payment details:

```
Card Number: 4111 1111 1111 1111
Cardholder: Any Name
Expiry Date: Any future date (MM/YY)
CVV: 123
```

---

## 🎯 Key Highlights

✅ **6 Required Screens Implemented**
- Registration
- Login  
- Dashboard (Product Catalog)
- Product Details
- Payment Gateway
- Orders History

✅ **Solid Architecture**
- Layered architecture with separation of concerns
- Component-based design
- Service-oriented business logic

✅ **Complete Documentation**
- Logical architecture with UML diagrams
- ER model with relationships
- Comprehensive user guide

✅ **Modern Development Practices**
- TypeScript for type safety
- Reactive programming with RxJS
- Standalone components (Angular 17+)
- Route guards for security

---

## 🔮 Future Enhancements

- [ ] Backend API integration (Node.js/Express)
- [ ] Database integration (PostgreSQL/MongoDB)
- [ ] Admin panel for product management
- [ ] Email notifications
- [ ] Product reviews and ratings
- [ ] Wishlist functionality
- [ ] Multiple payment gateways
- [ ] Real-time order tracking
- [ ] PWA support

---

## 🤝 Contributing

This is an academic project. For suggestions or improvements:

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

---

## 📝 License

This project is created for academic purposes as part of the Full Stack Application Development course at BITS Pilani.

---

## 👨‍💻 Author

**BITS WILP Student**  
Course: Full Stack Application Development  
Semester: 2  
Assignment: E-Commerce App Design

---

## 🙏 Acknowledgments

- BITS Pilani Work Integrated Learning Program
- Full Stack Application Development Course Faculty
- Angular Team for excellent documentation
- Unsplash for product images

---

## 📞 Support

For any queries or issues:
- Check the [Complete Documentation](./documentation/3_Complete_Documentation.md)
- Review the [Logical Architecture](./documentation/1_Logical_Architecture.md)
- Refer to the [ER Model](./documentation/2_ER_Model.md)

---

**Made with ❤️ using Angular**

---

*Last Updated: February 7, 2026*
