using System.ComponentModel.DataAnnotations;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Models;

public class RideStopViewModel
{
    public string AddressText { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public short OrderingNumber { get; set; }
}