using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

[TestFixture]
public class AuthServiceTests
{
    private IAuthService authService;
    private ApplicationDbContext context;
    private UserManager<User> userManager;
    private SignInManager<User> signInManager;
    private IMapper mapper;

    private IServiceCollection services;

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
        userManager = provider.GetRequiredService<UserManager<User>>();
        signInManager = provider.GetRequiredService<SignInManager<User>>();

        authService = new AuthService(userManager, signInManager);

        // Seed example data
        await userManager.CreateAsync(
                    mapper.Map<User>(new UserInputModel()
                    {
                        Email = "testing1@example.com",
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "+359888888888",
                        UserName = "john_doe"
                    }), "12341234");

        await userManager.CreateAsync(
                    mapper.Map<User>(new UserInputModel()
                    {
                        Email = "testing2@example.com",
                        FirstName = "Jane",
                        LastName = "Doe",
                        PhoneNumber = "+359888888889",
                        UserName = "jane_doe"
                    }), "12341234");

    }

    [Test]
    public async Task Test_CheckUserExistsAsync_Valid()
    {
        // Arrange
        string email = "testing1@example.com";

        // Act
        var result = await authService.CheckUserExistsAsync(email);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Test_CheckUserExistsAsync_Invalid()
    {
        // Arrange
        string email = "testing@example.com";

        // Act
        var result = await authService.CheckUserExistsAsync(email);

        // Assert
        Assert.That(result, Is.False);
    }
}
