using Microsoft.EntityFrameworkCore;
using YourApp.DALL.Models;

namespace YourApp.DALL
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SchoolDb;Integrated Security=True;Connect Timeout=30;");
            }
        }
    }
}
