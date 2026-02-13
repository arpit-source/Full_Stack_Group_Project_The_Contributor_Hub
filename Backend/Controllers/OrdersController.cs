using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Data;
using ContributorHubApi.Models;
using ContributorHubApi.Models.DTOs;

namespace ContributorHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IServiceScopeFactory _scopeFactory;

        public OrdersController(AppDbContext db, IServiceScopeFactory scopeFactory)
        {
            _db = db;
            _scopeFactory = scopeFactory;
        }

        private int GetUserId() => int.Parse(Request.Headers["X-User-Id"].ToString());

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = GetUserId();
            var orders = await _db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders.Select(MapToDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var userId = GetUserId();
            var order = await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null) return NotFound();
            return Ok(MapToDto(order));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var userId = GetUserId();

            var cartItems = await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (cartItems.Count == 0)
                return BadRequest(new { message = "Cart is empty" });

            var order = new Order
            {
                UserId = userId,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                Status = "Pending",
                PaymentMethod = $"Card ending in {dto.PaymentInfo.CardNumber[^4..]}",
                ShippingAddress = dto.ShippingAddress,
                OrderDate = DateTime.UtcNow,
                Items = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    ProductPrice = c.Product.Price,
                    ProductImageUrl = c.Product.ImageUrl,
                    ProductCategory = c.Product.Category,
                    Quantity = c.Quantity
                }).ToList()
            };

            _db.Orders.Add(order);
            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();

            _ = Task.Run(() => ProgressOrderStatus(order.Id));

            return Ok(MapToDto(order));
        }

        private async Task ProgressOrderStatus(int orderId)
        {
            await Task.Delay(10000);
            await UpdateStatus(orderId, "Pending", "Processing");

            await Task.Delay(10000);
            await UpdateStatus(orderId, "Processing", "Shipped");

            await Task.Delay(10000);
            await UpdateStatus(orderId, "Shipped", "Delivered");
        }

        private async Task UpdateStatus(int orderId, string expectedStatus, string newStatus)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var order = await db.Orders.FindAsync(orderId);
            if (order != null && order.Status == expectedStatus)
            {
                order.Status = newStatus;
                if (newStatus == "Delivered")
                    order.DeliveryDate = DateTime.UtcNow;
                await db.SaveChangesAsync();
            }
        }

        private static OrderDto MapToDto(Order o) => new()
        {
            Id = o.Id,
            UserId = o.UserId,
            TotalAmount = o.TotalAmount,
            Status = o.Status,
            PaymentMethod = o.PaymentMethod,
            ShippingAddress = o.ShippingAddress,
            OrderDate = o.OrderDate,
            DeliveryDate = o.DeliveryDate,
            Items = o.Items.Select(i => new CartItemDto
            {
                Quantity = i.Quantity,
                Product = new ProductDto
                {
                    Id = i.ProductId,
                    Name = i.ProductName,
                    Price = i.ProductPrice,
                    ImageUrl = i.ProductImageUrl,
                    Category = i.ProductCategory
                }
            }).ToList()
        };
    }
}
