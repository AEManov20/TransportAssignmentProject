using Transport.Data.Models;

namespace Transport.BusinessLogic.Repositories.Contracts;

interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(Guid id);
}