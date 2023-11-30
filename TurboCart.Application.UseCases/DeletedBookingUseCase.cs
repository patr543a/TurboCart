using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class DeletedBookingUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IDeletedBookingUseCase
{
    public async Task<DeletedBooking?> AddDeletedBooking(Booking booking, string reason)
        => _unitOfWork
            .DeletedBookingRepository
                .AddDeletedBooking(booking, reason);

    public async Task<DeletedBooking?> AddDeletedBooking(DeletedBooking deletedBooking)
    {
        _unitOfWork
            .DeletedBookingRepository
                .Add(deletedBooking);

        return deletedBooking;
    }

    public async Task<DeletedBooking?> GetDeletedBooking(int deletedBookingId)
        => _unitOfWork
            .DeletedBookingRepository
                .GetById(deletedBookingId);

    public async Task<IEnumerable<DeletedBooking>?> GetDeletedBookings()
        => _unitOfWork
            .DeletedBookingRepository
                .GetAll(includes: "Customer");

    public async Task<IEnumerable<DeletedBooking>?> GetDeletedBookingsByCustomer(int customerId)
        => _unitOfWork 
            .DeletedBookingRepository
                .GetAll(c => c.CustomerId == customerId);
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously