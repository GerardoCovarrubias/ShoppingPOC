namespace Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public string AssignedTo { get; set; } 
        public string Category { get; set; } 
        public string Priority { get; set; } 
    }
}
