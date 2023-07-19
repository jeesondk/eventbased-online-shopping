using Common.Domain.Shop.Events;
using MassTransit;

namespace StoreService;

public interface IService
{
    Task CreateNewSession(ConsumeContext<NewSession> context);
    Task AddItem(ConsumeContext<AddItem> context);
    Task RemoveItem(ConsumeContext<RemoveItem> context);
    Task GetSession(ConsumeContext<GetSession> context);
    Task CheckOut(ConsumeContext<CheckOut> context);
}