namespace Models
{
    public class Ticket 
    {
        public int Id { get; set; }
        public int SaleId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } 
        public Sale Sale { get; set; } 
        public ICollection<TicketDetail> TicketDetail { get; set; } 
    }
}
