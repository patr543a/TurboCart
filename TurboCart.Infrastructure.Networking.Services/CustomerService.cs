using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class CustomerService
    : ServiceBase, ICustomerUseCase
{
    public CustomerService()
        : base(@"http://localhost:5082/")
    { 
    }

    public async Task<Customer?> AddCustomer(Customer customer)
        => await PostAsJsonAsync("api/Customer", customer);

    public async Task<int?> DeleteCustomer(int customerId)
        => await DeleteFromJsonAsync<int>($"api/Customer/{customerId}");

    public async Task<Customer?> GetCustomer(int customerId)
        => await GetFromJsonAsync<Customer>($"api/Customer/{customerId}");

    public async Task<IEnumerable<Customer>?> GetAllCustomers()
        => await GetFromJsonAsync<IEnumerable<Customer>>($"api/Customer");

    public async Task<bool?> IsValidCustomer(Customer customer)
        => throw new NotImplementedException();

    public async Task<Customer?> UpdateCustomer(Customer customer)
        => await PutAsJsonAsync("api/Customer", customer);


}
