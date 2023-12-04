using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;
using TurboCart.Infrastructure.Persistence.Interfaces.UnitOfWorks;

namespace TurboCart.Application.UseCases;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public class UserUseCase(ITurboCartUnitOfWork _unitOfWork)
    : IUserUseCase
{
    public async Task<User?> AddUser(Guid guid, User user)
    {
        if (!await IsValidUser(user) ?? false)
            throw new ArgumentException("User was not valid", nameof(user));

        _unitOfWork
            .UserRepository
                .Add(user);

        _unitOfWork.Commit();

        return user;
    }

    public async Task<Guid> Authenticate(string username, string password)
    {
        var guid = _unitOfWork
            .UserRepository
                .Authenticate(username, password);

        _unitOfWork.Commit();

        return guid;
    }

    public async Task<bool?> Authenticate(Guid guid)
        => _unitOfWork
            .UserRepository
                .Authenticate(guid);

    public async Task<bool?> DeleteUser(Guid guid)
    {
        _unitOfWork
            .UserRepository
                .DeleteUser(guid);

        _unitOfWork.Commit();

        return true;
    }

    public async Task<bool?> IsValidUser(User user)
    {
        if (user.Username is null || user.Username.Length > 50)
            return false;

        if (user.Password is null || user.Password.Length > 20)
            return false;

        return true;
    }

    public async Task<User?> UpdateUser(Guid guid, User user)
    {
        _unitOfWork
            .UserRepository
                .UpdateUser(guid, user);

        return user;
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously