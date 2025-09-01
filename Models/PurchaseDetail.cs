

namespace Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; } //   public int PaymentMethodId { get; set; } // Foreign key to Payments
        public int ProductId { get; set; } // Foreign key to Products
        public int PurchaseId { get; set; } // Foreign key to Purchases
        public int PaymentMethodId { get; set; } // Foreign key to PaymentMethods
        public int Quantity { get; set; } // Number of products purchased
        public DateTime CreatedAT { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } // Date and time of the purchase detail creation
        public Product Product { get; set; } // Navigation property to Product
        public Purchase Purchase { get; set; }
        public PaymentMethod PaymentMethod { get; set; } // Navigation property to PaymentMethod
    }
}

