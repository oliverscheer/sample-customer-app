using Customer.DatabaseLogic.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Customer.DatabaseLogic.Entities
{
    [EntityTypeConfiguration(typeof(CountryConfiguration))]
    public class Country
    {
        [Key]
        [StringLength(2)]
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            string tableName = "Countries";
            builder.ToTable(tableName, "dbo");

            // Unique Index
            // No duplicate Country Name allowed
            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            // Seeding All Countries
            List<Country> countries = [];
            foreach (var country in Countries.All)
            {
                Country newCountry = new()
                {
                    Id = country.Value,
                    Name = country.Key
                };
                countries.Add(newCountry);
            }

            builder.HasData(
                countries
            );
        }
    }
}
