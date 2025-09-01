namespace Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int SaleDetailId { get; set; }
        public string Name { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Description { get; set; } // Optional description of the payment method
        public bool IsActive { get; set; } // Indicates if the payment method is currently active
        public ICollection<SaleDetail> SaleDetail { get; set; } // Navigation property to SaleDetails7
        public ICollection<Payment> Payment { get; set; } // Navigation property to Payments
        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } // Navigation property to PurchaseDetails
        public ICollection<TicketDetail> TicketDetail { get; set; } // Navigation property to TicketDetails
    }
}
