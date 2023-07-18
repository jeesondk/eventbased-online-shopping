using MassTransit;

namespace UserService.Infrastructure;
/// <summary>
/// Generic EventPublisher for publishing events to MassTransit infrastructure
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventPublisher<T> : IEventPublisher<T> where T : class
{
    private readonly IBus _eventbus;

    public EventPublisher(ILogger<EventPublisher<T>> logger, IBus eventbus)
    {
        _eventbus = eventbus;
    }

    public async Task PublishEvent(T @event)
    {
        await _eventbus.Publish<T>(@event);
    }
}