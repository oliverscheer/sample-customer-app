using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.BusinessLogic.Database.Entities
{
    [EntityTypeConfiguration(typeof(CustomerConfiguration))]
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            string tableName = "Companies";
            builder.ToTable(tableName, "dbo");

            // Create 100 Sample Customers
            List<Customer> customers = [];
            for (int i = 0; i < 100; i++)
            {
                Customer newCustomer = new()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Customer {i}",
                    Description = $"Description {i}"
                };
                customers.Add(newCustomer);
            }

            builder.HasData(
                customers
            );
        }
    }
}
