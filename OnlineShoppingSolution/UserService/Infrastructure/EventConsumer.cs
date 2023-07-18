using Common.Domain.User.Events;
using MassTransit;

namespace UserService.Infrastructure;

/// <summary>
/// Generic EventConsumer class for consuming Event from MassTransit infrastructure
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventConsumer<T>: IConsumer<T> where T : class
{
    private readonly ILogger<EventConsumer<T>> _logger;
    private readonly IUserService _service;
    
    public EventConsumer(ILogger<EventConsumer<T>> logger, IUserService service)
    {
        _logger = logger;
        _service = service;
    }
    
    public async Task Consume(ConsumeContext<T> context)
    {
        _logger.LogInformation("Consuming event type: {EventType}, with ID: {EventId}", context.Message.GetType(), context.MessageId);

        switch (context.Message.GetType().Name)
        {
            case nameof(CreateUser):
                await _service.CreateUser((context.Message as CreateUser)!);
                break;
            case nameof(GetUser):
                await _service.GetUser((context.Message as GetUser)!);
                break;
            default: throw new ArgumentException("Unknowen Event type");
        }

        _logger.LogInformation("Done processing event");
    }
}