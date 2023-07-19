using Common.Domain.Product.Entities;

namespace API.Stubs;

public interface IProductCatalogStub
{
    IReadOnlyList<Product> GetAllProducts();
}