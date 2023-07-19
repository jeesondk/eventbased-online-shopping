namespace Common.Domain.Shop;

public class ShopSession
{
    public Guid Id { get; set; }
    public List<Product.Entities.Product> Items { get; set; } = new ();
}