using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class CommentVote : BaseVote
    {
        public int CommentId { get; set; }
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
    }
}