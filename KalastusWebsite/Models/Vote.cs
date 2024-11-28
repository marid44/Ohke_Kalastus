using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int MediaId { get; set; }

        [Required]
        public string UserId { get; set; } // Käyttäjän yksilöivä ID

        public bool IsUpVote { get; set; } // True = UpVote, False = DownVote

        [ForeignKey(nameof(MediaId))]
        public Media Media { get; set; }
    }
}
