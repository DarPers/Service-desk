using FluentAssertions;
using Newtonsoft.Json;
using ServiceDesk.Domain.Enums;
using ServiceDesk.IntegrationTests.TestData;
using ServiceDeskAPI.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace ServiceDesk.IntegrationTests;

public class TicketsControllerIntegrationTests : BaseIntegrationTestClass
{
    public TicketsControllerIntegrationTests(TestingWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAllTickets_WhiteData_ReturnList()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var newTicket = TestTicketViewModel.CreateTicket(user.Id);
        var newTickets = new List<TicketViewModel>();

        for (int i = 0; i < 5; i++)
        {
            var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint, newTicket);
            newTickets.Add(ticket);
        }

        //Act
        var response = await _client.GetAsync(Endpoints.TicketsEndpoint);
        var ticketsString = await response.Content.ReadAsStringAsync();
        var tickets = JsonConvert.DeserializeObject<List<TicketViewModel>>(ticketsString);

        // Assert
        tickets.Should().NotBeNull();

        for (int i = 0; i < tickets.Count; i++)
        {
            tickets[i].Id.Should().Be(newTickets[i].Id);
        }
    }

    [Fact]
    public async Task CreateTicket_ValidTicket_ReturnCreatedTicket()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var newTicket = TestTicketViewModel.CreateTicket(user.Id);

        //Act
        var response = await _client.PostAsJsonAsync(Endpoints.TicketsEndpoint, newTicket);
        var createdTicketString = await response.Content.ReadAsStringAsync();
        var createdTicket = JsonConvert.DeserializeObject<TicketViewModel>(createdTicketString);

        // Assert
        createdTicket.Should().NotBeNull();
        createdTicket?.Name.Should().Be(newTicket.Name);
    }

    [Fact]
    public async Task CreateTicket_InvalidTicket_ReturnNotFound()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var newTicket = TestTicketViewModel.CreateTicket(user.Id);

        //Act
        var response = await _client.PostAsJsonAsync(Endpoints.TicketsEndpoint, newTicket);
        var responseContentString = await response.Content.ReadAsStringAsync();
        var responseContent = JsonConvert.DeserializeObject<ErrorModel>(responseContentString);

        // Assert
        responseContent.Should().NotBeNull();
        responseContent?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetTicketFromCertainUser_ValidUserId_ReturnTicket()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);

        //Act
        var response = await _client.GetAsync(Endpoints.AddIdToEndpoint(Endpoints.TicketsWithUserIdEndpoint, user.Id));
        var ticketString = await response.Content.ReadAsStringAsync();
        var ticketViewModels = JsonConvert.DeserializeObject<List<TicketViewModel>>(ticketString);

        // Assert
        ticketViewModels.Should().NotBeNull();

        foreach (var ticket in ticketViewModels)
        {
            ticket.UserId.Should().Be(user.Id);
        }
    }

    [Fact]
    public async Task DeleteTicket_ValidTicketId_ReturnOk()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var newTicket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint, TestTicketViewModel.CreateTicket(user.Id));

        //Act
        var response = await _client.DeleteAsync(Endpoints.AddIdToEndpoint(Endpoints.TicketsEndpoint, newTicket.Id));

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateTicket_ValidTicketId_ReturnUpdatedTicket()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint, TestTicketViewModel.CreateTicket(user.Id));
        var updatedTicket = TestTicketViewModel.UpdateTicket(user.Id);

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.TicketsEndpoint, ticket.Id), updatedTicket);
        var updatedTicketString = await response.Content.ReadAsStringAsync();
        var updatedTicketFromResponse = JsonConvert.DeserializeObject<TicketViewModel>(updatedTicketString);

        // Assert
        updatedTicketFromResponse.Should().NotBeNull();
        updatedTicketFromResponse?.Id.Should().Be(ticket.Id);
    }

    [Fact]
    public async Task UpdateTicket_InvalidTicketId_ReturnUpdatedTicket()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var ticket = TestTicketViewModel.CreateTicketViewModel(user.Id);
        var updatedTicket = TestTicketViewModel.UpdateTicket(user.Id);

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.TicketsEndpoint, ticket.Id), updatedTicket);
        var responseContentString = await response.Content.ReadAsStringAsync();
        var responseContent = JsonConvert.DeserializeObject<ErrorModel>(responseContentString);

        // Assert
        responseContent.Should().NotBeNull();
        responseContent?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateTicketStatus_ValidTicketId_ReturnUpdatedTicket()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint, TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint, TestTicketViewModel.CreateTicket(user.Id));
        var newStatus = Status.Ready;

        //Act
        var response = await _client.PatchAsJsonAsync(Endpoints.AddStatusToEndpoint(
            Endpoints.AddIdToEndpoint(Endpoints.TicketsEndpoint, ticket.Id), newStatus.ToString()), newStatus);
        var updatedTicketString = await response.Content.ReadAsStringAsync();
        var updatedTicketFromResponse = JsonConvert.DeserializeObject<TicketViewModel>(updatedTicketString);

        // Assert
        updatedTicketFromResponse.Should().NotBeNull();
        updatedTicketFromResponse?.Id.Should().Be(ticket.Id);
        updatedTicketFromResponse?.Status.Should().Be(newStatus);
    }

    [Fact]
    public async Task UpdateTicketStatus_InvalidTicketId_ReturnNotFound()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var ticket = TestTicketViewModel.CreateTicketViewModel(user.Id);
        var newStatus = Status.Ready;

        //Act
        var response = await _client.PatchAsJsonAsync(Endpoints.AddStatusToEndpoint(
            Endpoints.AddIdToEndpoint(Endpoints.TicketsEndpoint, ticket.Id), newStatus.ToString()), newStatus);
        var responseContentString = await response.Content.ReadAsStringAsync();
        var responseContent = JsonConvert.DeserializeObject<ErrorModel>(responseContentString);

        // Assert
        responseContent.Should().NotBeNull();
        responseContent?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
