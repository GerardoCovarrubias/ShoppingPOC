namespace Models
{
    public class Sales
    { 
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
      
    }
}
