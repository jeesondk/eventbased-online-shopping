using Common.Domain.User.Entities;
using UserService.Models;

namespace UserService.Users.Extentions;

public static class UserEnityToModel
{
    
    public static AddressModel ToModel(this Address address)
    {
        return new AddressModel
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }
    
    public static UserModel ToModel(this User user)
    {
        return new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address.ToModel()
        };
    }
}