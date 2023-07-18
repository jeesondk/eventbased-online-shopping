using Common.Domain.User.Entities;

namespace UserService.Users.Queries;

public interface IUserQueries
{
    /// <summary>
    /// Get User by UserName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>User</returns>
    Task<User> Get(string userName);
}