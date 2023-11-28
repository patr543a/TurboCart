using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IBookingRepository
    : IRepository<Booking, int>
{
    IEnumerable<Booking>? GetTodaysBookings();
}
