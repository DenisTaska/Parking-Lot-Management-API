namespace Parking_Lot_Management.API.Models;

public enum DayType
{
    Weekday,
    Weekend
}

public class PricingPlanDto
{
    public decimal HourlyPricing { get; set; }
    public decimal DailyPricing { get; set; }
    public int MinimumHours { get; set; }
}
