using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class BookingRepository(DbContext _dbContext)
    : RepositoryBase<Booking>(_dbContext), IBookingRepository
{
    public IEnumerable<Booking> GetTodaysBookings()
        => _set
        .Where(b => b.Start.Date == DateTime.Today)
        .ToArray();
}
