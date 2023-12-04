using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IUserRepository
    : IRepository<User, string>
{
    Guid Authenticate(string username, string password);
    bool Authenticate(Guid guid);
    void DeleteUser(Guid guid);
    void UpdateUser(Guid guid, User user);
}
