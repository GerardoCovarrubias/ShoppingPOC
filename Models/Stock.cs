namespace Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
        public Stock(string productName, int quantity, string location, DateTime lastUpdated)
        {
            ProductName = productName;
            Quantity = quantity;
            Location = location;
            LastUpdated = lastUpdated;
        }
    }
}
