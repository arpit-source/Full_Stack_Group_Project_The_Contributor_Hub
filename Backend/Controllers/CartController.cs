using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Data;
using ContributorHubApi.Models;
using ContributorHubApi.Models.DTOs;

namespace ContributorHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db) => _db = db;

        private int GetUserId() => int.Parse(Request.Headers["X-User-Id"].ToString());

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            return Ok(await GetCartItems(GetUserId()));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = GetUserId();
            var existing = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == dto.ProductId);

            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
            }
            else
            {
                _db.CartItems.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await _db.SaveChangesAsync();
            return Ok(await GetCartItems(userId));
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateQuantity(int productId, UpdateCartDto dto)
        {
            var userId = GetUserId();
            var item = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (item == null) return NotFound();

            if (dto.Quantity <= 0)
                _db.CartItems.Remove(item);
            else
                item.Quantity = dto.Quantity;

            await _db.SaveChangesAsync();
            return Ok(await GetCartItems(userId));
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var userId = GetUserId();
            var item = await _db.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }

            return Ok(await GetCartItems(userId));
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();
            var items = await _db.CartItems.Where(c => c.UserId == userId).ToListAsync();
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
            return Ok(new List<CartItemDto>());
        }

        private async Task<List<CartItemDto>> GetCartItems(int userId)
        {
            return await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Select(c => new CartItemDto
                {
                    Quantity = c.Quantity,
                    Product = new ProductDto
                    {
                        Id = c.Product.Id,
                        Name = c.Product.Name,
                        Description = c.Product.Description,
                        Price = c.Product.Price,
                        Category = c.Product.Category,
                        ImageUrl = c.Product.ImageUrl,
                        Stock = c.Product.Stock,
                        Rating = c.Product.Rating,
                        Reviews = c.Product.Reviews
                    }
                }).ToListAsync();
        }
    }
}
