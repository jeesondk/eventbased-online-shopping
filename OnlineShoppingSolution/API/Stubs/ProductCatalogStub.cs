
using Common.Domain.Product.Entities;

namespace API.Stubs;

public class ProductCatalogStub : IProductCatalogStub
{
    private IReadOnlyList<Product> _products;
    
    public ProductCatalogStub()
    {
        _products = new[]
        {
            new Product
            {
                Id = 1,
                Sku = 10001,
                Name = "Mountain Bike - Pro",
                Price = 1200.99
            },
            new Product
            {
                Id = 2,
                Sku = 10002,
                Name = "Road Bike - Advanced",
                Price = 1500.99
            },
            new Product
            {
                Id = 3,
                Sku = 10003,
                Name = "City Bike - Basic",
                Price = 500.00
            },
            new Product
            {
                Id = 4,
                Sku = 10004,
                Name = "Electric Bike - Advanced",
                Price = 2200.50
            },
            new Product
            {
                Id = 5,
                Sku = 10005,
                Name = "Folding Bike - Compact",
                Price = 800.00
            },
            new Product
            {
                Id = 6,
                Sku = 10006,
                Name = "BMX Bike - Pro",
                Price = 600.99
            },
            new Product
            {
                Id = 7,
                Sku = 10007,
                Name = "Hybrid Bike - Comfort",
                Price = 950.00
            },
            new Product
            {
                Id = 8,
                Sku = 10008,
                Name = "Kids Bike - Starter",
                Price = 300.00
            },
            new Product
            {
                Id = 9,
                Sku = 10009,
                Name = "Women's Bike - Advanced",
                Price = 1300.00
            },
            new Product
            {
                Id = 10,
                Sku = 10010,
                Name = "Cruiser Bike - Classic",
                Price = 700.00
            }
        };
    }
    
    public IReadOnlyList<Product> GetAllProducts()
    {
        return _products;
    }
}