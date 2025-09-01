namespace Models
{
    public class Payment
    {
        
        public int Id { get; set; }

        public int PurchaseId { get; set; } // Foreign key to Purchases

        public int PaymentMethodId { get; set; } // Foreign key to PaymentMethods

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Nullable in case the purchase detail hasn't been updated ye
        public DateTime? UpdatedAt { get; set; } // Date and time of the purchase detail creation
        public Purchase Purchase { get; set; } // Navigation property to Purchase

        public PaymentMethod PaymentMethod { get; set; } // Navigation property to PaymentMethod
    }
}
