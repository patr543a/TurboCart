using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class DeletedBookingService
    : ServiceBase, IDeletedBookingUseCase
{
    public DeletedBookingService()
        : base(@"http://localhost:5082/")
    {
    }
    public Task<DeletedBooking?> AddDeletedBooking(Booking booking, string reason)
        => PostAsJsonAsync<DeletedBooking>("api/DeletedBooking", new(booking, reason));

    public Task<DeletedBooking?> AddDeletedBooking(DeletedBooking deletedBooking)
        => PostAsJsonAsync("api/DeletedBooking", deletedBooking);

    public Task<DeletedBooking?> GetDeletedBooking(int deletedBookingId)
        => GetFromJsonAsync<DeletedBooking>($"api/DeletedBooking/{deletedBookingId}");

    public Task<IEnumerable<DeletedBooking>?> GetDeletedBookings()
        => GetFromJsonAsync<IEnumerable<DeletedBooking>>("api/DeletedBooking");

    public Task<IEnumerable<DeletedBooking>?> GetDeletedBookingsByCustomer(int customerId)
        => GetFromJsonAsync<IEnumerable<DeletedBooking>>($"api/DeletedBooking/by/{customerId}");
}
