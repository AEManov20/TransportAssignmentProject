namespace Transport.BusinessLogic.Models;

public class VehicleViewModel
{
    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string RegistrationNumber { get; set; } = string.Empty;

    public string RegisteredInCountry { get; set; } = string.Empty;

    public short Seats { get; set; }

    public string Color { get; set; } = string.Empty;
}