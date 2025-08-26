namespace Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } // Navigation property to Product
        public int Quantity { get; set; }
        public DateTime CreatedAT { get; set; } = DateTime.UtcNow; // Nullable in case the purchase detail hasn't been updated yet
        public DateTime? UpdatedAt { get; set; } // Date and time of the purchase detail creation
    }
}
