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
}
