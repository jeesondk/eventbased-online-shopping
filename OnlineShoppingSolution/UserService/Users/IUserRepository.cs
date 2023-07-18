using Common.Domain.User.Entities;
using UserService.Models;

namespace UserService.Users;

public interface IUserRepository
{
    Task<UserModel> GetUserByUserName(string userName);
    Task CreateUser(UserModel user);
}