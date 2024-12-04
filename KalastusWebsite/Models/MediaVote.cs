using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class MediaVote : BaseVote
    {
        public int MediaId { get; set; }
        [ForeignKey(nameof(MediaId))]
        public Media Media { get; set; }
    }
}