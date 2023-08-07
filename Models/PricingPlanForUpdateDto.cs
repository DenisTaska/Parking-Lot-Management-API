using System.ComponentModel.DataAnnotations;

namespace Parking_Lot_Management.API.Models;

public class PricingPlanForUpdateDto
{
    [Required(ErrorMessage = "HourlyPricing is required.")]
    public int HourlyPricing { get; set; }
    
    [Required(ErrorMessage = "DailyPricing is required.")]
    public int DailyPricing { get; set; }
    
    [Required(ErrorMessage = "MinimumHours is required.")]
    public int MinimumHours { get; set; }
}