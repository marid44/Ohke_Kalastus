using Microsoft.AspNetCore.Components.Forms;
using System;

namespace KalastusWebsite.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public string UploadedBy { get; set; }
        public List<MediaComment> Comments { get; set; } = new();

    }

    // Add the MediaUploadModel class here
    public class MediaUploadModel
    {
        public IBrowserFile? File { get; set; }
    }
}
