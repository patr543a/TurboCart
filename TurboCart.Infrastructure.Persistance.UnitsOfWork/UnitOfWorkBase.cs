using Microsoft.EntityFrameworkCore;
using TurboCart.Infrastructure.Persistence.Interfaces;

namespace TurboCart.Infrastructure.Persistance.UnitOfWorks;

public abstract class UnitOfWorkBase(DbContext? _dbContext)
    : IUnitOfWork
{
    public virtual void Commit()
    {
        try
        {
            _dbContext!.SaveChanges();
        }
        catch (Exception e)
        {
            throw new UnitOfWorkException("Unable to commit transaction.", e);
        }
    }    

    public virtual void Dispose()
    {
        try
        {
            _dbContext?.Dispose();
        }
        catch (Exception e)
        {
            throw new UnitOfWorkException("Unable to dispose.", e);
        }
        finally
        {
            _dbContext = null;
        }

        GC.SuppressFinalize(this);
    }

    public virtual void Rollback()
        => throw new UnitOfWorkException("Unable to perform action.");
}
