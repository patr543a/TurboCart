using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IDeletedBookingRepository
    : IRepository<DeletedBooking, int>
{
    DeletedBooking AddDeletedBooking(Booking booking, string reason);
}
