﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

[Table("User", Schema = "Users")]
[Index(nameof(UserName), IsUnique = true)]
[Index(nameof(Id))]
[PrimaryKey("Id")]
public class UserModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(64)]
    public string UserName { get; set; } = string.Empty;
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(256)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(256)]
    public string LastName { get; set; } = string.Empty;
    public AddressModel Address { get; set; } = new();

    public User ToEntity()
    {
        return new User
        {
            Id = Id,
            UserName = UserName,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address.ToEntity()
        };
    }
}