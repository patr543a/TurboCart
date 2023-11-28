using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface IUserUseCase
{
    Task<bool?> Authenticate(string username, string password);
    Task<User?> AddUser(string username, string password, User user);
    Task<User?> UpdateUser(string username, string password, User user);
    Task<bool?> DeleteUser(string username, string password);
    Task<bool?> IsValidUser(User user);
}
