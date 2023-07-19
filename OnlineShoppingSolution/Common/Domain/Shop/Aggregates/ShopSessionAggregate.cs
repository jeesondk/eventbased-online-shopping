using Common.Domain.Shop.Events;
using JasperFx.Core;

namespace Common.Domain.Shop.Aggregates;

public class ShopSessionAggregate
{

    public Guid Id { get; set; }
    public List<Product.Entities.Product> Items => _products;
    
    private List<Product.Entities.Product> _products = new();

    public void Apply(AddItem add)
    {
        _products.Fill(add.Item);
    }
    
    public void Apply(RemoveItem remove)
    {
        var itemToRemove = _products.FirstOrDefault(x => x.Sku == remove.Sku);
        if(itemToRemove != null)
            _products.Remove(itemToRemove);
    }
}