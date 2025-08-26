namespace Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Description { get; set; } // Optional description of the payment method
        public bool IsActive { get; set; } // Indicates if the payment method is currently active
    }
}
