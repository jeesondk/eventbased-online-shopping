using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core.Arguments;
using UserService;
using UserService.Infrastructure;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserServiceTests.UnitTests;

public class UserServiceTests
{
    private IService _service;
    private ILogger<UserService.Service> _logger;
    private IUserCommands _userCommands;
    private IUserQueries _userQueries;
    private readonly IEventPublisher<GetUserResponse> _publishGetUserReponse;
    private readonly IEventPublisher<CreateUserResponse> _publishCreateUserReponse;

    public UserServiceTests()
    {
        _logger = Substitute.For<ILogger<UserService.Service>>();
        _userCommands = Substitute.For<IUserCommands>();
        _userQueries = Substitute.For<IUserQueries>();
        _publishGetUserReponse = Substitute.For<IEventPublisher<GetUserResponse>>();
        _publishCreateUserReponse = Substitute.For<IEventPublisher<CreateUserResponse>>();
        
        _service = new UserService.Service(_logger, _userCommands, _userQueries);
    }

    [Fact]
    public void CreateUserEvent_ShouldCallCreateOnUserCommands()
    {
        var user = new User
        {
            UserName = "TestUser1",
            Email = "testuser1@test.com",
            FirstName = "Test 1",
            LastName = "Testsen",
            Address = new Address
            {
                Street = "Tester road 223",
                City = "Test city",
                PostalCode = "9999",
                Country = "Test"
            }

        };
        // Arrange
        var createUserEvent = new CreateUser
        {
            User = user 
        };
        var createUserContext = Substitute.For<ConsumeContext<CreateUser>>();
        createUserContext.Message.Returns(createUserEvent);

        // Act
         _service.CreateUser(createUserContext);

         // Assert
        _userCommands.Received().Create(Arg.Is<User>(u => u == user));
        //createUserContext.Received().RespondAsync(Arg.Any<CreateUserResponse>());
    }

    [Fact]
    public void GetUser_ShouldCallGetOnUserQueries()
    {
        // Arrange
        var getUserEvent = new GetUser
        {
            UserName = "TestUser1"
        };

        var expectedUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "TestUser1",
            Email = "testuser1@test.com",
            FirstName = "Test 1",
            LastName = "Testsen",
            Address = new Address
            {
                Street = "Tester road 223",
                City = "Test city",
                PostalCode = "9999",
                Country = "Test"
            }

        };

        _userQueries.Get(getUserEvent.UserName).Returns(expectedUser);
        var getUserContext = Substitute.For<ConsumeContext<GetUser>>();
        getUserContext.Message.Returns(getUserEvent);

        // Act
        _service.GetUser(getUserContext);

        // Assert
        _userQueries.Received().Get(Arg.Is<string>(s => s == expectedUser.UserName));
        //getUserContext.Received().RespondAsync(Arg.Any<GetUserResponse>());
    }
}