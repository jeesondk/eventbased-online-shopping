using Common.Domain.User.Entities;

namespace UserService.Users.Queries;

/// <summary>
/// CQRS Query collection for User datastore
/// </summary>
public class UserQueries : IUserQueries
{
    private readonly IUserRepository _repository;

    public UserQueries(IUserRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Get User by UserName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>User</returns>
    public async Task<User> Get(string userName)
    {
        var res = await _repository.GetUserByUserName(userName);
        return res.ToEntity();
    }
}