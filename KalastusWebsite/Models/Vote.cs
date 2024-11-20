using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ConversationId { get; set; }
        public int? CommentId { get; set; }
        public bool IsUpvote { get; set; }
        public DateTime CreatedAt { get; set; }

        public Conversation? Conversation { get; set; }
        public Comment? Comment { get; set; }
    }
}
