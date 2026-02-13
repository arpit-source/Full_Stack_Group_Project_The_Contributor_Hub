using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContributorHubApi.Data;
using ContributorHubApi.Models;
using ContributorHubApi.Models.DTOs;

namespace ContributorHubApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db) => _db = db;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password,
                Name = dto.Name,
                Phone = dto.Phone,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address,
                CreatedAt = user.CreatedAt
            });
        }
    }
}
