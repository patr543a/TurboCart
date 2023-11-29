using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class UserRepository(DbContext _dbContext)
    : RepositoryBase<User, string>(_dbContext), IUserRepository
{
    public void Authenticate(string username, string password)
    {
        var user = GetById(username);

        if (user is null || user.Password != password)
            throw new RepositoryException("Authentication failed");
    }

    public void DeleteUser(string username, string password)
    {
        try
        {
            Authenticate(username, password);

            Delete(username);
        }
        catch (Exception ex)
        {
            throw new Exception("Authentication failed", ex);
        }
    }

    public void UpdateUser(string username, string password, User user)
    {
        throw new NotImplementedException();
    }

    protected override string GetEntityId(User entity)
        => entity.Username ?? "";
}
