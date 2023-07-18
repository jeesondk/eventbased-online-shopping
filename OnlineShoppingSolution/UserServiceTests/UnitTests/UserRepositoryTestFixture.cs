using Microsoft.EntityFrameworkCore;
using NSubstitute;
using UserService.Context;
using UserService.Models;

namespace UserServiceTests.UnitTests;

public class UserRepositoryTestFixture: IDisposable
{

    private readonly UserContext _userContext;
    private readonly DbSet<UserModel> _dbSet;
    
    public DbSet<UserModel> GetDbSet() => _dbSet;
    public UserContext GetUserContext() => _userContext;

    public UserRepositoryTestFixture()
    {
        var users = new List<UserModel>
        {
            new UserModel
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
            },
            Password = new PasswordModel
            {
                Password = "secRet!"
            }
        },
            new UserModel
            {
                UserName = "TestUser2",
                Email = "testuser2@test.com",
                FirstName = "Test 2",
                LastName = "Testsen",
                Address = new AddressModel
                {
                    Street = "Tester road 223",
                    City = "Test city",
                    PostalCode = "9999",
                    Country = "Test"
                },
                Password = new PasswordModel
                {
                    Password = "secRet!"
                }
            }
        }.AsQueryable();

        _dbSet = Substitute.For<DbSet<UserModel>, IQueryable<UserModel>>();
        ((IQueryable<UserModel>)_dbSet).Provider.Returns(users.Provider);
        ((IQueryable<UserModel>)_dbSet).Expression.Returns(users.Expression);
        ((IQueryable<UserModel>)_dbSet).ElementType.Returns(users.ElementType);
        ((IQueryable<UserModel>)_dbSet).GetEnumerator().Returns(users.GetEnumerator());

        var dbContextOptions = new DbContextOptionsBuilder<UserContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _userContext = new UserContext(dbContextOptions);
        _userContext.Users.AddRange(users);
        _userContext.SaveChanges();

    }

    public void Dispose()
    {
        
    }
    
}