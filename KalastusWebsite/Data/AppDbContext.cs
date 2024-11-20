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
        public DbSet<Conversation> Conversations { get; set; } // Puuttuva Conversations
        public DbSet<Comment> Comments { get; set; } // Puuttuva Comments

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Suhteen määrittelyt Conversationsille ja Commentsille (jos tarpeen)
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Conversation)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
