namespace TurboCart.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}
