using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.BusinessLogic.Services.Contracts;

public interface IUserService
{
    Task<Tuple<User?, string?>> CreateUserAsync(UserInputModel user);
    
    Task<User?> GetUserByIdAsync(Guid id);

    Task<User?> GetUserByEmailAsync(string email);

    Task<Tuple<User?, string?>> UpdateUserAsync(UserInputModel user);

    Task<Tuple<bool, string?>> DeleteUserAsync(Guid id);

    Task<Tuple<bool, string?>> UpdateUserPassword(Guid id, string oldPassword, string newPassword);
}
