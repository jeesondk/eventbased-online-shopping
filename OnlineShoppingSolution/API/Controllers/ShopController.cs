using System.Net;
using Common.Domain.Product.Entities;
using Common.Domain.Shop;
using Common.Domain.Shop.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class ShopController: Controller
{
    private readonly ILogger<ShopController> _logger;
    private readonly IRequestClient<NewSession> _createClient;
    private readonly IRequestClient<AddItem> _addClient;
    private readonly IRequestClient<RemoveItem> _removeClient;
    private readonly IRequestClient<GetSession> _getClient;

    public ShopController(ILogger<ShopController> logger, IRequestClient<NewSession> createClient, IRequestClient<AddItem> addClient, IRequestClient<RemoveItem> removeClient, IRequestClient<GetSession> getClient)
    {
        _logger = logger;
        _createClient = createClient;
        _addClient = addClient;
        _removeClient = removeClient;
        _getClient = getClient;
    }


    [HttpPost("v1/NewSession")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ShopSession), 200 )]
    public async Task<IActionResult> NewShopSession()
    {
        var request = new NewSession
        {
            Id = NewId.NextGuid()
        };

        try
        {
            var response = await _createClient.GetResponse<CreateBasketResponse>(request);
            return Ok(new ShopSession{ Id = response.Message.Id});
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }
    
    [HttpPost("v1/AddItem/{sessionId}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(long), 200 )]
    public async Task<IActionResult> AddItem([FromRoute] Guid sessionId, [FromBody] Product item)
    {
        var request = new AddItem
        {
            BasketId = sessionId,
            Item = item
        };

        try
        {
            var response = await _addClient.GetResponse<AddItemResponse>(request);
            return Ok(response.Message.Sku);
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }
    
    [HttpPost("v1/RemoveItem/{sessionId}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(long), 200)]
    public async Task<IActionResult> RemoveItem([FromRoute] Guid sessionId, long sku)
    {
        var request = new RemoveItem
        {
            BasketId = sessionId,
            Sku = sku
        };

        try
        {
            var response = await _removeClient.GetResponse<RemoveItemResponse>(request);
            return Ok(response.Message.Sku);
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }
    
    [HttpGet("v1/GetSession/{sessionId}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ShopSession), 200 )]
    public async Task<IActionResult> GetSession([FromRoute] Guid sessionId)
    {
        var request = new GetSession
        {
            Id = sessionId,
        };

        try
        {
            var response = await _getClient.GetResponse<GetBasketResponse>(request);
            return Ok(new ShopSession {Id = response.Message.Id, Items = response.Message.Items});
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }
}