using UserService.Domain.enums;

namespace Common.Domain.User.Events;

public class CreateUser
{
    public EventActions Action { get; set; }
    public Entities.User User { get; set; } = new();
    public string Password { get; set; } = string.Empty;
}