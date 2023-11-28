using Microsoft.EntityFrameworkCore;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.Repositories;

namespace TurboCart.Infrastructure.Persistance.Repositories;

public class CustomerRepository(DbContext _dbContext)
    : RepositoryBase<Customer, int>(_dbContext), ICustomerRepository
{
    protected override int GetEntityId(Customer entity)
        => entity.CustomerId;
}
