using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.ViewModels;

namespace ServiceDesk.IntegrationTests.TestData;
public static class TestUserViewModel
{
    public static UserRegistrationViewModel RegistrationUser => new()
    {
        FirstName = "Daniel",
        LastName = "Barkov",
        Email = "daniel@gmail.com",
        Password = "in1357THyHTR",
        Role = Role.User
    };

    public static UserViewModel CreateUserViewModel => new()
    {
        Id = Guid.NewGuid(),
        FirstName = "Daniel",
        LastName = "Barkov",
        Email = "daniel@gmail.com",
    };
}
