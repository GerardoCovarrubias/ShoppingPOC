
namespace Models
{
    public class SalesDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string ProductId { get; set; }
        public int QuantitySold { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Product Product { get; set; }

    }
}
