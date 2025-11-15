namespace Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int SaleDetailId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public bool IsActive { get; set; } 
        public ICollection<SaleDetail> SaleDetail { get; set; } 
        public ICollection<Payment> Payment { get; set; } 
        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } 
        public ICollection<TicketDetail> TicketDetail { get; set; } 
    }
}
