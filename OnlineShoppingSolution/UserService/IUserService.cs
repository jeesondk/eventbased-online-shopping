using Common.Domain.User.Events;

namespace UserService;

public interface IUserService
{
    void CreateUserEvent(CreateUser @event);
    void GetUser(GetUser @event);
}