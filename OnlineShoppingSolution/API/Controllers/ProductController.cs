using API.Stubs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Product controller, handling product catalog functions
/// This will be using Stubbed data
/// </summary>
[Route("api/[controller]")]
public class ProductController: Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductCatalogStub _products;

    public ProductController(ILogger<ProductController> logger, IProductCatalogStub products)
    {
        _logger = logger;
        _products = products;
    }
    
    [HttpGet]
    public IActionResult AllProducts()
    {
        try
        {
            var products = _products.GetAllProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
}