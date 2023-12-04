using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Infrastructure.Networking.Services;

public class UserService
    : ServiceBase, IUserUseCase
{
    public UserService()
        : base(@"http://localhost:5199/")
    {
    }

    public async Task<User?> AddUser(Guid guid, User user)
        => await PostAsJsonAsync($"api/User/{guid}", user);

    public async Task<Guid> Authenticate(string username, string password)
        => await PostAsJsonAsync<Guid?>($"api/User/Auth/{username}/{password}", null) ?? Guid.Empty;

    public async Task<bool?> Authenticate(Guid guid)
        => await PostAsJsonAsync<bool?>($"api/User/Auth/{guid}", null);

    public async Task<bool?> DeleteUser(Guid guid)
        => await DeleteFromJsonAsync<bool?>($"api/User/{guid}");

    public async Task<bool?> IsValidUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UpdateUser(Guid guid, User user)
        => await PutAsJsonAsync($"api/User/{guid}", user);
}
