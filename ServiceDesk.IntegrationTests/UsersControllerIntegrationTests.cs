using FluentAssertions;
using Newtonsoft.Json;
using ServiceDesk.IntegrationTests.TestData;
using ServiceDeskAPI.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace ServiceDesk.IntegrationTests;

public class UsersControllerIntegrationTests : BaseIntegrationTestClass
{
    public UsersControllerIntegrationTests(TestingWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteUser_ValidUserId_ReturnOk()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);

        //Act
        var response = await _client.DeleteAsync(Endpoints.AddIdToEndpoint(Endpoints.UsersEndpoint, user.Id));

        //Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateUser_ValidUser_ReturnNewUser()
    {
        //Arrange
        var user = TestUserViewModel.RegistrationUser;

        //Act
        var response = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);

        //Assert
        response.Should().NotBeNull();
        response.Email.Should().Be(user.Email);
        response.LastName.Should().Be(user.LastName);
    }

    [Fact]
    public async Task UpdateUser_ValidIdUser_ReturnUpdatedUser()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var updatedUser = TestUserViewModel.UpdateUserViewModel;

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.UsersEndpoint, user.Id), updatedUser);
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<UserViewModel>(contentString);

        //Assert
        content.Should().NotBeNull();
        content?.Id.Should().Be(user.Id);
        content?.Email.Should().Be(updatedUser.Email);
        content?.FirstName.Should().Be(updatedUser.FirstName);
    }

    [Fact]
    public async Task UpdateUser_InvalidIdUser_ReturnNotFound()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var updatedUser = TestUserViewModel.UpdateUserViewModel;

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.UsersEndpoint, user.Id), updatedUser);
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ErrorModel>(contentString);

        //Assert
        content.Should().NotBeNull();
        content?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetUserById_ValidUserId_ReturnUser()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);

        //Act
        var response = await _client.GetAsync(Endpoints.AddIdToEndpoint(Endpoints.UsersEndpoint, user.Id));
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<UserViewModel>(contentString);

        // Assert
        content.Should().NotBeNull();
        content?.Id.Should().Be(user.Id);
    }

}
