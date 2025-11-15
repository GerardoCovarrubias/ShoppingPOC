namespace Models
{
    public class Payment
    {  
        public int Id { get; set; }
        public int PurchaseId { get; set; } 
        public int PaymentMethodId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedAt { get; set; } 
        public Purchase Purchase { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
    }
}
