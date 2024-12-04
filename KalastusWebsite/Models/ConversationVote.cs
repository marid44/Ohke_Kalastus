using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class ConversationVote : BaseVote
    {
        public int ConversationId { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }
    }
}	