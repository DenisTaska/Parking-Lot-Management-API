using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;
using DayType = Parking_Lot_Management.API.Entities.DayType;

namespace Parking_Lot_Management.API.Services;

public class LogService : ILogService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IParkingSpotService _parkingSpotService;

    public LogService(DataContext context, IMapper mapper, IParkingSpotService parkingSpotService)
    {
        _context = context;
        _mapper = mapper;
        _parkingSpotService = parkingSpotService;
    }

    public async Task<List<LogDto>> GetAllLogs()
    {
        var logs = await _context.Logs
            .Select(l => new LogDto
            {
                Id = l.Id,
                Code = l.Code,
                Price = l.Price,
                SubscriptionId = l.Subscription.Id,
                CheckInTime = l.CheckInTime,
                CheckOutTime = l.CheckInTime,
                SubscriberName = l.Subscription.Subscriber.FirstName,
                SubscriptionIsDeleted = l.Subscription.IsDeleted,
                CheckedOut = l.CheckedOut
            })
            .ToListAsync();

        return logs;
    }

    public async Task<List<LogDto>> GetAllLogs(string? code, string? subscriberName)
    {
        if (!string.IsNullOrEmpty(code))
        {
            code = code.Trim();
            return await _context.Logs
                .Select(l => new LogDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Price = l.Price,
                    SubscriptionId = l.Subscription.Id,
                    CheckInTime = l.CheckInTime,
                    CheckOutTime = l.CheckInTime,
                    SubscriptionIsDeleted = l.Subscription.IsDeleted,
                    CheckedOut = l.CheckedOut
                })
                .Where(l => l.Code == code)
                .OrderBy(l => l.Code).ToListAsync();
        }

        if (!string.IsNullOrEmpty(subscriberName))
        {
            subscriberName = subscriberName.Trim();
            return await _context.Logs
                .Select(l => new LogDto
                {
                    Id = l.Id,
                    Code = l.Code,
                    Price = l.Price,
                    SubscriptionId = l.Subscription.Id,
                    CheckInTime = l.CheckInTime,
                    CheckOutTime = l.CheckInTime,
                    SubscriberName = l.Subscription.Subscriber.FirstName,
                    SubscriptionIsDeleted = l.Subscription.IsDeleted,
                    CheckedOut = l.CheckedOut
                })
                .Where(l => l.SubscriberName == subscriberName)
                .OrderBy(l => l.SubscriberName).ToListAsync();
        }

        return await GetAllLogs();
    }

    public async Task<List<Log>> GetAllLogsForDay(DateTime date)
    {
        var logs = await _context.Logs
            .Where(l => l.CheckInTime.Date == date.Date) // Filter by the date
            .ToListAsync();
        return logs;
    }

    public async Task<List<LogForResponseDto>> CreateLog(LogForCreationDto logForCreationDto)
    {
        var log = _mapper.Map<Log>(logForCreationDto);

        log.Price = CalculatePrice(logForCreationDto);

        var subscription =
            await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == logForCreationDto.SubscriptionId);

        log.Subscription = subscription;

        if (log.Subscription != null && log.Subscription.IsDeleted == false)
        {
            log.Price = 0;
        }

        //Make SubscriptionId Unique
        if (await _context.Logs.AnyAsync(l =>
                l.SubscriptionId == logForCreationDto.SubscriptionId && l.SubscriptionId != null &&
                l.CheckedOut == false))
        {
            throw new Exception(
                $"The Subscriber with the id: {logForCreationDto.SubscriptionId} has already logged and has not checked out");
        }

        //Make Code Unique and check for CheckOut to be false
        if (await _context.Logs.AnyAsync(l => l.Code == logForCreationDto.Code && l.CheckedOut == false))
        {
            throw new Exception(
                $"The Subscriber with the Code: {logForCreationDto.Code} has already logged and has not checked out");
        }

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();

        var responseLog = new LogForResponseDto()
        {
            Id = log.Id,
            Code = log.Code,
            CheckInTime = log.CheckInTime,
            CheckOutTime = log.CheckOutTime,
            SubscriptionId = log.SubscriptionId,
            SubscriptionIsDeleted =
                subscription?.IsDeleted ?? null, // If subscription is null, assume IsDeleted as null
            Price = log.Price,
            CheckedOut = log.CheckedOut
        };
        return new List<LogForResponseDto> { responseLog };
    }

    private decimal CalculatePrice(LogForCreationDto logForCreationDto)
    {
        var pricingPlan = GetPricingPlanByDay(logForCreationDto.CheckInTime).Result;

        TimeSpan totalDuration = logForCreationDto.CheckOutTime - logForCreationDto.CheckInTime;
        double totalHours = totalDuration.TotalHours;

        if (totalDuration.TotalMinutes <= 15)
        {
            return 0; // No charge for the grace period
        }

        if (totalHours < 1)
        {
            return pricingPlan.HourlyPricing; // Charge for one whole hour
        }

        decimal totalHoursDecimal = (decimal)totalHours;

        if (totalHours <= pricingPlan.MinimumHours)
        {
            return totalHoursDecimal * pricingPlan.HourlyPricing;
        }
        else
        {
            int fullDays = (int)Math.Floor(totalHours / 24);
            double remainingHours = totalHours % 24;

            decimal remainingHoursDecimal = (decimal)remainingHours;

            if (remainingHours <= pricingPlan.MinimumHours)
            {
                return (fullDays * pricingPlan.DailyPricing) + (remainingHoursDecimal * pricingPlan.HourlyPricing);
            }
            else
            {
                return (fullDays + 1) * pricingPlan.DailyPricing;
            }
        }
    }

    private async Task<PricingPlanDto> GetPricingPlanByDay(DateTime checkInTime)
    {
        var dayType = GetDayType(checkInTime);

        var pricingPlan = await _context.PricingPlans
            .FirstOrDefaultAsync(plan => plan.DayType == dayType);

        if (pricingPlan == null)
        {
            throw new Exception($"Pricing plan not found for DayType: {dayType}");
        }

        return new PricingPlanDto
        {
            HourlyPricing = pricingPlan.HourlyPricing,
            DailyPricing = pricingPlan.DailyPricing,
            MinimumHours = pricingPlan.MinimumHours
        };
    }

    private DayType GetDayType(DateTime checkInTime)
    {
        return checkInTime.DayOfWeek switch
        {
            DayOfWeek.Saturday or DayOfWeek.Sunday => DayType.Weekend,
            _ => DayType.Weekday,
        };
    }

    public async Task<List<Log>?> CheckOutLog(int id)
    {
        var log = await _context.Logs.FindAsync(id);
        if (log is null)
            return null;

        log.CheckedOut = true;
        await _context.SaveChangesAsync();

        return await _context.Logs.ToListAsync();
    }
}