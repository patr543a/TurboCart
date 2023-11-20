using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

public class BookingUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IBookingUseCase
{
    public void BookNew(DateTime dateTime, Customer customer)
    {
        var booking = new Booking() { 
            Start = dateTime, 
            Customer = customer 
        };

        if (customer.CustomerId == 0)
            _unitOfWork
                .CustomerRepository
                    .Add(customer);
        
        _unitOfWork
            .BookingRepository
                .Add(booking);

        _unitOfWork.Commit();
    }

    public IEnumerable<Booking> GetAllBookings()
        => _unitOfWork
            .BookingRepository
                .GetAll();

    public IEnumerable<Booking> GetTodaysBookings()
        => _unitOfWork
            .BookingRepository
                .GetTodaysBookings();
}
