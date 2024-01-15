using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class CancelRideModel
{
    [Required]
    public Guid RideId { get; set; }
}