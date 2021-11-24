using System.Threading.Tasks;
using WebApIHotelListing.Models;

namespace WebApIHotelListing.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateUser(LoginDTO userDTO);
        Task<string> CreateToken();
    }

}
