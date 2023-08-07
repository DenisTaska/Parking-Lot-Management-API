using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMapper _mapper;
        
        public LogController(ILogService logService, IMapper mapper)
        {
            _logService = logService;
            _mapper = mapper;
        }

        [HttpGet("GetAllLogs")]
        public async Task<ActionResult<List<LogDto>>> GetAllLogs(string? code, string? subscriberFirstName)
        {
            var logs = await _logService.GetAllLogs(code, subscriberFirstName);
            return Ok(_mapper.Map<IEnumerable<LogDto>>(logs));
        }

        [HttpGet("GetLogsForDay/{date}")]
        public async Task<ActionResult<List<Log>>> GetLogsForDay(DateTime date)
        {
            var logs = await _logService.GetAllLogsForDay(date);
            return Ok(logs);
        }

        [HttpPost("CreateLog")]
        public async Task<ActionResult<List<LogForResponseDto>>> CreateLog(
            LogForCreationDto logForCreationDto)
        {
            var result = await _logService.CreateLog(logForCreationDto);
            return Ok(result);
        }

        [HttpDelete("CheckOutLog/{id}")]
        public async Task<ActionResult<List<Log>>> CheckOutLog(int id)
        {
            var result = await _logService.CheckOutLog(id);
            if (result is null)
                return NotFound("Subscription not found.");

            return Ok(result);
        }
    }
}