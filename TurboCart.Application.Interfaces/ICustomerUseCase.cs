using TurboCart.Domain.Entities;

namespace TurboCart.Application.Interfaces;

public interface ICustomerUseCase
{
    Task<bool?> IsValidCustomer(Customer customer);
    Task<Customer?> AddCustomer(Customer customer);
    Task<Customer?> UpdateCustomer(Customer customer);
    Task<int?> DeleteCustomer(int customerId);
    Task<Customer?> GetCustomer(int customerId);
}
