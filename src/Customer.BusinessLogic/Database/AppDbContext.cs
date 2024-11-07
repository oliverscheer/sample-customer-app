using Customer.BusinessLogic.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.DatabaseLogic
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<BusinessLogic.Database.Entities.Customer> Customers { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }

    }
}
