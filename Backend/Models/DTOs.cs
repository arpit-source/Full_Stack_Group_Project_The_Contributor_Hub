namespace ContributorHubApi.Models.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateCartDto
    {
        public int Quantity { get; set; }
    }

    public class CartItemDto
    {
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public double Rating { get; set; }
        public int Reviews { get; set; }
    }

    public class CreateOrderDto
    {
        public PaymentInfoDto PaymentInfo { get; set; } = null!;
        public string ShippingAddress { get; set; } = string.Empty;
    }

    public class PaymentInfoDto
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolder { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
