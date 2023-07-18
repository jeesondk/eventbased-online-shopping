using Common.Domain.User.Events;
using UserService.Infrastructure;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserService;

public class Service : IUserService
{
    private readonly ILogger<Service> _logger;
    private readonly IUserCommands _commands;
    private readonly IUserQueries _queries;
    private readonly IEventPublisher<GetUserResponse> _publishGetUserResponse;
    private readonly IEventPublisher<CreateUserResponse> _publishCreateUserResponse;

    /// <summary>
    /// Service Layer, intention is to decouple Event infrastructure and CQRS classes 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="commands"></param>
    /// <param name="queries"></param>
    /// <param name="publishGetUserResponse"></param>
    /// <param name="publishCreateUserResponse"></param>
    public Service(ILogger<Service> logger, IUserCommands commands, IUserQueries queries, IEventPublisher<GetUserResponse> publishGetUserResponse, IEventPublisher<CreateUserResponse> publishCreateUserResponse)
    {
        _logger = logger;
        _commands = commands;
        _queries = queries;
        _publishGetUserResponse = publishGetUserResponse;
        _publishCreateUserResponse = publishCreateUserResponse;
    }

    public async Task CreateUser(CreateUser @event)
    {
        await _commands.Create(@event.User);
        await _publishCreateUserResponse.PublishEvent(new CreateUserResponse {UserName = @event.User.UserName});
    }

    public async Task GetUser(GetUser @event)
    {
        var user = await _queries.Get(@event.UserName);
        await _publishGetUserResponse.PublishEvent(new GetUserResponse {User = user});
    }
    
}