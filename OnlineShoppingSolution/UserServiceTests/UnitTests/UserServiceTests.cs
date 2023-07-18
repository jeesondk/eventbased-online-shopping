using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using UserService;
using UserService.Users.Commands;
using UserService.Users.Queries;

namespace UserServiceTests.UnitTests;

public class UserServiceTests
{
    private IUserService _userService;
    private ILogger<UserService.UserService> _logger;
    private IUserCommands _userCommands;
    private IUserQueries _userQueries;
    
    public UserServiceTests()
    {
        _logger = Substitute.For<ILogger<UserService.UserService>>();
        _userCommands = Substitute.For<IUserCommands>();
        _userQueries = Substitute.For<IUserQueries>();
        
        _userService = new UserService.UserService(_logger, _userCommands, _userQueries);
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
         _userService.CreateUserEvent(createUserEvent);
        
        // Assert
        _userCommands.Received().Create(Arg.Is<User>(u => u == user)); 
    }

    [Fact]
    public async void GetUser_ShouldCallGetOnUserQueries()
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

        // Act
        var result = await _userService.GetUser(getUserEvent);

        // Assert
        result.Should().BeEquivalentTo(expectedUser);
    }
}