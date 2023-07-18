using System.Net;
using API.Entites;
using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class UserController: Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IRequestClient<CreateUser> _createUserClient;


    public UserController(ILogger<UserController> logger, IRequestClient<CreateUser> createUserClient)
    {
        _logger = logger;
        _createUserClient = createUserClient;
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateUser([FromBody] NewUser payload)
    {
        var request = new CreateUser();
        request.User = payload.User;
        request.User.Password = new Password {secret = payload.Password};
        
        try
        {
            var response = await _createUserClient.GetResponse<CreateUserResponse>(request);
            return Ok(response.Message.UserName);
        }
        catch (RequestException rx)
        {
            _logger.LogError(rx.Message);
            return StatusCode((int) HttpStatusCode.RequestTimeout);
        }
        
    }

    /*[HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserByUserName([FromQuery(Name = "UserName")] string userName)
    {
        var request = new GetUser
        {
            UserName = userName
        };
        var response = await _clientGetUser.GetResponse<GetUserResponse>(request);

        return Ok(response.Message.User);
    }*/
}