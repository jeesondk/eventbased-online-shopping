using System.Net;
using API.Entites;
using Common.Domain.User.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/// <summary>
/// API Controller for handling "Create and Read" User actions
/// </summary>
[Route("api/[controller]")]
public class UserController: Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IRequestClient<CreateUser> _createUserClient;
    private readonly IRequestClient<GetUser> _getUserClient;


    public UserController(ILogger<UserController> logger, IRequestClient<CreateUser> createUserClient, IRequestClient<GetUser> getUserClient)
    {
        _logger = logger;
        _createUserClient = createUserClient;
        _getUserClient = getUserClient;
    }

    /// <summary>
    /// Creates a new User based on a "NewUser" object
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateUser([FromBody] NewUser payload)
    {
        var request = new CreateUser
        {
            User = payload.User,
        };

        try
        {
            var response = await _createUserClient.GetResponse<CreateUserResponse>(request);
            return Ok(new { userName = response.Message.UserName });
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }

    /// <summary>
    /// Get a user from the database based on UserName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserByUserName([FromQuery(Name = "UserName")] string userName)
    {
        var request = new GetUser
        {
            UserName = userName
        };

        try
        {
            var response = await _getUserClient.GetResponse<GetUserResponse>(request);
            return Ok(response.Message.User);
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
    }
}