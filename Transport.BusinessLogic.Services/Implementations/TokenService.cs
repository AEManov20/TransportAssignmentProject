using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    public async Task<JwtSecurityToken?> CreateTokenForUserAsync(Guid userId)
    {
        var user = await this.userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return null;
        }

        var userRoles = await this.userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        authClaims
            .AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        return CreateToken(authClaims);
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtAccessTokenSecret"]!));
        _ = int.TryParse(this.configuration["JwtAccessTokenValidityInHours"], out var tokenValidity);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(tokenValidity),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}