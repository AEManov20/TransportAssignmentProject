namespace Transport.Data.Models;

public partial class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string RegistrationNumber { get; set; } = null!;

    public string RegisteredInCountry { get; set; } = null!;

    public short Seats { get; set; }

    public string Color { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
