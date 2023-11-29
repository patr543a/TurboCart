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
            throw new AuthenticationException("Authentication failed");
    }

    public void DeleteUser(string username, string password)
    {
        try
        {
            Authenticate(username, password);

            Delete(username);
        }
        catch (AuthenticationException ex)
        {
            throw new AuthenticationException("Authentication failed", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not delete", ex);
        }
    }

    public void UpdateUser(string username, string password, User user)
    {
        try
        {
            Authenticate(username, password);

            Update(user);
        }
        catch (AuthenticationException ex)
        {
            throw new AuthenticationException("Authentication failed", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not update", ex);
        }
    }

    protected override string GetEntityId(User entity)
        => entity.Username ?? "";
}
