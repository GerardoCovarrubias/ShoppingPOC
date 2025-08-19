using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Purchase
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; } // Number of products purchased
         
        public decimal TotalPrice { get; set; } // Total price for the purchase
        public DateTime PurchaseDate { get; set; } // Date of the purchase
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Status { get; set; } // e.g., Completed, Pending, Cancelled
        DateTime UpdatedAt { get; set; } // Date and time of the purchase detail creation
        DateTime? CreatedAT { get; set; } // Nullable in case the purchase detail hasn't been updated yet

    }
}
