using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

/// <summary>
/// ORM Model: secret
/// </summary>
[Table("Password", Schema = "Users")]
[Index(nameof(Id), IsUnique = true)]
public class PasswordModel 
{   
    [Key]
    public Guid Id { get; set; }
    public string Password { get; set; } = string.Empty;

    public Password ToEntity()
    {
        return new Password {secret = Password};
    }
}