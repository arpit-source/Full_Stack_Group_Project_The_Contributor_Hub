namespace ContributorHubApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
