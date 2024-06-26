﻿using Microsoft.EntityFrameworkCore;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Infrastructure.Persistance.UnitOfWorks;

public class TurboCartUnitOfWork(
    DbContext _dbContext,
    IBookingRepository _bookingRepository,
    ICustomerRepository _customerRepository,
    IUserRepository _userRepository,
    IDeletedBookingRepository _deletedBookingRepository)
    : UnitOfWorkBase(_dbContext), ITurboCartUnitOfWork
{
    public IBookingRepository BookingRepository => _bookingRepository;
    public ICustomerRepository CustomerRepository => _customerRepository;
    public IUserRepository UserRepository => _userRepository;
    public IDeletedBookingRepository DeletedBookingRepository => _deletedBookingRepository;
}
