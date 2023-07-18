namespace UserService.Infrastructure;

public interface IEventPublisher<in T> where T : class
{
    Task PublishEvent(T @event);
}