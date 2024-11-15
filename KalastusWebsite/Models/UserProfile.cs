namespace KalastusWebsite.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImagePath { get; set; } = "/images/default-profile.png";
        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public List<BioHistory> BioHistory { get; set; } = new();
    }

    public class BioHistory
    {
        public int Id { get; set; }
        public string? BioText { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
