using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface IUserUseCase
{
    Task<Guid> Authenticate(string username, string password);
    Task<bool?> Authenticate(Guid guid);
    Task<User?> AddUser(Guid guid, User user);
    Task<User?> UpdateUser(Guid guid, User user);
    Task<bool?> DeleteUser(Guid guid);
    Task<bool?> IsValidUser(User user);
}
