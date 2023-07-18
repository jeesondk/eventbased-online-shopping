namespace UserService.Infrastructure;

public interface IEventPublisher<T> where T : class
{
    Task PublishEvent(T @event);
}