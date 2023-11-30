using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class DeletedBookingUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IDeletedBookingUseCase
{
    public async Task<DeletedBooking> AddDeletedBooking(Booking booking, string reason)
        => _unitOfWork
            .DeletedBookingRepository
                .AddDeletedBooking(booking, reason);
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously