using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Parking_Lot_Management.API.Entities;

public class Subscription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Code { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [ForeignKey("IdCardNumber")]
    public Subscriber Subscriber { get; set; }

    [Required]
    public int DiscountedValue { get; set; }

    [Required]
    public decimal Price { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public Subscription()
    {
        IsDeleted = false;
    }
    
    public Subscription(decimal price)
    {
        Price = price;
        IsDeleted = false;
    }
}