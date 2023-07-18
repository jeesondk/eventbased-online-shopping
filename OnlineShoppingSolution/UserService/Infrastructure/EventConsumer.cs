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
    private readonly IService _service;
    
    public EventConsumer(ILogger<EventConsumer<T>> logger, IService service)
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
                await _service.CreateUser((context as ConsumeContext<CreateUser>)!);
                break;
            case nameof(GetUser):
                await _service.GetUser((context as ConsumeContext<GetUser>)!);
                break;
            default: throw new ArgumentException("Unknown Event type");
        }

        _logger.LogInformation("Done processing event");
    }
}