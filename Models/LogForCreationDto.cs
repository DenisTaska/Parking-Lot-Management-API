using System.ComponentModel.DataAnnotations;

namespace Parking_Lot_Management.API.Models;

public class LogForCreationDto
{
    [Required(ErrorMessage = "Code is required.")]
    public string Code { get; set; }
    public int? SubscriptionId { get; set; }
    
    [Required(ErrorMessage = "CheckInTime is required.")]
    public DateTime CheckInTime { get; set; }
    
    [Required(ErrorMessage = "CheckOutTime is required.")]
    public DateTime CheckOutTime { get; set; }
}