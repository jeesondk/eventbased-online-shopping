using Common.Domain.User.Events;
using MassTransit;

namespace UserService;

public interface IService
{
    Task CreateUser(ConsumeContext<CreateUser> context);
    Task GetUser(ConsumeContext<GetUser> context);
}