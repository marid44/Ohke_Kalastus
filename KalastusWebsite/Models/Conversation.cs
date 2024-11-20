using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Username { get; set; } // Keskustelun kirjoittaja
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Kommentit, jotka liittyvät keskusteluun
        public List<Comment> Comments { get; set; } = new();
    }
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; } // Viittaus keskusteluun

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Text { get; set; }

        public string Username { get; set; } // Kommentin kirjoittaja
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int Upvotes { get; set; } = 0; 
        public int Downvotes { get; set; } = 0; 

        // Navigaatio-ominaisuus takaisin keskusteluun
        public Conversation Conversation { get; set; }
    }


}