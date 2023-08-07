using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Parking_Lot_Management.API.Models;

public class SubscriptionForCreationDto
{
    [Required(ErrorMessage = "Code is required.")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "IdCardNumber is required.")]
    public string IdCardNumber { get; set; }

    [Required(ErrorMessage = "DiscountedValue is required.")]
    public int DiscountedValue { get; set; }
    
    [Required(ErrorMessage = "StartDate is required.")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "EndDate is required.")]
    public DateTime EndDate { get; set; }
}