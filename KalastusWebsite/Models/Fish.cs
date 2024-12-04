namespace KalastusWebsite.Models
{
    public class Fish
    {
        public int Id { get; set; }
        public string FinnishName { get; set; }
        public string EnglishName { get; set; }
        public string Habitat { get; set; } // Example: Freshwater, Saltwater, Brackish
        public string HabitatFI { get; set; } // Habitat in Finnish
        public string DescriptionFI { get; set; } // Description in Finnish
        public string DescriptionEN { get; set; } // Description in English
        public string ImageUrl { get; set; } // Wikipedia or other source image URL
    }

}