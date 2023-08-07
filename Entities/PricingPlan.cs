using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Entities;

public class PricingPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public decimal HourlyPricing { get; set; }
    
    [Required]
    public decimal DailyPricing { get; set; } 
    
    [Required]
    public int MinimumHours { get; set; }

    [Required]
    public DayType DayType { get; set; }
}

public enum DayType
{
    Weekday,
    Weekend
}