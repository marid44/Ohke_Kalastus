using KalastusWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace KalastusWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BioHistory> BioHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Media> MediaFiles { get; set; }
        public DbSet<MediaComment> MediaComments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }

        // New Fish entity DbSet
        public DbSet<Fish> Fishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Suhde: Conversation <-> Comments
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Suhde: Media <-> Votes
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Media)
                .WithMany(m => m.Votes)
                .HasForeignKey(v => v.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Yksilöllinen rajoite: Varmistaa, että yksi käyttäjä voi äänestää vain kerran per media
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.MediaId, v.UserId })
                .IsUnique();

            // Suhde: MediaComment <-> Media
            modelBuilder.Entity<MediaComment>()
                .HasOne(mc => mc.Media)
                .WithMany(m => m.Comments)
                .HasForeignKey(mc => mc.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Fish data
            modelBuilder.Entity<Fish>().HasData(
                new Fish
                {
                    Id = 1,
                    FinnishName = "Ahven",
                    EnglishName = "Perch",
                    Habitat = "Freshwater",
                    HabitatFI = "Makea vesi",
                    DescriptionFI = "Ahven elää järvissä, joissa on puhdasta vettä.",
                    DescriptionEN = "Perch lives in clean freshwater lakes.",
                    ImageUrl = "wwwroot/images/ahven.jpg"

                },
                new Fish
                {
                    Id = 2,
                    FinnishName = "Hauki",
                    EnglishName = "Pike",
                    Habitat = "Freshwater",
                    HabitatFI = "Makea vesi",
                    DescriptionFI = "Hauki on tunnettu petokala Suomessa.",
                    DescriptionEN = "Pike is a well-known predatory fish in Finland.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/44/Hecht.jpg"
                },
                new Fish
                {
                    Id = 3,
                    FinnishName = "Siika",
                    EnglishName = "Whitefish",
                    Habitat = "Brackish/Sea",
                    HabitatFI = "Murtovesi / Meri",
                    DescriptionFI = "Siika elää Itämeressä ja joissakin järvissä.",
                    DescriptionEN = "Whitefish lives in the Baltic Sea and some lakes.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a7/Coregonus_lavaretus.jpg"
                }
            );

        }
    }
}
