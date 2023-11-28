using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class UserRepository(DbContext _dbContext)
    : RepositoryBase<Customer, int>(_dbContext), IUserRepository
{
    public void Add(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User>? GetAll(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string? includes = null)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    protected override int GetEntityId(Customer entity)
    {
        throw new NotImplementedException();
    }

    User? IRepository<User, int>.GetById(int entityId, string? includes)
    {
        throw new NotImplementedException();
    }
}
