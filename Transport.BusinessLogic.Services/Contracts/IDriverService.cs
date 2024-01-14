using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IDriverService
{
    /// <summary>
    /// Creates a driver
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="vehicleInput">Vehicle input data</param>
    /// <returns>Newly created driver's data</returns>
    Task<DriverViewModel?> CreateDriverAsync(Guid userId, VehicleInputModel vehicleInput);

    /// <summary>
    /// Gets driver by supplied driver ID
    /// </summary>
    /// <param name="id">Driver ID</param>
    /// <returns>The driver's data to whom the ID is associated</returns>
    Task<DriverViewModel?> GetDriverById(Guid id);

    /// <summary>
    /// Gets driver by supplied vehicle ID
    /// </summary>
    /// <param name="vehicleId">Vehicle ID</param>
    /// <returns>The driver's data to whom the vehicle ID is associated</returns>
    Task<DriverViewModel?> GetDriverByVehicleIdAsync(Guid vehicleId);

    /// <summary>
    /// Updates driver vehicle via supplied driver ID
    /// </summary>
    /// <param name="id">Driver ID</param>
    /// <param name="vehicleInput">Vehicle input data</param>
    /// <returns>The updated driver's data</returns>
    Task<DriverViewModel?> UpdateDriverVehicleAsync(Guid id, VehicleInputModel vehicleInput);

    /// <summary>
    /// Updates the availability of said driver
    /// </summary>
    /// <param name="id">Driver ID</param>
    /// <param name="availability">Availability status</param>
    /// <returns>The updated driver availability status</returns>
    Task<DriverAvailability?> UpdateDriverAvailability(Guid id, DriverAvailability availability);

    /// <summary>
    /// Deletes driver by ID
    /// </summary>
    /// <param name="id">Driver ID</param>
    /// <returns>A boolean that tells whether the driver has been deleted or not</returns>
    Task<bool> DeleteDriverById(Guid id);
}
