using System.ComponentModel.DataAnnotations;

namespace KalastusWebsite.Models
{
    public class BaseVote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsUpVote { get; set; }
    }
}