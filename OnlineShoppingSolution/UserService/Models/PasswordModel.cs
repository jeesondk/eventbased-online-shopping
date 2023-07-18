using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

/// <summary>
/// ORM Model: secret
/// </summary>
[Table("Password", Schema = "Users")]
[Index(nameof(UserId), IsUnique = true)]
[PrimaryKey("UserId")]
public class PasswordModel
{
    [ForeignKey("UserId-Password")]
    public Guid UserId { get; set; }
    public string Password { get; set; } = string.Empty;

    public Password ToEntity()
    {
        return new Password {secret = Password};
    }
}