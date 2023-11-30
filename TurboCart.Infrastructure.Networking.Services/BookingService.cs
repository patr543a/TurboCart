using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class BookingService
    : ServiceBase, IBookingUseCase
{
    public BookingService()
        : base(@"http://localhost:5082/")
    {
    }

    public async Task<Booking?> AddBooking(Booking booking)
        => await PostAsJsonAsync("api/Booking", booking);

    public async Task<DeletedBooking?> DeleteBooking(DeletedBooking deletedBooking)
        => await PostAsJsonAsync($"api/Booking/Delete", deletedBooking);

    public async Task<IEnumerable<Booking>?> GetAllBookings()
        => await GetFromJsonAsync<IEnumerable<Booking>>("api/Booking");

    public async Task<Booking?> GetBooking(int bookingId)
        => await GetFromJsonAsync<Booking>($"api/Booking/{bookingId}");

    public async Task<IEnumerable<Booking>?> GetBookingsForDate(DateOnly date)
        => await GetFromJsonAsync<IEnumerable<Booking>>($"api/Booking/Date/{date}");

    public async Task<IEnumerable<Booking>?> GetThisWeeksBookings()
        => await GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/Week");

    public async Task<IEnumerable<Booking>?> GetTodaysBookings()
        => await GetFromJsonAsync<IEnumerable<Booking>>("api/Booking/Today");

    public async Task<bool?> IsValidBooking(Booking booking)
        => throw new NotImplementedException();

    public async Task<Booking?> UpdateBooking(Booking booking)
        => await PutAsJsonAsync("api/Booking", booking);
}
