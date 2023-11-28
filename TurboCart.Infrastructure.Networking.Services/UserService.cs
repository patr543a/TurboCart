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

    public async Task<User?> AddUser(string username, string password, User user)
        => await PostAsJsonAsync($"api/User/{username}/{password}", user);

    public async Task<bool?> Authenticate(string username, string password)
        => await PostAsJsonAsync<bool?>($"api/User/Auth/{username}/{password}", null);

    public async Task<bool?> DeleteUser(string username, string password)
        => await DeleteFromJsonAsync<bool?>($"api/User/{username}/{password}");

    public async Task<bool?> IsValidUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UpdateUser(string username, string password, User user)
        => await PutAsJsonAsync($"api/User/{username}/{password}", user);
}
