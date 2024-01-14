using System.ComponentModel.DataAnnotations.Schema;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Models;

public class DriverViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DriverAvailability AvailabilityStatus { get; set; }

    public VehicleViewModel Vehicle { get; set; } = null!;
}