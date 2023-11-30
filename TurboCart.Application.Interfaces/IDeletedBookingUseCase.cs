using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface IDeletedBookingUseCase
{
    Task<DeletedBooking> AddDeletedBooking(Booking booking, string reason);
}
