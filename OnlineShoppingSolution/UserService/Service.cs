using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using MassTransit;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserService;

public class Service : IService
{
    private readonly ILogger<Service> _logger;
    private readonly IUserCommands _commands;
    private readonly IUserQueries _queries;

    /// <summary>
    /// Service Layer, intention is to decouple Event infrastructure and CQRS classes 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="commands"></param>
    /// <param name="queries"></param>
    /// <param name="publishGetUserResponse"></param>
    /// <param name="publishCreateUserResponse"></param>
    public Service(ILogger<Service> logger, IUserCommands commands, IUserQueries queries)
    {
        _logger = logger;
        _commands = commands;
        _queries = queries;
    }

    public async Task CreateUser(ConsumeContext<CreateUser> context)
    {
        _logger.LogInformation("Creating User...");
        
        await _commands.Create(context.Message.User);
        _logger.LogInformation("User Created: {UserName}", context.Message.User.UserName);
        await context.RespondAsync<CreateUserResponse>(new {UserName = context.Message.User.UserName});
    }

    public async Task GetUser(ConsumeContext<GetUser> context)
    {
        _logger.LogInformation("Get User...");

        var user = await _queries.Get(context.Message.UserName);
        _logger.LogInformation("User retrieved: {UserName}", context.Message.UserName);
        await context.RespondAsync<GetUserResponse>(new { User = user});
    }
    
}