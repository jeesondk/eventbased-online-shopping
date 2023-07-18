using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

[Table("Address", Schema = "Users")]
[Index(nameof(UserId), IsUnique = true)]
[PrimaryKey("UserId")]
public class AddressModel
{

    [Key]
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    [MaxLength(256)]
    public string Street { get; set; } = string.Empty;
    [MaxLength(256)]
    public string City { get; set; } = string.Empty;
    [MaxLength(256)]
    public string PostalCode { get; set; } = string.Empty;
    [MaxLength(256)]
    public string Country { get; set; } = string.Empty;

    public Address ToEntity()
    {
        return new Address
        {
            Street = Street,
            City = City,
            PostalCode = PostalCode,
            Country = Country
        };
    }
}