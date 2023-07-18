using Common.Domain.User.Entities;
using Common.Domain.User.Events;
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
    private IUserService _userService;
    private ILogger<UserService.UserService> _logger;
    private IUserCommands _userCommands;
    private IUserQueries _userQueries;
    private readonly IEventPublisher<GetUserResponse> _publishGetUserReponse;
    private readonly IEventPublisher<CreateUserResponse> _publishCreateUserReponse;

    public UserServiceTests()
    {
        _logger = Substitute.For<ILogger<UserService.UserService>>();
        _userCommands = Substitute.For<IUserCommands>();
        _userQueries = Substitute.For<IUserQueries>();
        _publishGetUserReponse = Substitute.For<IEventPublisher<GetUserResponse>>();
        _publishCreateUserReponse = Substitute.For<IEventPublisher<CreateUserResponse>>();
        
        _userService = new UserService.UserService(_logger, _userCommands, _userQueries, _publishGetUserReponse, _publishCreateUserReponse);
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

        // Act
         _userService.CreateUser(createUserEvent);
         _publishCreateUserReponse.PublishEvent(Arg.Any<CreateUserResponse>()).Returns(Task.CompletedTask);
        
        // Assert
        _userCommands.Received().Create(Arg.Is<User>(u => u == user));
        _publishCreateUserReponse.Received().PublishEvent(Arg.Any<CreateUserResponse>());
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
        _publishGetUserReponse.PublishEvent(Arg.Any<GetUserResponse>()).Returns(Task.CompletedTask);

        // Act
        _userService.GetUser(getUserEvent);

        // Assert
        _userQueries.Received().Get(Arg.Is<string>(s => s == expectedUser.UserName));
        _publishGetUserReponse.Received().PublishEvent(Arg.Any<GetUserResponse>());
    }
}