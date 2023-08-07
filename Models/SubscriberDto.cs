using System.Runtime.InteropServices.JavaScript;

namespace Parking_Lot_Management.API.Models;

public class SubscriberDto
{
    public string IdCardNumber{ get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public DateTime Birthday { get; set; }
    public int PlateNumber { get; set; }
    public bool IsDeleted { get; set; }
}