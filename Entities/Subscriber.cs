using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace Parking_Lot_Management.API.Entities;

public class Subscriber
{
    [Key]
    [MaxLength(10)]
    public string IdCardNumber { get; set; }
    
    [Required]
    public string FirstName { get; set; }   
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; } 
    
    [Required]
    public int PlateNumber { get; set; }
    
    [Required]
    public int PhoneNumber { get; set; }
    
    [Required]
    public DateTime Birthday { get; set; }
    public bool IsDeleted { get; set; }
    
    public Subscriber()
    {
        IsDeleted = false;
    }
}