using System.Linq.Expressions;

namespace TurboCart.Infrastructure.Persistence.Interfaces;

public interface IRepository<TEntity, TKey>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TKey entityId);
    void Delete(TEntity entity);
    TEntity? GetById(TKey entityId, string? includes = null);
    IEnumerable<TEntity>? GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includes = null);
}
