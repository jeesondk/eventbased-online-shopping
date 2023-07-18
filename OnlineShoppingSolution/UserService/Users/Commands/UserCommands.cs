using Common.Domain.User.Entities;
using UserService.Users.Extentions;

namespace UserService.Users.Commands;

/// <summary>
/// CQRS Command collection for User datastore
/// </summary>
public class UserCommands : IUserCommands
{
    private readonly IUserRepository _repository;

    public UserCommands(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(User user)
    {
        var model = user.ToModel();
        await _repository.CreateUser(model);
    }
    
}