using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;


namespace Transport.WebHost.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly IAuthService authService;
    private readonly ILogger<AuthController> logger;
    private readonly IMapper mapper;

    public AuthController(
        IUserService userService,
        ITokenService tokenService,
        IAuthService authService,
        ILogger<AuthController> logger,
        IMapper mapper)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.authService = authService;
        this.logger = logger;
        this.mapper = mapper;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserInputModel userInput)
    {
        if (await authService.CheckUserExistsAsync(userInput.Email))
        {
            return Conflict("user-already-exists");
        }

        var (_, identityResult) = await userService.CreateUserAsync(userInput);

        if (!identityResult.Succeeded)
            return BadRequest(identityResult.Errors.FirstOrDefault()?.Description);
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] UserLoginModel userInput)
    {
        if (!await authService.CheckUserExistsAsync(userInput.Email))
        {
            return NotFound("user-not-found");
        }

        if (!await authService.CheckPassword(userInput.Email, userInput.Password))
        {
            return BadRequest("incorrect-password");
        }

        var user = await userService.GetUserByEmailAsync(userInput.Email);

        var token = await tokenService.CreateTokenForUserAsync(user!.Id);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
