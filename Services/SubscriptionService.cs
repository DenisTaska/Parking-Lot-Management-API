using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;
using DayType = Parking_Lot_Management.API.Entities.DayType;

namespace Parking_Lot_Management.API.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public SubscriptionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SubscriptionDto>> GetAllSubscriptions()
    {
        var subscriptions = await _context.Subscriptions
            .Select(s => new SubscriptionDto
            {
                Id = s.Id,
                Code = s.Code,
                Price = s.Price,
                DiscountedValue = s.DiscountedValue,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IdCardNumber = s.Subscriber.IdCardNumber,
                IsDeleted = s.IsDeleted
            })
            .ToListAsync();

        return subscriptions;
    }

    public async Task<List<SubscriptionDto>> GetAllSubscriptions(string? code, string? subscriberName)
    {
        IQueryable<Subscription> query = _context.Subscriptions;

        if (!string.IsNullOrEmpty(code))
        {
            code = code.Trim();
            query = query.Where(c => c.Code == code);
        }

        if (!string.IsNullOrEmpty(subscriberName))
        {
            subscriberName = subscriberName.Trim();
            query = query.Where(c => c.Subscriber.FirstName == subscriberName);
        }

        var subscriptions = await query
            .Select(s => new SubscriptionDto
            {
                Id = s.Id,
                Code = s.Code,
                Price = s.Price,
                DiscountedValue = s.DiscountedValue,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IdCardNumber = s.Subscriber.IdCardNumber,
                IsDeleted = s.IsDeleted
            })
            .OrderBy(s => s.Code) // You can order by any other property if needed
            .ToListAsync();

        return subscriptions;
    }

    public async Task<Subscription> GetSingleSubscription(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription is null)
            return null;

        return subscription;
    }

    public async Task<List<Subscription>?> DeleteSubscription(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription is null)
            return null;

        subscription.IsDeleted = true;
        await _context.SaveChangesAsync();

        return await _context.Subscriptions.ToListAsync();
    }
    
    public async Task<List<Subscription>> CreateSubscription(SubscriptionForCreationDto subscriptionForCreationDto)
    {
        if (await _context.Subscriptions.AnyAsync(s => s.Code == subscriptionForCreationDto.Code))
        {
            throw new Exception(
                $"The Subscription with the code: {subscriptionForCreationDto.Code} already exists");
        }

        DayType dayType = GetDayType(subscriptionForCreationDto.StartDate);
        var pricingPlan = await GetPricingPlanByDayType(dayType);

        if (pricingPlan == null)
        {
            throw new Exception($"Pricing plan not found for DayType: {dayType}");
        }

        decimal suggestedPrice = CalculateSubscriptionPrice(pricingPlan, subscriptionForCreationDto.StartDate,
            subscriptionForCreationDto.EndDate, subscriptionForCreationDto.DiscountedValue);


        var subscription = _mapper.Map<Subscription>(subscriptionForCreationDto);

        subscription.Price = suggestedPrice;

        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(s => s.IdCardNumber == subscriptionForCreationDto.IdCardNumber);
        if (subscriber == null)
        {
            throw new Exception($"Subscriber not found with IdCardNumber: {subscriptionForCreationDto.IdCardNumber}");
        }

        subscription.Subscriber = subscriber;

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
        return await _context.Subscriptions.ToListAsync();
    }
    
    
    public async Task<Subscription?> UpdateSubscription(int id, SubscriptionForUpdateDto request)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription is null)
            return null;
        
        DayType dayType = GetDayType(request.StartDate);
        var pricingPlan = await GetPricingPlanByDayType(dayType);

        if (pricingPlan == null)
        {
            throw new Exception($"Pricing plan not found for DayType: {dayType}");
        }

        decimal suggestedPrice = CalculateSubscriptionPrice(pricingPlan, request.StartDate,
            request.EndDate, request.DiscountedValue);


        _mapper.Map(request, subscription);

        subscription.Price = suggestedPrice;

        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(s => s.IdCardNumber == request.IdCardNumber);
        if (subscriber == null)
        {
            throw new Exception($"Subscriber not found with IdCardNumber: {request.IdCardNumber}");
        }

        subscription.Subscriber = subscriber;

        await _context.SaveChangesAsync();
        return subscription;
    }
    private async Task<PricingPlan> GetPricingPlanByDayType(DayType dayType)
    {
        return await _context.PricingPlans
            .Where(plan => plan.DayType == dayType)
            .FirstOrDefaultAsync();
    }
    private int CountWeekdays(DateTime startDate, DateTime endDate)
    {
        int weekdays = 0;
        DateTime currentDate = startDate;
        while (currentDate <= endDate)
        {
            if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdays++;
            }

            currentDate = currentDate.AddDays(1);
        }

        return weekdays;
    }
    private decimal CalculateSubscriptionPrice(PricingPlan pricingPlan, DateTime startDate, DateTime endDate,
        decimal discountValue = 0)
    {
        if (pricingPlan == null)
        {
            return -1;
        }

        int numberOfDays = CountWeekdays(startDate, endDate);
        decimal dailyPrice = pricingPlan.DailyPricing;
        decimal subscriptionPrice = dailyPrice * numberOfDays - discountValue;
        return subscriptionPrice;
    }
    private DayType GetDayType(DateTime date)
    {
        // Assuming Monday to Friday are considered weekdays, and Saturday and Sunday are weekends
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            return DayType.Weekend;
        }
        return DayType.Weekday;
    }
}