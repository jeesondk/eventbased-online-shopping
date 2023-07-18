namespace Common.Domain.User.Events;

public interface ICreateUserResponse
{
    string UserName { get; set; }
}