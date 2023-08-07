using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_Lot_Management.API.Entities;

public class Log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Code { get; set; }

    [Required]
    public DateTime CheckInTime { get; set; }
    
    [Required]
    public DateTime CheckOutTime { get; set; }

    [ForeignKey("SubscriptionId")]
    public Subscription? Subscription { get; set; }

    public int? SubscriptionId { get; set; }

    public decimal Price { get; set; }
    
    public bool CheckedOut { get; set; }

    public Log()
    {
        Price = 0;
        CheckedOut = false;
    }
}