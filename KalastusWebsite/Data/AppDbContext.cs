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

        public DbSet<Conversation> Conversations { get; set; } // Keskustelut
        public DbSet<Comment> Comments { get; set; } // Kommentit
        public DbSet<Vote> Votes { get; set; } // Äänet

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Suhde: Conversation <-> Comments

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);


            // Vote-entiteetin konfigurointi
            modelBuilder.Entity<Vote>(entity =>
            {
                // Erilliset indeksit äänestyksen uniikeille yhdistelmille
                entity.HasIndex(v => new { v.UserId, v.ConversationId })
                    .IsUnique()
                    .HasFilter("[ConversationId] IS NOT NULL");

                entity.HasIndex(v => new { v.UserId, v.CommentId })
                    .IsUnique()
                    .HasFilter("[CommentId] IS NOT NULL");

                // Conversation-relaatio
                entity.HasOne(v => v.Conversation)
                    .WithMany()
                    .HasForeignKey(v => v.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(false);

                // Comment-relaatio
                entity.HasOne<Comment>()
                    .WithMany()
                    .HasForeignKey(v => v.CommentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(false);
            });

        }
    }
}
