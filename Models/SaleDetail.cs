
namespace Models
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string ProductId { get; set; }
        public int PaymentMethodId { get; set; }
        public int QuantitySold { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Product Product { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public Sale Sale { get; set; }

    }
}
