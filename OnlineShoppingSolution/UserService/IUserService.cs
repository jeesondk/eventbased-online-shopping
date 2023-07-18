using Common.Domain.User.Entities;
using Common.Domain.User.Events;

namespace UserService;

public interface IUserService
{
    Task CreateUserEvent(CreateUser @event);
    Task<User> GetUser(GetUser @event);
}