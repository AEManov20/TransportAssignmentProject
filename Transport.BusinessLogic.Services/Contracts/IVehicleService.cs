using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IVehicleService
{
    Task<Vehicle?> GetVehicleById(Guid id);

    Task<Tuple<Vehicle?, string?>> CreateVehicle(VehicleInputModel vehicle);

    Task<Tuple<Vehicle?, string?>> UpdateVehicle(VehicleInputModel vehicle);

    Task<Tuple<bool, string?>> DeleteVehicle(Guid id);
}