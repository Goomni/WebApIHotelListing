using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApIHotelListing.Data;

namespace WebApIHotelListing.Configuration.Entity
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },

                new Country
                {
                    Id = 2,
                    Name = "Bahams",
                    ShortName = "BS"
                },

                new Country
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                });
        }
    }

}
