using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Services.Interfaces;

public interface IPricingPlanService
{
    Task<List<PricingPlan>> GetAllPricingPlans();
    Task<PricingPlan?> UpdatePricingPlan(int id, PricingPlanForUpdateDto request);
}