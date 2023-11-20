using Microsoft.EntityFrameworkCore;
using TurboCart.Infrastructure.Persistence.Interfaces;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public abstract class RepositoryBase<T>(DbContext _dbContext)
    : IRepository<T>
    where T : class
{
    protected DbSet<T> _set = _dbContext.Set<T>();

    public void Add(T entity)
        => _set.Add(entity);

    public IEnumerable<T> GetAll()
        => _set.ToArray();

    public T? GetById(object entityId)
        => _set.Find(entityId);

}
