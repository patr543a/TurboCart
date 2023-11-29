using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IUserRepository
    : IRepository<User, string>
{
    void Authenticate(string username, string password);
    void DeleteUser(string username, string password);
    void UpdateUser(string username, string password, User user);
}
