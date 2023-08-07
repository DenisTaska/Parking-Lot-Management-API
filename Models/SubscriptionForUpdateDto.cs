using System.ComponentModel.DataAnnotations;

namespace Parking_Lot_Management.API.Models;

public class SubscriptionForUpdateDto
{
    [Required(ErrorMessage = "IdCardNumber is required.")]
    public string IdCardNumber { get; set; }

    [Required(ErrorMessage = "DiscountedValue is required.")]
    public int DiscountedValue { get; set; }
    
    [Required(ErrorMessage = "StartDate is required.")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "EndDate is required.")]
    public DateTime EndDate { get; set; }
}