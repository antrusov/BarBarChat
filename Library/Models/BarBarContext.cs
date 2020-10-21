using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class BarBarContext : DbContext
    {
        public BarBarContext(DbContextOptions<BarBarContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}