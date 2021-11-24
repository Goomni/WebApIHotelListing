using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApIHotelListing.Configuration;
using WebApIHotelListing.Data;
using WebApIHotelListing.Models;

namespace WebApIHotelListing.Services
{
    public class JWTAuthService : IAuthService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public JWTAuthService(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var credentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(credentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtConfig>();
            var token = new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(jwtSettings.LifeTime),
                    signingCredentials: credentials
                );

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("JWT_KEY", EnvironmentVariableTarget.Machine);
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)                
            };

            var roles = await _userManager.GetRolesAsync(_user);
            if(roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims;
        }

        public async Task<bool> ValidateUser(LoginDTO userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            if( _user == null )
            {
                return false;
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
            if( isValidPassword == false )
            {
                return false;
            }

            return true;
        }

    }
}
