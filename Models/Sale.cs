namespace Models
{
    public class Sale
    { 
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; } // Foreign key to Users
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Nullable in case the purchase detail hasn't been updated ye
        public DateTime? UpdatedAt { get; set; } // Date and time of the purchase detail creation
        public ICollection<SaleDetail> SaleDetail { get; set; } // Navigation property to SaleDetails
        public Ticket Ticket { get; set; } // Navigation property to Ticket
    }
}
