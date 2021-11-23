using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApIHotelListing.Data;

namespace WebApIHotelListing
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<AppDataContext>().AddDefaultTokenProviders();
        }

    }
}
