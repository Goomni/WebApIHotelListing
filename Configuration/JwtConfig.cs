using System;

namespace WebApIHotelListing.Configuration
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double LifeTime { get; set; }

    }
}
