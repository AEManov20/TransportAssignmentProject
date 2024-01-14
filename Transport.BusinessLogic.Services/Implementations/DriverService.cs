using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

internal class DriverService : IDriverService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public DriverService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<DriverViewModel?> CreateDriverAsync(Guid userId, VehicleInputModel vehicle)
    {
        Driver driver = new()
        {
            Id = userId,
            Vehicle = mapper.Map<Vehicle>(vehicle)
        };

        var createdDriver = context.Drivers.Add(driver);
        
        await context.SaveChangesAsync();

        return mapper.Map<DriverViewModel>(createdDriver.Entity);
    }

    public async Task<DriverViewModel?> GetDriverById(Guid id)
    {
        var driver = await context.Drivers
            .Include(x => x.Vehicle)
            .FirstOrDefaultAsync(x => x.Id == id);

        return driver == null ? null : mapper.Map<DriverViewModel>(driver);
    }

    public async Task<DriverViewModel?> GetDriverByVehicleIdAsync(Guid vehicleId)
    {
        var driver = await context.Drivers
            .Include(x => x.Vehicle)
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId);
        
        return driver == null ? null : mapper.Map<DriverViewModel>(driver);
    }

    public async Task<DriverViewModel?> UpdateDriverVehicleAsync(Guid id, VehicleInputModel vehicleInput)
    {
        var driver = await context.Drivers
            .Include(driver => driver.Vehicle)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (driver == null)
        {
            return null;
        }

        var oldVehicle = driver.Vehicle;

        driver.Vehicle = mapper.Map<Vehicle>(vehicleInput);
        context.Vehicles.Remove(oldVehicle);

        await context.SaveChangesAsync();

        return mapper.Map<DriverViewModel>(driver);
    }

    public async Task<DriverAvailability?> UpdateDriverAvailability(Guid id, DriverAvailability availability)
    {
        var driver = await context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

        if (driver == null)
        {
            return null;
        }

        driver.AvailabilityStatus = availability;

        await context.SaveChangesAsync();

        return driver.AvailabilityStatus;
    }

    public async Task<bool> DeleteDriverById(Guid id)
    {
        var driver = await context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

        if (driver == null)
        {
            return false;
        }

        context.Drivers.Remove(driver);

        await context.SaveChangesAsync();

        return true;
    }
}
