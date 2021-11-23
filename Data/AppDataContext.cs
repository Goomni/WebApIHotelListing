using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApIHotelListing.Configuration.Entity;

namespace WebApIHotelListing.Data
{
    public class AppDataContext : IdentityDbContext<ApiUser>
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {

        }

        #region 데이터 시딩
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
        }

        #endregion 데이터 시딩

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
