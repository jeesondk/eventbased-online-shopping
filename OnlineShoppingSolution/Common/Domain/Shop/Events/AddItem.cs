namespace Common.Domain.Shop.Events;

public class AddItem
{
    public Guid BasketId { get; set; }
    public Product.Entities.Product Item { get; set; } = new();
}