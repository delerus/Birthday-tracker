using Microsoft.EntityFrameworkCore;
using Birthday_tracker.Models;

namespace Birthday_tracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public IConfiguration _config { get; set; }

        public ApplicationDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }

        public DbSet<Birthday> Birthdays { get; set; }
    }
}