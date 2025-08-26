namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public Stock Stock { get; set; } // Navigation property to Stock
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } // Navigation property to PurchaseDetails
    
        public ICollection<SalesDetail> SalesDetail { get; set; } // Navigation property to SalesDetails
    }
  }


