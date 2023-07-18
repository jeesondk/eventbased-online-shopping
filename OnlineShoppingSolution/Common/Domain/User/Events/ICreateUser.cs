namespace Common.Domain.User.Events;

public interface ICreateUser
{
    Entities.User User { get; set; }
    string Password { get; set; }
}