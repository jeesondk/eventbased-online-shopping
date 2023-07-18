namespace Common.Domain.User.Events;

public class CreateUser
{
    public Entities.User User { get; set; } = new();
}