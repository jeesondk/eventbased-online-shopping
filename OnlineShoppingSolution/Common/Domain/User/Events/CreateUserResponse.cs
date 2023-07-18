namespace Common.Domain.User.Events;

public class CreateUserResponse : ICreateUserResponse
{
    public string UserName { get; set; } = string.Empty;
}