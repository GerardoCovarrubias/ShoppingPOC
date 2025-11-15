namespace Models
{
    public class Purchase
    {
        public int Id { get; set; }
        DateTime UpdatedAt { get; set; } 
        DateTime CreatedAT { get; set; } 
        
        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } 
        
        public ICollection<Payment> Payment { get; set; } 
    }
}
