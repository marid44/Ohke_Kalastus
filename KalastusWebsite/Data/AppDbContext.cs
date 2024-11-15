using KalastusWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace KalastusWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // Lisää tämä

        public DbSet<Media> MediaFiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MediaComment> MediaComments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}