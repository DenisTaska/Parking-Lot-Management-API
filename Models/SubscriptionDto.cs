using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Parking_Lot_Management.API.Models;

public class SubscriptionDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string IdCardNumber { get; set; }
    public decimal Price { get; set; }
    public int DiscountedValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; }
}