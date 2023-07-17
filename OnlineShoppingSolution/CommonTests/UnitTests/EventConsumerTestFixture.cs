using Common.Consumer;
using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using Microsoft.Extensions.Logging;
using NSubstitute;
using UserService.Domain.Events;

namespace CommonTests.UnitTests;

public class EventConsumerTestFixture: IDisposable
{
    public ILogger<EventConsumer<T>> GetEventConsumerLoggerSubstitute<T>() where T: class
    {
        return  Substitute.For<ILogger<EventConsumer<T>>>();
    }

    public CreateUser GetCreateUserEvent()
    {
        return new CreateUser
        {
            User = new User {
            UserName = "IronMan",
            FirstName = "Tony",
            LastName = "Stark",
            Address = "Stark drive 1",
            City = "StarkCity",
            PostalCode = "40000",
            Country = "USA",
            Email = "Tony@stark.com"
            },
            Password = "I4mI0rnM4n!"

            
        };
    }
    
    public void Dispose()
    { }
}