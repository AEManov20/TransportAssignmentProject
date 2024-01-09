using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.Data.Models;

public class User
{
    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;

    [Required] public string Username { get; set; } = string.Empty;

    [Required] public string PasswordHash { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] [Phone] public string PhoneNumber { get; set; } = string.Empty;
}

public class Driver
{
    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    // TODO: Create special type for this field
    [Required] public string Availability { get; set; } = string.Empty;

    [ForeignKey(nameof(Id))] public User User { get; set; }
}

public class Vehicle
{
    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string Model { get; set; } = string.Empty;

    [Required] public string Brand { get; set; } = string.Empty;

    [Required] public string RegistrationNumber { get; set; } = string.Empty;

    [Required] public uint Seats { get; set; } = 0;

    [Required] public string Color { get; set; } = string.Empty;

    [Required] public string Type { get; set; } = string.Empty;

    // TODO: Make unique
    [Required] public string OwnerId { get; set; } = string.Empty;

    [ForeignKey(nameof(OwnerId))] public Driver Owner { get; set; }
}

public class Route
{
    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string StartPoint { get; set; } = string.Empty;

    [Required] public string EndPoint { get; set; } = string.Empty;

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    [Required] public string DriverId { get; set; } = string.Empty;

    [ForeignKey(nameof(DriverId))] public Driver Driver { get; set; }
}

public class Rider
{
    [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(Id))] public User User { get; set; }
}
