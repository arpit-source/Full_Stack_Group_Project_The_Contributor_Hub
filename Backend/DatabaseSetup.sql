-- =====================================================
-- Database Setup Script for ContributorHubDb
-- Run this in SQL Server Management Studio (SSMS)
-- =====================================================

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ContributorHubDb')
    CREATE DATABASE ContributorHubDb;
GO

USE ContributorHubDb;
GO

-- Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(500) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Address NVARCHAR(500) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
GO

CREATE UNIQUE INDEX IX_Users_Email ON Users(Email)
WHERE NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_Email');
GO

-- Products Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    ImageUrl NVARCHAR(500) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    Rating FLOAT NOT NULL DEFAULT 0,
    Reviews INT NOT NULL DEFAULT 0
);
GO

-- CartItems Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CartItems')
CREATE TABLE CartItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL DEFAULT 1
);
GO

-- Orders Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    PaymentMethod NVARCHAR(200) NOT NULL,
    ShippingAddress NVARCHAR(500) NOT NULL,
    OrderDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    DeliveryDate DATETIME2 NULL
);
GO

-- OrderItems Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderItems')
CREATE TABLE OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
    ProductId INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    ProductPrice DECIMAL(18,2) NOT NULL,
    ProductImageUrl NVARCHAR(500) NOT NULL,
    ProductCategory NVARCHAR(100) NOT NULL DEFAULT '',
    Quantity INT NOT NULL
);
GO

-- =====================================================
-- Seed Products (only if table is empty)
-- =====================================================
IF NOT EXISTS (SELECT TOP 1 1 FROM Products)
BEGIN
    INSERT INTO Products (Name, Description, Price, Category, ImageUrl, Stock, Rating, Reviews) VALUES
    ('Wireless Headphones', 'Premium wireless headphones with noise cancellation and 30-hour battery life', 2499.00, 'Electronics', 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500', 50, 4.5, 234),
    ('Smart Watch', 'Fitness tracker with heart rate monitor and GPS', 4999.00, 'Electronics', 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500', 30, 4.7, 456),
    ('Running Shoes', 'Lightweight running shoes with cushioned sole', 3499.00, 'Sports', 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500', 100, 4.3, 789),
    ('Laptop Backpack', 'Water-resistant laptop backpack with USB charging port', 1299.00, 'Accessories', 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500', 75, 4.6, 345),
    ('Coffee Maker', 'Programmable coffee maker with thermal carafe', 2999.00, 'Home', 'https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=500', 40, 4.4, 567),
    ('Yoga Mat', 'Non-slip eco-friendly yoga mat with carrying strap', 899.00, 'Sports', 'https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500', 120, 4.8, 234),
    ('Bluetooth Speaker', 'Portable waterproof speaker with 12-hour battery', 1999.00, 'Electronics', 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500', 60, 4.5, 432),
    ('Desk Lamp', 'LED desk lamp with adjustable brightness and color temperature', 1499.00, 'Home', 'https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500', 85, 4.2, 123);
END
GO

-- =====================================================
-- Seed Demo User (password: demo123)
-- The app's Program.cs also seeds this user automatically.
-- =====================================================
IF NOT EXISTS (SELECT TOP 1 1 FROM Users)
BEGIN
    INSERT INTO Users (Email, Password, Name, Phone, Address, CreatedAt) VALUES
    ('demo@example.com', 'demo123', 'Demo User', '+1234567890', '123 Demo Street, Demo City, DC 12345', GETUTCDATE());
END
GO

PRINT 'Database setup complete!';
GO
