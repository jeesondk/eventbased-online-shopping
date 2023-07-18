using Common.Domain.User.Entities;
using Common.Domain.User.Events;

namespace UserService;

public interface IUserService
{
    Task CreateUser(CreateUser @event);
    Task GetUser(GetUser @event);
}