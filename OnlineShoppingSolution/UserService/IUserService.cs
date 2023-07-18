using Common.Domain.User.Entities;
using Common.Domain.User.Events;

namespace UserService;

public interface IUserService
{
    void CreateUserEvent(CreateUser @event);
    User GetUser(GetUser @event);
}