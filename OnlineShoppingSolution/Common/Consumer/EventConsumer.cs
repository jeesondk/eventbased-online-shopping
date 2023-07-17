using Marten;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Consumer;

public class EventConsumer<T>: IConsumer<T> where T : class 
{
    public EventConsumer(ILogger<EventConsumer<T>> logger, IDocumentSession documentSessionSub)
    {
        throw new NotImplementedException();
    }

    public Task Consume(ConsumeContext<T> context)
    {
        throw new NotImplementedException();
    }
}