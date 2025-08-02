using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } // e.g., Credit Card, PayPal, Bank Transfer
        public string Description { get; set; } // Optional description of the payment method
        public bool IsActive { get; set; } // Indicates if the payment method is currently active
        public DateTime CreatedAt { get; set; } // Timestamp when the payment method was created
        public DateTime? UpdatedAt { get; set; } // Nullable in case the payment method hasn't been updated yet
    }
}
