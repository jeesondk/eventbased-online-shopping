namespace UserService.Domain.Events;

public class SignInUser
{
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}