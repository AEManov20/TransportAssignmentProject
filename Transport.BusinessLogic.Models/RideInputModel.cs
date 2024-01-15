using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class RideInputModel
{
    [Required]
    public ICollection<RideStopInputModel> RideStops { get; set; } = new List<RideStopInputModel>();
}