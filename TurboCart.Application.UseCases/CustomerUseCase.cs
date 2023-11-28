using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class CustomerUseCase(ITurboCartUnitOfWork _unitOfWork)
    : ICustomerUseCase
{
    public async Task<bool?> IsValidCustomer(Customer customer)
    {
        if (customer.Name is null || customer.Name.Length > 100)
            return false;

        if (customer.Bookings is null)
            return false;

        return true;
    }

    public async Task<Customer?> GetCustomer(int customerId)
        => _unitOfWork
            .CustomerRepository
                .GetById(customerId, "Bookings");

    public async Task<Customer?> AddCustomer(Customer customer)
    {
        if (!await IsValidCustomer(customer) ?? false)
            throw new ArgumentException("Customer was not valid", nameof(customer));

        _unitOfWork
            .CustomerRepository
                .Add(customer);

        _unitOfWork.Commit();

        return customer;
    }

    public async Task<int?> DeleteCustomer(int customerId)
    {
        _unitOfWork
            .CustomerRepository
                .Delete(customerId);

        _unitOfWork.Commit();

        return customerId;
    }    

    public async Task<Customer?> UpdateCustomer(Customer customer)
    {
        if (!await IsValidCustomer(customer) ?? false)
            throw new ArgumentException("Customer was not valid", nameof(customer));

        _unitOfWork
            .CustomerRepository
                .Update(customer);

        _unitOfWork.Commit();

        return customer;
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously