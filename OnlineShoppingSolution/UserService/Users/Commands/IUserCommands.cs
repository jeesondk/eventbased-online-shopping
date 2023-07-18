using Common.Domain.User.Entities;

namespace UserService.Users.Commands;

public interface IUserCommands
{
    Task Create(User user);
}