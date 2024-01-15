using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[TestFixture]
class DriverServiceTests
{
    private ApplicationDbContext context;
    private IMapper mapper;

    private IServiceCollection services;

    private User userOne, userTwo;

    [SetUp]
    public async Task Setup()
    {
        services = new ServiceCollection();

        services.AddAutoMapper(typeof(Mappings));

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: "TransportDatabase");
            opt.EnableSensitiveDataLogging();
            opt.EnableThreadSafetyChecks();
            opt.EnableDetailedErrors();
        });

        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddLogging();

        var provider = services.BuildServiceProvider();

        context = provider.GetRequiredService<ApplicationDbContext>();
        mapper = provider.GetRequiredService<IMapper>();
        UserManager<User> userManager = provider.GetRequiredService<UserManager<User>>();

        userOne = mapper.Map<User>(new UserInputModel()
        {
            Email = "testing1@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "+359888888888",
            UserName = "john_doe"
        });

        userTwo = mapper.Map<User>(new UserInputModel()
        {
            Email = "testing2@example.com",
            FirstName = "Jane",
            LastName = "Doe",
            PhoneNumber = "+359888888889",
            UserName = "jane_doe"
        });

        // Seed example data
        await userManager.CreateAsync(userOne, "12341234Aa_");

        await userManager.CreateAsync(userTwo, "12341234Bb_");
    }

    [Test]
    public async Task CreateDriver_Valid_ReturnsDriverViewModel()
    {
        // Arrange
        IDriverService driverService = new DriverService(context, mapper);
        var vehicle = A.Fake<VehicleInputModel>();

        // Act
        var result = await driverService.CreateDriverAsync(userOne.Id, vehicle);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(userOne.Id));
        Assert.That(result.Vehicle.Model, Is.EqualTo(vehicle.Model));
        Assert.That(result.Vehicle.RegistrationNumber, Is.EqualTo(vehicle.RegistrationNumber));
        Assert.That(result.Vehicle.Brand, Is.EqualTo(vehicle.Brand));
        Assert.That(result.Vehicle.Color, Is.EqualTo(vehicle.Color));
        Assert.That(result.Vehicle.RegisteredInCountry, Is.EqualTo(vehicle.RegisteredInCountry));
        Assert.That(result.Vehicle.Seats, Is.EqualTo(vehicle.Seats));
    }

    [Test]
    public async Task GetDriverById_Invalid_ReturnsNull()
    {
        // Arrange
        IDriverService driverService = new DriverService(context, mapper);

        // Act
        var result = await driverService.GetDriverById(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.Null);
    }
}