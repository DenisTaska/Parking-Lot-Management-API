using System.ComponentModel.DataAnnotations;

namespace Parking_Lot_Management.API.Models;

public class SubscriberForCreationDto
{
    [Required(ErrorMessage = "IdCardNumber is required.")]
    public string IdCardNumber{ get; set; }
    
    [Required(ErrorMessage = "FirstName is required.")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "LastName is required.")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "PhoneNumber is required.")]
    public int PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Birthday is required.")]
    public DateTime Birthday { get; set; }
    
    [Required(ErrorMessage = "PlateNumber is required.")]
    public int PlateNumber { get; set; }
}

