using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Purchases
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign key to Products
        public int Quantity { get; set; } // Number of products purchased
        public decimal TotalPrice { get; set; } // Total price for the purchase
        public DateTime PurchaseDate { get; set; } // Date of the purchase
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Status { get; set; } // e.g., Completed, Pending, Cancelled
        
    }
}
