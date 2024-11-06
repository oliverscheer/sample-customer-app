using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.DatabaseLogic.Entities
{
    [EntityTypeConfiguration(typeof(CustomerConfiguration))]
    public class Customer
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            string tableName = "Companies";
            builder.ToTable(tableName, "dbo");
        }
    }
}
