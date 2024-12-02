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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");

        public DbSet<Vote> Votes { get; set; }

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

        }
    }
}
