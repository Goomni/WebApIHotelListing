using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApIHotelListing.IRepository;
using WebApIHotelListing.Models;

namespace WebApIHotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]        
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var result = _mapper.Map<IList<CountryDTO>>(countries);
                _logger.LogInformation($"[{nameof(CountryController)}/{nameof(GetCountries)}] {countries}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(CountryController)}/{nameof(GetCountries)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }

        static readonly List<string> includes = new() { "Hotels" };

        [HttpGet("{countryID:int}")]
        public async Task<IActionResult> GetCountry([FromRoute] int countryID)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(x => x.Id == countryID, includes);
                var result = _mapper.Map<CountryDTO>(country);
                _logger.LogInformation($"[{nameof(CountryController)}/{nameof(GetCountries)}] {country}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(CountryController)}/{nameof(GetCountries)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
