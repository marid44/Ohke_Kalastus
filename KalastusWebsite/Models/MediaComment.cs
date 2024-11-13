using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class MediaComment
    {
        public int Id { get; set; }

        [Required]
        public int MediaId { get; set; } // Viittaus mediaan

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Text { get; set; }

        public string Username { get; set; } // Kommentin kirjoittaja
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigaatio-ominaisuus takaisin mediaan
        public Media Media { get; set; }
    }
}
