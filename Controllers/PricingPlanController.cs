using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PricingPlanController : Controller
    {
        
        private readonly IPricingPlanService _pricingPlanService;
        private readonly IMapper _mapper;


        public PricingPlanController(IPricingPlanService pricingPlanService, IMapper mapper)
        {
            _pricingPlanService = pricingPlanService;
            _mapper = mapper;
        }
        
        [HttpGet("GetAllPricingPlans")]
        public async Task<ActionResult<List<PricingPlan>>> GetAllPricingPlans()
        {
            var pricingPlans = await _pricingPlanService.GetAllPricingPlans();
            return Ok(_mapper.Map<IEnumerable<PricingPlan>>(pricingPlans));
        }
        
        [HttpPut("UpdatePricingPlan/{id}")]
        public async Task<ActionResult<List<PricingPlan>>> UpdatePricingPlan(int id, PricingPlanForUpdateDto request)
        {
            var result = await _pricingPlanService.UpdatePricingPlan(id, request);
            if (result is null)
                return NotFound("Plan not found.");

            return Ok(result);
        }
    }
}