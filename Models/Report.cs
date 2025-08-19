namespace Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // e.g., Open, In Progress, Closed
        public string AssignedTo { get; set; } // User or team assigned to the report
        public string Category { get; set; } // e.g., Bug, Feature Request, Improvement
        public string Priority { get; set; } // e.g., Low, Medium, High
    }
}
