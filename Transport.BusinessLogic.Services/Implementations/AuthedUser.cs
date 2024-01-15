using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Transport.BusinessLogic.Services.Contracts;

namespace Transport.BusinessLogic.Services.Implementations;

public class AuthedUser : IAuthedUser
{
    public AuthedUser(IHttpContextAccessor httpContextAccessor)
    {
        this.UserId = Guid.Parse(httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        this.Email = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
    }

    public Guid UserId { get; }
    
    public string Email { get; }
}