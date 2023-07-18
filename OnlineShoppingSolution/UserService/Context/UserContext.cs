using Common.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Context;

/// <summary>
/// EntityFramework Context class for underlying data store
/// </summary>
public class UserContext: DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<AddressModel> Addresses { get; set; }

    public UserContext(DbContextOptions<UserContext> options): base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {  }
    
}