using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TurboCart.Infrastructure.Persistence.Interfaces;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public abstract class RepositoryBase<TEntity, TKey>(DbContext _dbContext)
    : IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : notnull
{
    protected DbSet<TEntity> _set = _dbContext.Set<TEntity>();

    public void Add(TEntity entity)
        => _set.Add(entity);

    public void Delete(TKey entityId)
    {
        var entity = _set.Find(entityId) 
            ?? throw new ArgumentException("Entity was not found", nameof(entityId));

        Delete(entity);
    }

    public void Delete(TEntity entity)
    {
        if (_set.Entry(entity).State == EntityState.Detached)
            _set.Attach(entity);

        _set.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        if (_set.Entry(entity).State == EntityState.Detached)
            _set.Attach(entity);

        _set.Entry(entity).State = EntityState.Modified;
    }

    public IEnumerable<TEntity>? GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includes = null)
    {
        var query = _set as IQueryable<TEntity>;

        if (includes is not null)
            foreach (var property in includes.Split(';', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(property);

        if (filter is not null)
            query = query.Where(filter);

        return orderBy is null ? query : orderBy(query);
    }

    public TEntity? GetById(TKey entityId, string? includes = null)
    {
        if (includes is null)
            return _set.Find(entityId);

        var query = _set as IQueryable<TEntity>;

        foreach (var property in includes.Split(';', StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(property);

        return query.ToArray()
            .FirstOrDefault(e => 
                GetEntityId(e)
                    .Equals(entityId));
    }

    protected abstract TKey GetEntityId(TEntity entity);
}
