using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Data;
using ContributorHubApi.Models.DTOs;

namespace ContributorHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _db.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                Rating = p.Rating,
                Reviews = p.Reviews
            }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();

            return Ok(new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                Rating = p.Rating,
                Reviews = p.Reviews
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var lower = query.ToLower();
            var products = await _db.Products
                .Where(p => p.Name.ToLower().Contains(lower) ||
                            p.Description.ToLower().Contains(lower) ||
                            p.Category.ToLower().Contains(lower))
                .Select(p => new ProductDto
                {
                    Id = p.Id, Name = p.Name, Description = p.Description,
                    Price = p.Price, Category = p.Category, ImageUrl = p.ImageUrl,
                    Stock = p.Stock, Rating = p.Rating, Reviews = p.Reviews
                }).ToListAsync();

            return Ok(products);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _db.Products.Select(p => p.Category).Distinct().ToListAsync();
            return Ok(categories);
        }
    }
}
