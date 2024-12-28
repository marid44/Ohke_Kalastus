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
        public DbSet<Event> Events { get; set; }

        public DbSet<ConversationVote> ConversationVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        public DbSet<MediaVote> MediaVotes { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Marker> Markers { get; set; } // Add Marker DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships and constraints

            // Relationship: Conversation <-> Comments
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Media <-> Votes
            modelBuilder.Entity<MediaVote>()
                .HasOne(v => v.Media)
                .WithMany(m => m.Votes)
                .HasForeignKey(v => v.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique votes per media by a single user
            modelBuilder.Entity<MediaVote>()
                .HasIndex(v => new { v.MediaId, v.UserId })
                .IsUnique();

            // Relationship: MediaComment <-> Media
            modelBuilder.Entity<MediaComment>()
                .HasOne(mc => mc.Media)
                .WithMany(m => m.Comments)
                .HasForeignKey(mc => mc.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConversationVote>()
                .HasIndex(v => new { v.ConversationId, v.UserId })
                .IsUnique();

            modelBuilder.Entity<CommentVote>()
                .HasIndex(v => new { v.CommentId, v.UserId })
                .IsUnique();

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
                    ImageUrl = "/images/ahven.jpg"
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
                    ImageUrl = "/images/hauki.jpg"
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
                    ImageUrl = "/images/siika.jpg"
                }
            );

            // Relationship: Marker <-> User
            modelBuilder.Entity<Marker>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
