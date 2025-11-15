namespace Models
{
    public class Sale
    { 
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedAt { get; set; } 
        public ICollection<SaleDetail> SaleDetail { get; set; } 
        public Ticket Ticket { get; set; } 
    }
}
