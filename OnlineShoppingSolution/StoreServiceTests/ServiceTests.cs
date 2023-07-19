using Common.Domain.Shop.Aggregates;
using Common.Domain.Shop.Events;
using FluentAssertions;
using Marten;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using StoreService;

namespace StoreServiceTests;

public class ServiceTests
{
    private Service _service;
    private ILogger<Service> _logger;
    private IDocumentSession _session;
    
    public ServiceTests()
    {
        _logger = Substitute.For<ILogger<Service>>();
        _session = Substitute.For<IDocumentSession>();
        _service = new Service(_logger, _session);
    }

    [Fact]
    public async Task CreateNewSession_ShouldStartStream()
    {
        // Arrange
        var context = Substitute.For<ConsumeContext<NewSession>>();
        context.Message.Returns(new NewSession());

        // Act
        await _service.CreateNewSession(context);

        // Assert
        _session.Events.Received().StartStream<NewSession>(context.Message.Id, context.Message);
    }

    [Fact]
    public async Task AddItem_ShouldAppendEvent()
    {
        // Arrange
        var context = Substitute.For<ConsumeContext<AddItem>>();
        context.Message.Returns(new AddItem());

        // Act
        await _service.AddItem(context);

        // Assert
        _session.Events.Received().Append(context.Message.BasketId, context.Message);
    }

    [Fact]
    public async Task RemoveItem_ShouldAppendEvent()
    {
        // Arrange
        var context = Substitute.For<ConsumeContext<RemoveItem>>();
        context.Message.Returns(new RemoveItem());

        // Act
        await _service.RemoveItem(context);

        // Assert
        _session.Events.Received().Append(context.Message.BasketId, context.Message);
    }

    [Fact]
    public async Task GetSession_ShouldAggregateStream()
    {
        // Arrange
        var context = Substitute.For<ConsumeContext<GetSession>>();
        context.Message.Returns(new GetSession());

        // Act
        await _service.GetSession(context);

        // Assert
        await _session.Events.Received().AggregateStreamAsync<ShopSessionAggregate>(context.Message.Id);
    }

    [Fact]
    public void CheckOut_ShouldThrowNotImplementedException()
    {
        // Arrange
        var context = Substitute.For<ConsumeContext<CheckOut>>();
        context.Message.Returns(new CheckOut());

        // Act && Assert
        _service.CheckOut(context).Exception.Should().BeOfType<AggregateException>();

    }
}