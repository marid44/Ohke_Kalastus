using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KalastusWebsite.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use SQLite connection string (adjust path as needed)
            optionsBuilder.UseSqlite("Data Source=app.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
