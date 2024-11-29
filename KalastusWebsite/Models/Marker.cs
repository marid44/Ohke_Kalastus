using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KalastusWebsite.Models
{
    public class Marker
    {
        // Primary key for the Marker
        public int Id { get; set; }

        // Geographical latitude of the marker
        [Required]
        public double Latitude { get; set; }

        // Geographical longitude of the marker
        [Required]
        public double Longitude { get; set; }

        // Foreign key linking the marker to the user who created it
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Optional name or description of the marker
        public string? MarkerName { get; set; }

        // Navigation property to the User entity
        public User User { get; set; }
    }
}
