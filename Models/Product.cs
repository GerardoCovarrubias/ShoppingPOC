namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public Stock Stock { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetail { get; set; } 
    
        public ICollection<SaleDetail> SaleDetail { get; set; } 
    }
  }


