namespace Common.Domain.Shop.Events;

public class RemoveItem
{
    public Guid BasketId { get; set; }
    public long Sku { get; set; }
}