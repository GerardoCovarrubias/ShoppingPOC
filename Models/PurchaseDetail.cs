

namespace Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; } 
        public int ProductId { get; set; } 
        public int PurchaseId { get; set; } 
        public int PaymentMethodId { get; set; } 
        public int Quantity { get; set; } 
        public DateTime CreatedAT { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } 
        public Product Product { get; set; } 
        public Purchase Purchase { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
    }
}

