using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public enum DriverAvailability : short
{
    Available,
    Driving,
    Offline
}

public interface IDriverService
{
    Task<Tuple<Driver?, string?>> CreateDriverAsync(DriverInputModel driver);

    Task<Driver?> GetDriverById(Guid id);

    Task<Driver?> GetDriverByVehicleIdAsync(Guid vehicleId);

    Task<Tuple<Driver?, string?>> UpdateDriverVehicleAsync(Guid vehicleId);

    Task<Tuple<bool, string?>> DeleteDriverById(Guid id);
}
