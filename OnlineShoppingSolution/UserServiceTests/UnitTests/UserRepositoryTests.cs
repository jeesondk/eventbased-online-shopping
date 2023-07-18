using FluentAssertions;
using UserService.Models;
using UserService.Users;

namespace UserServiceTests.UnitTests;

public class UserRepositoryTest: IClassFixture<UserRepositoryTestFixture>
{
    private IUserRepository _userRepository;
    private readonly UserRepositoryTestFixture _fixture;


    public UserRepositoryTest(UserRepositoryTestFixture fixture)
    {
        _fixture = fixture;
        _userRepository = new UserRepository(_fixture.GetUserContext());
    }

    [Fact]
    public async Task GetUserByUserName_ShouldReturnUserModel()
    {
        // Arrange
        string testUserName = "TestUser1";

        // Act
        var result = await _userRepository.GetUserByUserName(testUserName);

        // Assert
        result.Should().NotBeNull();
        result.UserName.Should().Be(testUserName);
    }

    [Fact]
    public async Task CreateUser_ShouldAddNewUser()
    {
        // Arrange
        var newUser = new UserModel
        {
            UserName = "TestUser3",
            Email = "testuser3@test.com",
            FirstName = "Test 3",
            LastName = "Testsen",
            Address = new AddressModel
            {
                Street = "Tester road 223",
                City = "Test city",
                PostalCode = "9999",
                Country = "Test"
            }
        };
        
        // Act
        await _userRepository.CreateUser(newUser);
        
        // Assert
        _fixture.GetUserContext().Users.Contains(newUser).Should().BeTrue();
    }
}