using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class ParkingSpotController : Controller
    {
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IMapper _mapper;


        public ParkingSpotController(IParkingSpotService parkingSpotService, IMapper mapper)
        {
            _parkingSpotService = parkingSpotService;
            _mapper = mapper;
        }

        [HttpGet("GetNumberOfAllSpots")]
        public async Task<ActionResult<object>> GetNumberOfAllSpots()
        {
            var totalSpots = await _parkingSpotService.GetAllSpots();

            return Ok(new {TotalCount = totalSpots.Count });
        }

        [HttpGet("GetNumberOfReservedSpots")]
        public async Task<ActionResult<object>> GetNumberOfReservedSpots()
        {
            var reservedSpots = await _parkingSpotService.GetNumberOfReservedSpots();

            return Ok(reservedSpots);
        }
        
        [HttpGet("GetNumberOfRegularSpots")]
        public async Task<ActionResult<object>> GetNumberOfRegularSpots()
        {
            var regularSpots = await _parkingSpotService.GetNumberOfRegularSpots();

            return Ok(regularSpots);
        }
        
        [HttpGet("GetNumberOfAllOccupiedRegularSpots")]
        public async Task<ActionResult<object>> GetAllOccupiedRegularSpots()
        {
            var totalOccupiedRegularSpotsSpots = await _parkingSpotService.GetNumberOfOccupiedRegularSpots();

            return Ok(totalOccupiedRegularSpotsSpots);
        }

        [HttpGet("GetNumberOfAllFreeRegularSpots")]
        public async Task<ActionResult<object>> GetAllFreeRegularSpots()
        {
            var totalFreeRegularSpotsSpots = await _parkingSpotService.GetNumberOfFreeRegularSpots();

            return Ok(totalFreeRegularSpotsSpots);
        }
        
        [HttpPost("CreateParkingSpot")]
        public async Task<OkObjectResult> CreateParkingSpot()
        {
            var result = await _parkingSpotService.CreateParkingSpot();
            return Ok(result);
        }
        
        [HttpDelete("DeleteParkingSlot/{id}")]
        public async Task<ActionResult<List<ParkingSpot>>> DeleteParkingSlot(int id)
        {
            var result = await _parkingSpotService.DeleteParkingSpot(id);
            if (result is null)
                return NotFound("Parking Lot is not found.");

            return Ok(result);
        }
    }
}
