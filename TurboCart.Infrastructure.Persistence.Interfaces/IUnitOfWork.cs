namespace TurboCart.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
    : IDisposable
{
    void Commit();
    void Rollback();
}
