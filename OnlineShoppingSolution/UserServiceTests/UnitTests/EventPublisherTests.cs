using Common.Domain.User.Entities;
using Common.Domain.User.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using UserService.Infrastructure;

namespace UserServiceTests.UnitTests;

public class EventPublisherTests
{
    private EventPublisher<GetUserResponse> _createUserEventPublisher;
    private IBus _eventBus;
    
    public EventPublisherTests()
    {
        _eventBus = Substitute.For<IBus>();
        _createUserEventPublisher = new EventPublisher<GetUserResponse>(Substitute.For<ILogger<EventPublisher<GetUserResponse>>>(), _eventBus);
    }

    [Fact]
    public async Task PublishEvent_ShouldPublishToEventBus()
    {
        // Arrange
        var getUserResponse = new GetUserResponse
        {
            User = new User
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
            }
        };

        // Act
        await _createUserEventPublisher.PublishEvent(getUserResponse);

        // Assert
        await _eventBus.Received().Publish(getUserResponse);
    }
}