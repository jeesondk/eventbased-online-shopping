using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserService;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserCommands _commands;
    private readonly IUserQueries _queries;

    public UserService(ILogger<UserService> logger, IUserCommands commands, IUserQueries queries)
    {
        _logger = logger;
        _commands = commands;
        _queries = queries;
    }

    public void CreateUserEvent(CreateUser @event)
    {
        throw new NotImplementedException();
    }

    public User GetUser(GetUser @event)
    {
        throw new NotImplementedException();
    }
    
}