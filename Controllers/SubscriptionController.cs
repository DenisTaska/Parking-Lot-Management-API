using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpGet("GetAllSubscriptions")]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAllSubscriptions(string? code, string? subscriberName)
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptions(code, subscriberName);
            return Ok(_mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions));
        }
        
        [HttpGet("GetSingleSubscription{id}")]
        public async Task<ActionResult<Subscription>> GetSingleSubscription(int id)
        {
            var result = await _subscriptionService.GetSingleSubscription(id);
            if (result is null)
                return NotFound("Subscription not found.");

            return Ok(result);
        }

        [HttpPost("CreateSubscription")]
        public async Task<ActionResult<List<Subscription>>> CreateSubscription(
            SubscriptionForCreationDto subscriptionForCreationDto)
        {
            var result = await _subscriptionService.CreateSubscription(subscriptionForCreationDto);
            return Ok(result);
        }
        
        [HttpPut("UpdateSubscription/{id}")]
        public async Task<ActionResult<List<Subscription>>> UpdateSubscription(int id, SubscriptionForUpdateDto request)
        {
            var result = await _subscriptionService.UpdateSubscription(id, request);
            if (result is null)
                return NotFound("Subscription not found.");

            return Ok(result);
        }
        
        [HttpDelete("DeleteSubscription/{id}")]
        public async Task<ActionResult<List<Subscription>>> DeleteSubscription(int id)
        {
            var result = await _subscriptionService.DeleteSubscription(id);
            if (result is null)
                return NotFound("Subscription not found.");

            return Ok(result);
        }
    }
}