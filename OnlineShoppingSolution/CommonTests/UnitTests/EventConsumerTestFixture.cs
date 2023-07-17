using Common.Consumer;
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
            UserName = "IronMan",
            FirstName = "Tony",
            LastName = "Stark",
            Address = "Stark drive 1",
            City = "StarkCity",
            PostalCode = "40000",
            Password = "I4mI0rnMan"
        };
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}