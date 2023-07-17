using UserService.Domain.enums;

namespace UserService.Domain.Events;

public class UserAction
{
    public UserActions Action { get; set; }
    public string UserName { get; set; } = string.Empty;
}