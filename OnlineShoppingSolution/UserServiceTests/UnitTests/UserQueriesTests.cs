using FluentAssertions;
using NSubstitute;
using UserService.Models;
using UserService.Users;
using UserService.Users.Queries;

namespace UserServiceTests.UnitTests;

public class UserQueriesTests
{
    private IUserQueries _userQueries;
    private IUserRepository _userRepository;

    public UserQueriesTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userQueries = new UserQueries(_userRepository);
    }

    [Fact]
    public async Task Get_ShouldReturnUserFromRepository()
    {
        // Arrange
        string testUserName = "TestUser1";
        var userModel = new UserModel
        {
            UserName = "TestUser1",
            Email = "testuser1@test.com",
            FirstName = "Test 1",
            LastName = "Testsen",
            Address = new AddressModel
            {
                Street = "Tester road 223",
                City = "Test city",
                PostalCode = "9999",
                Country = "Test"
            }
        };

        _userRepository.GetUserByUserName(testUserName).Returns(userModel);
        
        // Act
        var result = await _userQueries.Get(testUserName);

        // Assert
        result.Should().NotBeNull();
        result.UserName.Should().Be(testUserName);
    }
}