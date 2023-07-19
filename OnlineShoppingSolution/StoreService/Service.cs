using Common.Domain.Shop.Aggregates;
using Common.Domain.Shop.Events;
using Marten;
using MassTransit;

namespace StoreService;

/// <summary>
/// Service class used to handle events 
/// </summary>
public class Service : IService
{
    private readonly ILogger<Service> _logger;
    private readonly IDocumentSession _eventSession;

    public Service(ILogger<Service> logger, IDocumentSession eventSession)
    {
        _logger = logger;
        _eventSession = eventSession;
    }
    
    /// <summary>
    /// Creates a new session (shopping cart) and returns the Id to the caller
    /// </summary>
    /// <param name="context"></param>
    public async Task CreateNewSession(ConsumeContext<NewSession> context)
    {
        try
        {
            _eventSession.Events.StartStream<NewSession>(context.Message.Id, context.Message);
            await _eventSession.SaveChangesAsync();
            await context.RespondAsync<NewSessionResponse>(new { Id = context.Message.Id});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    
    /// <summary>
    /// Adds an item to the session (shopping cart) and returns the SKU of the item to the caller
    /// </summary>
    /// <param name="context"></param>
    public async Task AddItem(ConsumeContext<AddItem> context)
    {
        try
        {
            await AppendEvent(context.Message.BasketId, context.Message);
            await context.RespondAsync<AddItemResponse>(new { Sku = context.Message.Item.Sku});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    
    /// <summary>
    /// Removes an item from the session based on the SKU and returns the SKU to the caller
    /// </summary>
    /// <param name="context"></param>
    public async Task RemoveItem(ConsumeContext<RemoveItem> context)
    {
        try
        {
            await AppendEvent(context.Message.BasketId, context.Message);
            await context.RespondAsync<RemoveItemResponse>(new {Sku = context.Message.Sku});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    
    /// <summary>
    /// Materializes an event aggregate for a given session and returns the session (shopping cart) to the caller
    /// </summary>
    /// <param name="context"></param>
    public async Task GetSession(ConsumeContext<GetSession> context)
    {
        try
        {
            var session = await  _eventSession.Events.AggregateStreamAsync<ShopSessionAggregate>(context.Message.Id);
            await context.RespondAsync<GetSessionResponse>(new { Id = context.Message.Id, Items = session?.Items});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    
    /// <summary>
    /// NOT IMPLEMENTED! - Checks out the session, and sends it for payment as well as order processing
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task CheckOut(ConsumeContext<CheckOut> context)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Appends events to session
    /// </summary>
    /// <param name="streamId"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    private async Task AppendEvent<T>(Guid streamId, T item)
    {
        try
        {
            _eventSession.Events.Append(streamId, item);
            await _eventSession.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}