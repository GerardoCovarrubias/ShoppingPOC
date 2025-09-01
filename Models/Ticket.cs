namespace Models
{
    public class Ticket 
    {
        public int Id { get; set; }
        public int SaleId { get; set; } // Foreign key to Sales
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } // Nullable in case the ticket hasn't been updated yet
        public Sale Sale { get; set; } // Navigation property to Sale
        public ICollection<TicketDetail> TicketDetail { get; set; } // Navigation property to TicketDetails
    }
}
