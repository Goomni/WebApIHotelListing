using Microsoft.AspNetCore.Identity;

namespace WebApIHotelListing.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
