using Customer.DatabaseLogic.Const;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Customer.BusinessLogic.Database.Entities
{
    [EntityTypeConfiguration(typeof(CountryConfiguration))]
    public class CountryEntity : BaseEntity
    {
        public string Name { get; set; } = default!;
        [StringLength(2)]
        public string Code { get; set; } = default!;
    }

    internal class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
    {
        public void Configure(EntityTypeBuilder<CountryEntity> builder)
        {
            string tableName = "Countries";
            builder.ToTable(tableName, "dbo");

            // Unique Index
            // No duplicate Country Name allowed
            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            // Seeding All Countries
            List<CountryEntity> countries = [];
            foreach (var country in Countries.All)
            {
                CountryEntity newCountry = new()
                {
                    Id = country.Id,
                    Name = country.Name,
                    Code = country.Code

                };
                countries.Add(newCountry);
            }

            builder.HasData(
                countries
            );
        }
    }
}
