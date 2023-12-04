using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class UserRepository(DbContext _dbContext)
    : RepositoryBase<User, string>(_dbContext), IUserRepository
{
    public Guid Authenticate(string username, string password)
    {
        var user = GetById(username);

        if (user is null || user.Password != password)
            return Guid.Empty;

        return user.SessionToken = Guid.NewGuid();
    }

    public bool Authenticate(Guid guid)
    {
        var result = _set.FirstOrDefault(u => u.SessionToken == guid);

        if (result is null)
            return false;

        return true;
    }

    public void DeleteUser(Guid guid)
    {
        try
        {
            var auth = Authenticate(guid);

            if (!auth)
                throw new AuthenticationException("Invalid Guid");

            var user = _set.FirstOrDefault(u => u.SessionToken == guid) 
                ?? throw new Exception();

            Delete(user);
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

    public void UpdateUser(Guid guid, User user)
    {
        try
        {
            var auth = Authenticate(guid);

            if (!auth)
                throw new AuthenticationException("Invalid Guid");

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
