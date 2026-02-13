using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Models;

namespace ContributorHubApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            // Seed 8 products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Wireless Headphones", Description = "Premium wireless headphones with noise cancellation and 30-hour battery life", Price = 2499, Category = "Electronics", ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", Stock = 50, Rating = 4.5, Reviews = 234 },
                new Product { Id = 2, Name = "Smart Watch", Description = "Fitness tracker with heart rate monitor and GPS", Price = 4999, Category = "Electronics", ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500", Stock = 30, Rating = 4.7, Reviews = 456 },
                new Product { Id = 3, Name = "Running Shoes", Description = "Lightweight running shoes with cushioned sole", Price = 3499, Category = "Sports", ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500", Stock = 100, Rating = 4.3, Reviews = 789 },
                new Product { Id = 4, Name = "Laptop Backpack", Description = "Water-resistant laptop backpack with USB charging port", Price = 1299, Category = "Accessories", ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=500", Stock = 75, Rating = 4.6, Reviews = 345 },
                new Product { Id = 5, Name = "Coffee Maker", Description = "Programmable coffee maker with thermal carafe", Price = 2999, Category = "Home", ImageUrl = "https://images.unsplash.com/photo-1517668808822-9ebb02f2a0e6?w=500", Stock = 40, Rating = 4.4, Reviews = 567 },
                new Product { Id = 6, Name = "Yoga Mat", Description = "Non-slip eco-friendly yoga mat with carrying strap", Price = 899, Category = "Sports", ImageUrl = "https://images.unsplash.com/photo-1601925260368-ae2f83cf8b7f?w=500", Stock = 120, Rating = 4.8, Reviews = 234 },
                new Product { Id = 7, Name = "Bluetooth Speaker", Description = "Portable waterproof speaker with 12-hour battery", Price = 1999, Category = "Electronics", ImageUrl = "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=500", Stock = 60, Rating = 4.5, Reviews = 432 },
                new Product { Id = 8, Name = "Desk Lamp", Description = "LED desk lamp with adjustable brightness and color temperature", Price = 1499, Category = "Home", ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500", Stock = 85, Rating = 4.2, Reviews = 123 }
            );
        }
    }
}
