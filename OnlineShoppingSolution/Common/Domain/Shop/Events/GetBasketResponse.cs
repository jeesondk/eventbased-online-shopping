namespace Common.Domain.Shop.Events;

public class GetBasketResponse
{
    public Guid Id { get; set; }
    public List<Product.Entities.Product> Items { get; set; }
}