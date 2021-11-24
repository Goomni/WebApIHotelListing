using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApIHotelListing.Data;
using WebApIHotelListing.IRepository;
using WebApIHotelListing.Models;

namespace WebApIHotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unitOfWork, ILogger<HotelController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var result = _mapper.Map<IList<HotelDTO>>(hotels);
                _logger.LogInformation($"[{nameof(HotelController)}/{nameof(GetHotels)}] {hotels}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(HotelController)}/{nameof(GetHotels)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }

        static readonly List<string> include = new () { "Country" };

        [Authorize]
        [HttpGet("{hotelID:int}", Name = "GetHotel")]        
        public async Task<IActionResult> GetHotel([FromRoute] int hotelID)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(x => x.Id == hotelID, include);
                var result = _mapper.Map<HotelDTO>(hotel);
                _logger.LogInformation($"[{nameof(HotelController)}/{nameof(GetHotel)}] {hotel}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(HotelController)}/{nameof(GetHotel)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
        {
            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetHotel", new { hotelID = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(HotelController)}/{nameof(CreateHotel)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] CreateHotelDTO hotelDTO)
        {
            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetHotel", new { hotelID = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(HotelController)}/{nameof(CreateHotel)}] Something Wrong!");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
