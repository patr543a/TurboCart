using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class DeletedBookingRepository(DbContext _dbContext)
    : RepositoryBase<DeletedBooking, int>(_dbContext), IDeletedBookingRepository
{
    public DeletedBooking AddDeletedBooking(Booking booking, string reason)
    {
        var deleted = new DeletedBooking(booking, reason);

        _set.Add(deleted);

        return deleted;
    }

    protected override int GetEntityId(DeletedBooking entity)
        => entity.DeletedBookingId;
}
