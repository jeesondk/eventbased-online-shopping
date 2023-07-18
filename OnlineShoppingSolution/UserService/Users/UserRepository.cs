using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Models;

namespace UserService.Users;

/// <summary>
/// Data Access Layer for User persistence
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DbSet<UserModel> _dbSet;
    private readonly UserContext _context;

    /// <summary>
    /// Default Constructor for Dependency Injection
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(UserContext context)
    {
        _context = context;
        _dbSet = context.Set<UserModel>();
    }

    /// <summary>
    /// Get existing user from datastore by UserName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns>UserModel</returns>
    public async Task<UserModel> GetUserByUserName(string userName)
    {
        var model = await _dbSet.AsNoTracking().Where(u => u.UserName == userName).SingleAsync();
        return model;
    }
    
    /// <summary>
    /// Create a new user and persis to datastore
    /// </summary>
    /// <param name="user"></param>
    public async Task CreateUser(UserModel user)
    {
        var model = await _dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}