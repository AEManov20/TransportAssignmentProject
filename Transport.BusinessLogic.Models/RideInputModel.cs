using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class RideInputModel
{
    [Required]
    [MinLength(2)]
    public ICollection<RideStopInputModel> RideStops { get; set; } = new List<RideStopInputModel>();
}