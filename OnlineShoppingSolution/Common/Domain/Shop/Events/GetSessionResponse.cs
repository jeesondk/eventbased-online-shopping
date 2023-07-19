namespace Common.Domain.Shop.Events;

public class GetSessionResponse
{
    public Guid Id { get; set; }
    public List<Product.Entities.Product> Items { get; set; } = new();
}