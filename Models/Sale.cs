namespace Models
{
    public class Sale
    { 
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; } // Foreign key to Users
        public DateTime UpdatedAt { get; set; } // Date and time of the purchase detail creation
        public DateTime? CreatedAT { get; set; } // Nullable in case the purchase detail hasn't been updated ye

    }
}
