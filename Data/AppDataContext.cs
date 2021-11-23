using Microsoft.EntityFrameworkCore;

namespace WebApIHotelListing.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {

        }

        #region 데이터 시딩
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
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

            builder.Entity<Hotel>().HasData(
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

        #endregion 데이터 시딩

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
