using Microsoft.Extensions.DependencyInjection;
using Transport.BusinessLogic.Services.Contracts;
using Transport.BusinessLogic.Services.Implementations;

namespace Transport.BusinessLogic.Services;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection servicesCollection)
    {
        servicesCollection
            .AddScoped<IDriverService, DriverService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IRideService, RideService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IAuthedUser, AuthedUser>();
    }
}