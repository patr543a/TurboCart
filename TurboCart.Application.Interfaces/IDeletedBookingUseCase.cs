using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface IDeletedBookingUseCase
{
    Task<IEnumerable<DeletedBooking>?> GetDeletedBookings();
    Task<IEnumerable<DeletedBooking>?> GetDeletedBookingsByCustomer(int customerId);
    Task<DeletedBooking?> GetDeletedBooking(int deletedBookingId);
    Task<DeletedBooking?> AddDeletedBooking(Booking booking, string reason);
    Task<DeletedBooking?> AddDeletedBooking(DeletedBooking deletedBooking);
}
