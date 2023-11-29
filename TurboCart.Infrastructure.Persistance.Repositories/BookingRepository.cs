using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class BookingRepository(DbContext _dbContext)
    : RepositoryBase<Booking, int>(_dbContext), IBookingRepository
{
    public IEnumerable<Booking>? GetBookingsForDate(DateOnly date)
        => GetAll(b => b.Start.Date == date.ToDateTime(new TimeOnly()), includes: "Customer");

    public IEnumerable<Booking>? GetThisWeeksBookings()
        => GetAll(b => b.Start.Date <= DateTime.Today.AddDays(7), includes: "Customer");

    public IEnumerable<Booking>? GetTodaysBookings()
        => GetAll(b => b.Start.Date == DateTime.Today, includes: "Customer");

    protected override int GetEntityId(Booking entity)
        => entity.BookingId;
}
