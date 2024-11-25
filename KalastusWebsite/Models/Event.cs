namespace KalastusWebsite.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
    }
}
