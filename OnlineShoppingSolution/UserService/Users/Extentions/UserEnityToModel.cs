using Common.Domain.User.Entities;
using UserService.Models;

namespace UserService.Users.Extentions;

public static class UserEnityToModel
{
    
    private static AddressModel ToModel(this Address address)
    {
        return new AddressModel
        {
            Street = address.Street,
            City = address.City,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }

    private static PasswordModel ToModel(this Password password)
    {
        return new PasswordModel
        {
            Password = password.secret
        };
    }
    
    public static UserModel ToModel(this User user)
    {
        return new UserModel
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password.ToModel(),
            Address = user.Address.ToModel()
        };
    }
}