using Common.Domain.Shop.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using StoreService;
using StoreService.Infrastructure;

namespace StoreServiceTests;

public class EventConsumerTests
{
    private EventConsumer<NewSession> _newSessionEventConsumer;
    private EventConsumer<AddItem> _addItemEventConsumer;
    private EventConsumer<RemoveItem> _removeItemEventConsumer;
    private EventConsumer<GetSession> _getSessionEventConsumer;
    private EventConsumer<CheckOut> _checkoutEventConsumer;
    
    private ILogger<EventConsumer<NewSession>> _newSessionLogger;
    private ILogger<EventConsumer<AddItem>> _addItemLogger;
    private ILogger<EventConsumer<RemoveItem>> _removeItemLogger;
    private ILogger<EventConsumer<GetSession>> _getSessionLogger;
    private ILogger<EventConsumer<CheckOut>> _checkOutLogger;
    private IService _service;
    
    public EventConsumerTests()
    {
        _newSessionLogger = Substitute.For<ILogger<EventConsumer<NewSession>>>();
        _addItemLogger = Substitute.For<ILogger<EventConsumer<AddItem>>>();
        _removeItemLogger = Substitute.For<ILogger<EventConsumer<RemoveItem>>>();
        _getSessionLogger = Substitute.For<ILogger<EventConsumer<GetSession>>>();
        _checkOutLogger = Substitute.For<ILogger<EventConsumer<CheckOut>>>();
        _service = Substitute.For<IService>();
        
        _newSessionEventConsumer = new EventConsumer<NewSession>(_newSessionLogger, _service);
        _addItemEventConsumer = new EventConsumer<AddItem>(_addItemLogger, _service);
        _removeItemEventConsumer = new EventConsumer<RemoveItem>(_removeItemLogger, _service);
        _getSessionEventConsumer = new EventConsumer<GetSession>(_getSessionLogger, _service);
        _checkoutEventConsumer = new EventConsumer<CheckOut>(_checkOutLogger, _service);
    }

    [Fact]
    public async Task Consume_NewSession_ShouldCallCreateNewSessionOnService()
    {
        // Arrange
        var newSessionContext = Substitute.For<ConsumeContext<NewSession>>();
        newSessionContext.Message.Returns(new NewSession());

        // Act
        await _newSessionEventConsumer.Consume(newSessionContext);
        
        // Assert
        await _service.Received().CreateNewSession(Arg.Any<ConsumeContext<NewSession>>());
    }

    [Fact]
    public async Task Consume_AddItem_ShouldCallAddItemOnService()
    {
        // Arrange
        var addItemContext = Substitute.For<ConsumeContext<AddItem>>();
        addItemContext.Message.Returns(new AddItem());

        // Act
        await _addItemEventConsumer.Consume(addItemContext);

        // Assert
        await _service.Received().AddItem(Arg.Any<ConsumeContext<AddItem>>());
    }
    
    [Fact]
    public async Task Consume_GetSession_ShouldCallCreateNewSessionOnService()
    {
        // Arrange
        var getSessionContext = Substitute.For<ConsumeContext<GetSession>>();
        getSessionContext.Message.Returns(new GetSession());

        // Act
        await _getSessionEventConsumer.Consume(getSessionContext);
        
        // Assert
        await _service.Received().GetSession(Arg.Any<ConsumeContext<GetSession>>());
    }

    [Fact]
    public async Task Consume_RemoveItem_ShouldCallAddItemOnService()
    {
        // Arrange
        var removeItemContext = Substitute.For<ConsumeContext<RemoveItem>>();
        removeItemContext.Message.Returns(new RemoveItem());

        // Act
        await _removeItemEventConsumer.Consume(removeItemContext);

        // Assert
        await _service.Received().RemoveItem(Arg.Any<ConsumeContext<RemoveItem>>());
    }
    
    [Fact]
    public async Task Consume_CheckOut_ShouldCallAddItemOnService()
    {
        // Arrange
        var checkOutContext = Substitute.For<ConsumeContext<CheckOut>>();
        checkOutContext.Message.Returns(new CheckOut());

        // Act
        await _checkoutEventConsumer.Consume(checkOutContext);

        // Assert
        await _service.Received().CheckOut(Arg.Any<ConsumeContext<CheckOut>>());
    }
}