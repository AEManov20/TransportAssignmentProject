using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class VehicleInputModel
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Model { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string RegisteredInCountry { get; set; } = string.Empty;

    [Required]
    [Range(1, 8)]
    public short Seats { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Color { get; set; } = string.Empty;
}