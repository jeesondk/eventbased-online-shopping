using Common.Domain.Shop.Events;
using MassTransit;

namespace StoreService.Infrastructure;

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
            case nameof(NewSession):
                await _service.CreateNewSession((context as ConsumeContext<NewSession>)!);
                break;
            case nameof(AddItem):
                await _service.AddItem((context as ConsumeContext<AddItem>)!);
                break;
            case nameof(RemoveItem):
                await _service.RemoveItem((context as ConsumeContext<RemoveItem>)!);
                break;
            case nameof(GetSession):
                await _service.GetSession((context as ConsumeContext<GetSession>)!);
                break;
            case nameof(CheckOut):
                await _service.CheckOut((context as ConsumeContext<CheckOut>)!);
                break;
            default: throw new ArgumentException("Unknown Event type");
        }

        _logger.LogInformation("Done processing event");
    }
}