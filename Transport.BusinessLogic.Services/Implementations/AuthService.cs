using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<bool> CheckUserExistsAsync(string email)
    {
        return await userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> CheckPassword(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return false;

        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> CanUserSignIn(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        return user != null && await signInManager.CanSignInAsync(user);
    }
}