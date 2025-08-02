namespace Models
{
    public class Ticket 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } // Nullable in case the ticket hasn't been updated yet
        public string AssignedTo { get; set; } // User or team assigned to the ticket



    }
}
