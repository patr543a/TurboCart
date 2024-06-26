﻿using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

public interface IBookingRepository
    : IRepository<Booking, int>
{
    IEnumerable<Booking>? GetTodaysBookings();
    IEnumerable<Booking>? GetBookingsForDate(DateOnly date);
    IEnumerable<Booking>? GetThisWeeksBookings();
}
