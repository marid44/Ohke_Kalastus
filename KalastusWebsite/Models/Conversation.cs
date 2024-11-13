using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public string Username { get; set; }  // To store which user posted the conversation

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Lista kommenteista, jotka liittyvät keskusteluun
        public List<Comment> Comments { get; set; } = new();
    }
    public class Comment1
    {
        public int Id { get; set; }

        [Required]
        public int ConversationId { get; set; } // Foreign key to Conversation

        [Required]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Text { get; set; }

        public string Username { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}



