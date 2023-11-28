using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IUserRepository
    : IRepository<User, int>
{
}
