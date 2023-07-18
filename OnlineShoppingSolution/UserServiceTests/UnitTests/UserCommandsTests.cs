using Common.Domain.User.Entities;
using NSubstitute;
using UserService.Models;
using UserService.Users;
using UserService.Users.Commands;

namespace UserServiceTests.UnitTests;

public class UserCommandsTests
{
    private IUserCommands _userCommands;
    private IUserRepository _userRepository;

    public UserCommandsTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userCommands = new UserCommands(_userRepository);
    }

    [Fact]
    public async Task Create_ShouldCallCreateUserInRepository()
    {
        // Arrange
        var user = new User
        {
            UserName = "TestUser1",
            FirstName = "Test1",
            LastName = "Testensen",
            Email = "test1@test.com",
            Address = new Address()
        };

        // Act
        await _userCommands.Create(user);
        
        // Assert
        await _userRepository.Received().CreateUser(Arg.Is<UserModel>(u => u.UserName == user.UserName)); 
        // Ensure the argument matches the correct conversion from User to UserModel
    }
}