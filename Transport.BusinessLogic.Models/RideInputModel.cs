using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class RideInputModel
{
    [Required]
    public Guid DriverId { get; set; }

    [Required]
    ICollection<RideStopInputModel> RideStops { get; set; }
}