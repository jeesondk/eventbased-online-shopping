using Marten;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Common.Consumer;

public class EventConsumer<T>: IConsumer<T> where T : class 
{
    private readonly ILogger<EventConsumer<T>> _logger;
    private readonly IDocumentSession _session;

    public EventConsumer(ILogger<EventConsumer<T>> logger, IDocumentSession session)
    {
        _logger = logger;
        _session = session;
    }

    public async Task Consume(ConsumeContext<T> context)
    {
        _logger.LogInformation("Consuming event type: {EventType}, with ID: {EventId}", context.Message.GetType(), context.MessageId);

        if (context.Message.GetType().Name.Contains("Create"))
            await HandleCreate(context);
        else
           await  HandleAppend(context);
        
        _logger.LogInformation("Done processing event");
    }

    private async Task HandleCreate(ConsumeContext<T> context)
    {
        _session.Events.StartStream<T>(context.GetHeader(EVENTCONST.STREAM_ID_HEADER), context.Message);
        await SaveChanges();
    }

    private async Task HandleAppend(ConsumeContext<T> context)
    {
        _session.Events.Append(context.GetHeader(EVENTCONST.EVENT_ID_HEADER), context.Message);
        await SaveChanges();
    }

    private async Task SaveChanges()
    {
        try
        { 
            await _session.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}