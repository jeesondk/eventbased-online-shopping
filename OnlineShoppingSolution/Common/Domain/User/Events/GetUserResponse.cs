namespace Common.Domain.User.Events;

public class GetUserResponse
{
    public Entities.User User { get; set; } = new Entities.User();
}