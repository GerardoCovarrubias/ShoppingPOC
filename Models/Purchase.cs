namespace Models
{
    public class Purchase
    {
        public int Id { get; set; }
        DateTime UpdatedAt { get; set; } // Date and time of the purchase detail creation
        DateTime CreatedAT { get; set; } // Nullable in case the purchase detail hasn't been updated yet
        
        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } // Navigation property to PurchaseDetails
        
    }
}
