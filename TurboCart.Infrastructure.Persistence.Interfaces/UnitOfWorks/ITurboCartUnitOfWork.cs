using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

public interface ITurboCartUnitOfWork
    : IUnitOfWork
{
    IBookingRepository BookingRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IUserRepository UserRepository { get; }
}
