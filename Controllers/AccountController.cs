using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApIHotelListing.Data;
using WebApIHotelListing.Models;

namespace WebApIHotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;            
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);                
                if(result.Succeeded == false)
                {
                    _logger.LogError($"Registration Attempt for {userDto.Email} Failed!!!!! ModelState: {ModelState}");
                    return BadRequest("Registration Failed!");
                }

                await _userManager.AddToRolesAsync(user, userDto.Roles);
                return Accepted("Registration Succeeded");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Registration Attempt for {userDto.Email} Failed!!!!!");
                return Problem($"Registration Attempt for {userDto.Email} Failed!!!!!", statusCode: 500);
            }
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        //{
        //    _logger.LogInformation($"Login Attempt for {loginDto.Email}");

        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
        //        if (result.Succeeded == false)
        //        {
        //            _logger.LogError($"Login Attempt for {loginDto.Email} Failed!!!!! ModelState: {ModelState}");
        //            return Unauthorized("Login Failed!");
        //        }
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Login Attempt for {loginDto.Email} Failed!!!!!");
        //        return Problem($"Login Attempt for {loginDto.Email} Failed!!!!!", statusCode: 500);
        //    }
        //}
    }
}
