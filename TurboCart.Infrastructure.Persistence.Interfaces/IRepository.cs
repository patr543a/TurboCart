namespace TurboCart.Infrastructure.Persistence.Interfaces;

public interface IRepository<T>
{
    void Add(T entity);
    T? GetById(object entityId);
    IEnumerable<T> GetAll();
}
