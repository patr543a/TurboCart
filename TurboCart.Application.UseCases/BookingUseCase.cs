using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class BookingUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IBookingUseCase
{
    public async Task<bool?> IsValidBooking(Booking booking)

    {
        if (booking.Start > DateTime.Today - new TimeSpan(1, 0, 0, 0, 0, 0))
            return false;

        if (booking.Customer is null && booking.CustomerId == 0)
            return false;

        return true;
    }

    public async Task<Booking?> GetBooking(int bookingId)
        => _unitOfWork
            .BookingRepository
                .GetById(bookingId, includes: "Customer");

    public async Task<IEnumerable<Booking>?> GetAllBookings()
        => _unitOfWork
            .BookingRepository
                .GetAll(includes: "Customer");


    public async Task<IEnumerable<Booking>?> GetTodaysBookings()
        => _unitOfWork
            .BookingRepository
                .GetTodaysBookings();

    public async Task<Booking?> AddBooking(Booking booking)
    {
        if (!await IsValidBooking(booking) ?? false)
            throw new ArgumentException("Booking was not valid", nameof(booking));

        _unitOfWork
            .BookingRepository
                .Add(booking);

        _unitOfWork.Commit();

        return booking;
    }

    public async Task<Booking?> UpdateBooking(Booking booking)
    {
        if (!await IsValidBooking(booking) ?? false)
            throw new ArgumentException("Booking was not valid", nameof(booking));

        _unitOfWork
            .BookingRepository
                .Update(booking);

        _unitOfWork.Commit();

        return booking;
    }

    public async Task<int?> DeleteBooking(int bookingId)
    {
        _unitOfWork
            .BookingRepository
                .Delete(bookingId);

        _unitOfWork.Commit();

        return bookingId;
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously