using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transport.BusinessLogic.Models;
using Transport.BusinessLogic.Services.Contracts;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Implementations;

internal class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<Tuple<UserViewModel?, IdentityResult>> CreateUserAsync(UserInputModel userInput)
    {
        var user = mapper.Map<User>(userInput);
        var identityResult = await userManager.CreateAsync(user, userInput.Password);

        if (!identityResult.Succeeded)
        {
            return new(null, identityResult);
        }

        return new(mapper.Map<UserViewModel>(user), identityResult);
    }

    public async Task<UserViewModel?> GetUserByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        return mapper.Map<UserViewModel>(user);
    }

    public async Task<UserViewModel?> GetUserByIdAsync(Guid id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return null;

        return mapper.Map<UserViewModel>(user);
    }

    public async Task<Tuple<UserViewModel?, IdentityResult?>> UpdateUserAsync(Guid id, UserUpdateModel userUpdate)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return new(null, null);

        user.PhoneNumber = userUpdate.PhoneNumber;
        user.Email = userUpdate.Email;
        user.FirstName = userUpdate.FirstName;
        user.LastName = userUpdate.LastName;
        user.UserName = userUpdate.UserName;

        var identityResult = await userManager.UpdateAsync(user);

        if (identityResult.Succeeded)
            return new(mapper.Map<UserViewModel>(user), identityResult);
        else return new(null, identityResult);
    }

    public async Task<IdentityResult?> UpdateUserPassword(Guid id, string currentPassword, string newPassword)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return null;

        return await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<bool> UpdateUserLocationAsync(Guid id, double latitude, double longitude)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return false;

        user.LastPingedLatitude = latitude;
        user.LastPingedLongitude = longitude;

        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.Succeeded;
    }

    public async Task<Tuple<double, double>?> GetUserLocationAsync(Guid id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(e => e.Id == id);

        if (user == null)
            return null;

        return new(user.LastPingedLatitude, user.LastPingedLongitude);
    }
}