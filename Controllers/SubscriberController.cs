using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }

        [HttpGet("GetAllSubscribers")]
        public async Task<ActionResult<List<Subscriber>>> GetAllSubscribers(string? firstName, string? lastName, string? idCardNumber, string? email)
        {
            var subscribers = await _subscriberService.GetAllSubscribers(firstName, lastName, idCardNumber, email);
            return Ok(_mapper.Map<IEnumerable<Subscriber>>(subscribers));
        }
        
        [HttpGet("GetSingleSubscriber/{idCardNumber}")]
        public async Task<ActionResult<Subscriber>> GetSingleSubscriber(string idCardNumber)
        {
            var result = await _subscriberService.GetSingleSubscriber(idCardNumber);
            if (result is null)
                return NotFound("Subscriber not found.");

            return Ok(result);
        }

        [HttpPost("CreateSubscriber")]
        public async Task<ActionResult<List<Subscriber>>> CreateSubscriber(
            SubscriberForCreationDto subscriberForCreationDto)
        {
            var result = await _subscriberService.CreateSubscriber(subscriberForCreationDto);
            return Ok(result);
        }
        
        [HttpPut("UpdateSubscriber/{idCardNumber}")]
        public async Task<ActionResult<List<Subscriber>>> UpdateSubscriber(string idCardNumber, SubscriberForUpdateDto request)
        {
            var result = await _subscriberService.UpdateSubscriber(idCardNumber, request);
            if (result is null)
                return NotFound("Subscriber not found.");

            return Ok(result);
        }
        
        [HttpDelete("DeleteSubscriber/{idCardNumber}")]
        public async Task<ActionResult<List<Subscriber>>> DeleteSubscriber(string idCardNumber)
        {
            var result = await _subscriberService.DeleteSubscriber(idCardNumber);
            if (result is null)
                return NotFound("Subscriber not found.");

            return Ok(result);
        }
    }
}