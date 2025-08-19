

namespace Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }
        public int PaymentMethodId { get; set; } // Foreign key to Payments
        public int ProductId { get; set; } // Foreign key to Products
        public int PurchaseId { get; set; } // Foreign key to Purchases
        public int Quantity { get; set; } // Number of products purchased
        public decimal TotalPrice { get; set; } // Total price for the purchase
        public DateTime PurchaseDate { get; set; } // Date of the purchase
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Status { get; set; } // e.g., Completed, Pending, Cancelled
        public DateTime UpdatedAt { get; set; } // Date and time of the purchase detail creation
        public DateTime? CreatedAT { get; set; } // Nullable in case the purchase detail hasn't been updated yet
        public Product Product { get; set; } // Navigation property to Product
    }
}

