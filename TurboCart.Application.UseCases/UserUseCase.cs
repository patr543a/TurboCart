using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class UserUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IUserUseCase
{
    public async Task<User?> AddUser(string username, string password, User user)
    {
        if (!await IsValidUser(user) ?? false)
            throw new ArgumentException("User was not valid", nameof(user));

        _unitOfWork
            .UserRepository
                .Add(user);

        _unitOfWork.Commit();

        return user;
    }

    public Task<bool?> Authenticate(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> DeleteUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool?> IsValidUser(User user)
    {
        if (user.Username is null || user.Username.Length > 50)
            return false;

        if (user.Password is null || user.Password.Length > 20)
            return false;

        return true;
    }

    public Task<User?> UpdateUser(string username, string password, User user)
    {
        throw new NotImplementedException();
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously