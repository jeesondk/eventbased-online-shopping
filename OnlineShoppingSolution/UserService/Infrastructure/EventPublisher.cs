using MassTransit;

namespace UserService.Infrastructure;

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