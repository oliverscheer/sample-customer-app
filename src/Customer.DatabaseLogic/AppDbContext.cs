using Microsoft.EntityFrameworkCore;

namespace Customer.DatabaseLogic
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Entities.Country> Countries { get; set; }

    }
}
