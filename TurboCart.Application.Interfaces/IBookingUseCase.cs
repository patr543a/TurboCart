using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface IBookingUseCase
{
    Task<Booking?> AddBooking(Booking booking);
    Task<Booking?> UpdateBooking(Booking booking);
    Task<int?> DeleteBooking(int bookingId);
    Task<bool?> IsValidBooking(Booking booking);
    Task<Booking?> GetBooking(int bookingId);
    Task<IEnumerable<Booking>?> GetAllBookings();
    Task<IEnumerable<Booking>?> GetTodaysBookings();
    Task<IEnumerable<Booking>?> GetBookingsForDate(DateOnly date);
    Task<IEnumerable<Booking>?> GetThisWeeksBookings();
}
