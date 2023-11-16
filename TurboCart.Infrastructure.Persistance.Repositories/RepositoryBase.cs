using Microsoft.EntityFrameworkCore;
using TurboCart.Infrastructure.Persistence.Interfaces;

namespace TurboCart.Infrastructure.Persistance.Repositories;

internal class RepositoryBase<T>(DbContext _dbContext)
    : IRepository<T>
    where T : class
{
    private DbSet<T> _set = _dbContext.Set<T>();

    public IEnumerable<T> GetAll()
        => _set.ToArray();

    public T? GetById(object entityId)
        => _set.Find(entityId);

    public void Save(T entity)
        => _set.Add(entity);
}
