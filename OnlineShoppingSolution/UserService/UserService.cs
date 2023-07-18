using Common.Domain.User.Events;
using UserService.Infrastructure;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserService;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserCommands _commands;
    private readonly IUserQueries _queries;
    private readonly IEventPublisher<GetUserResponse> _publishGetUserResponse;

    /// <summary>
    /// Service Layer, intention is to decouple Event infrastructure and CQRS classes 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="commands"></param>
    /// <param name="queries"></param>
    /// <param name="publishGetUserResponse"></param>
    public UserService(ILogger<UserService> logger, IUserCommands commands, IUserQueries queries, IEventPublisher<GetUserResponse> publishGetUserResponse )
    {
        _logger = logger;
        _commands = commands;
        _queries = queries;
        _publishGetUserResponse = publishGetUserResponse;
    }

    public async Task CreateUser(CreateUser @event)
    {
        await _commands.Create(@event.User);
    }

    public async Task GetUser(GetUser @event)
    {
        var user = await _queries.Get(@event.UserName);
        await _publishGetUserResponse.PublishEvent(new GetUserResponse
        {
            User = user
        });
    }
    
}