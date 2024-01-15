using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IUserService
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A tuple with the newly created user's data and an identity result</returns>
    Task<Tuple<UserViewModel?, IdentityResult>> CreateUserAsync(UserInputModel user);
    
    /// <summary>
    /// Gets a user via a supplied user ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>The user's data</returns>
    Task<UserViewModel?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Gets a user via a supplied user email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns>The user's data</returns>
    Task<UserViewModel?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Updates the user via supplied update data
    /// </summary>
    /// <param name="id">The user's ID</param>
    /// <param name="user">Update data</param>
    /// <returns>A tuple with the updated user's data and an identity result</returns>
    Task<Tuple<UserViewModel?, IdentityResult?>> UpdateUserAsync(Guid id, UserUpdateModel user);

    /// <summary>
    /// Updates the user's password
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="currentPassword">User's current password</param>
    /// <param name="newPassword">New user password</param>
    /// <returns>A boolean which tells whether the user's password has been updated</returns>
    Task<IdentityResult?> UpdateUserPassword(Guid id, string currentPassword, string newPassword);

    /// <summary>
    /// Updates the user ping location
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="location">Geographical location</param>
    /// <returns>A boolean which tells whether the operation was successful or not</returns>
    Task<bool> UpdateUserLocationAsync(Guid id, LocationModel location);

    /// <summary>
    /// Gets the last pinged user location
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>A tuple with the latitude and longitude</returns>
    Task<LocationModel?> GetUserLocationAsync(Guid id);
}
