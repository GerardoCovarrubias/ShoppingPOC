namespace Models
{
    public class TicketDetail
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; } // Foreign key to Payments
        public int TicketId { get; set; } // Foreign key to Ticket
        public int SalesId { get; set; } // Foreign key to Sales
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } // Nullable in case the ticket hasn't been updated yet

    }
}
