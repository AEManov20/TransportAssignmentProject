using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class RideStopInputModel
{
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string AddressText { get; set; } = string.Empty;
}