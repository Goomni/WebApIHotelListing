using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApIHotelListing.Data;

namespace WebApIHotelListing.Configuration.Entity
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
             new Hotel
             {
                 Id = 1,
                 Name = "Jamaica Negril Hotel",
                 Address = "Negril",
                 CountryId = 1,
                 Rating = 5
             },

             new Hotel
             {
                 Id = 2,
                 Name = "Bahamas George Town Hotel",
                 Address = "George Town",
                 CountryId = 2,
                 Rating = 4.5
             },

             new Hotel
             {
                 Id = 3,
                 Name = "CI Nassua Hotel",
                 Address = "Nassua",
                 CountryId = 3,
                 Rating = 4
             });
        }
    }

}
