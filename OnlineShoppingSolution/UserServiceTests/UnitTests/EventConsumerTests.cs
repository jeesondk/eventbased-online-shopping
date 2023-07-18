using Common.Domain.User.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using UserService;
using UserService.Infrastructure;

namespace UserServiceTests.UnitTests;

public class EventConsumerTests
{
    private readonly EventConsumer<CreateUser> _createUserEventConsumer;
    private readonly EventConsumer<GetUser> _getUserEventConsumer;
    private readonly ILogger<EventConsumer<CreateUser>> _createUserLogger;
    private readonly ILogger<EventConsumer<GetUser>> _getUserLogger;
    private readonly IService _service;
    
    public EventConsumerTests()
    {
        _createUserLogger = Substitute.For<ILogger<EventConsumer<CreateUser>>>();
        _getUserLogger = Substitute.For<ILogger<EventConsumer<GetUser>>>();
        _service = Substitute.For<IService>();
        
        _createUserEventConsumer = new EventConsumer<CreateUser>(_createUserLogger, _service);
        _getUserEventConsumer = new EventConsumer<GetUser>(_getUserLogger, _service);
    }

    [Fact]
    public async Task Consume_CreateUser_ShouldCallCreateUserOnService()
    {
        // Arrange
        var createUserContext = Substitute.For<ConsumeContext<CreateUser>>();
        createUserContext.Message.Returns(new CreateUser());

        // Act
        await _createUserEventConsumer.Consume(createUserContext);
        
        // Assert
        await _service.Received().CreateUser(Arg.Any<ConsumeContext<CreateUser>>());
    }

    [Fact]
    public async Task Consume_GetUser_ShouldCallGetUserOnService()
    {
        // Arrange
        var getUserContext = Substitute.For<ConsumeContext<GetUser>>();
        getUserContext.Message.Returns(new GetUser());

        // Act
        await _getUserEventConsumer.Consume(getUserContext);

        // Assert
        await _service.Received().GetUser(Arg.Any<ConsumeContext<GetUser>>());
    }
}