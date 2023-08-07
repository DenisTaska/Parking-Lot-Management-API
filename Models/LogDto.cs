
using Parking_Lot_Management.API.Entities;

namespace Parking_Lot_Management.API.Models;

public class LogDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int? SubscriptionId { get; set; }
    public string? SubscriberName { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public decimal Price { get; set; }
    public bool? SubscriptionIsDeleted { get; set; }
    public bool CheckedOut { get; set; }
    
    public LogDto()
    {
        Price = 0;
        CheckedOut = false;
    }
}