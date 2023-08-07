namespace Parking_Lot_Management.API.Models;

public class LogForResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int? SubscriptionId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckOutTime { get; set; }
    public decimal Price { get; set; }
    public bool? SubscriptionIsDeleted { get; set; }
    
    public bool CheckedOut { get; set; }
}