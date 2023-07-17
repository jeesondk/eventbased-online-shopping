using Common.Consumer;
using Marten;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UserService.Domain.Events;
using FluentAssertions;
using MassTransit;

namespace CommonTests.UnitTests;

public class EventConsumerTests : IClassFixture<EventConsumerTestFixture>
{
    private readonly EventConsumerTestFixture _fixture;


    public EventConsumerTests(EventConsumerTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void CanConsumeCreateUser()
    {
        var eventCtx = _fixture.GetCreateUserEvent();

        var documentSessionSub = Substitute.For<IDocumentSession>();
        var contextSub = Substitute.For<ConsumeContext<CreateUser>>();

        contextSub.Message.Returns(eventCtx);
        documentSessionSub.SaveChangesAsync().Returns(Task.CompletedTask);

        var sut = new EventConsumer<CreateUser>(_fixture.GetEventConsumerLoggerSubstitute<CreateUser>(),
            documentSessionSub);
        await sut.Consume(contextSub);

        var res = documentSessionSub.Received().SaveChangesAsync();
        res.Status.Should().Be(TaskStatus.RanToCompletion);
    }

    [Fact]
    public async Task WillThrowOnSaveFailedCreateCartEvent()
    {
        var eventCtx = _fixture.GetCreateUserEvent();

        var documentSessionSub = Substitute.For<IDocumentSession>();
        var contextSub = Substitute.For<ConsumeContext<CreateUser>>();

        contextSub.Message.Returns(eventCtx);
        documentSessionSub.SaveChangesAsync().ThrowsAsync(new Exception("Unable to save to event store"));

        var sut = new EventConsumer<CreateUser>(_fixture.GetEventConsumerLoggerSubstitute<CreateUser>(),
            documentSessionSub);

        var act = () => sut.Consume(contextSub);

        await act.Should().ThrowAsync<Exception>().WithMessage("Unable to save to event store");
    }
}