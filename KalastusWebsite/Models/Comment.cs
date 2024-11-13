using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Comment text cannot exceed 500 characters.")]
        public string Text { get; set; } // Kommentin sisältö

        [Required]
        public string Author { get; set; } // Kommentoijan nimi

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Kommentin luontiaika

        // Viittaus Media-tauluun
        public int? MediaId { get; set; } // Nullable, jos kommentti ei liity mediaan
        [ForeignKey("MediaId")]
        public Media Media { get; set; } // Navigaatio-ominaisuus mediaan

        // Viittaus Conversation-tauluun
        public int? ConversationId { get; set; } // Nullable, jos kommentti ei liity keskusteluun
        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; } // Navigaatio-ominaisuus keskusteluun
    }
}
