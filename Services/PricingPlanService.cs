using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Services;

public class PricingPlanService : IPricingPlanService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PricingPlanService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PricingPlan>> GetAllPricingPlans()
    {
        var plans = await _context.PricingPlans.ToListAsync();
        return plans;
    }

    public async Task<PricingPlan?> UpdatePricingPlan(int id, PricingPlanForUpdateDto request)
    {
        var plan = await _context.PricingPlans.FindAsync(id);
        if (plan is null)
            return null;

        _mapper.Map(request, plan);

        await _context.SaveChangesAsync();

        return plan;
    }
}