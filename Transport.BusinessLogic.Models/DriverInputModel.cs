using System.ComponentModel.DataAnnotations;

namespace Transport.BusinessLogic.Models;

public class DriverInputModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public VehicleInputModel Vehicle { get; set; }
}