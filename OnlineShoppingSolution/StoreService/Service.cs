using Common.Domain.Shop.Events;
using MassTransit;

namespace StoreService;

public class Service : IService
{
    public async Task CreateNewSession(ConsumeContext<NewSession> context)
    {
        throw new NotImplementedException();
    }
    
    public async Task AddItem(ConsumeContext<AddItem> context)
    {
        throw new NotImplementedException();
    }
    
    public async Task RemoveItem(ConsumeContext<RemoveItem> context)
    {
        throw new NotImplementedException();
    }
    
    public async Task GetSession(ConsumeContext<GetSession> context)
    {
        throw new NotImplementedException();
    }
    
    public async Task CheckOut(ConsumeContext<CheckOut> context)
    {
        throw new NotImplementedException();
    }
}