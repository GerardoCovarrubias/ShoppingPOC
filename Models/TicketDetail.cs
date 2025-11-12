namespace Models
{
    public class TicketDetail
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; } 
        public int TicketId { get; set; } 
        public int SalesId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
        public Ticket Ticket { get; set; } 
    }
}
