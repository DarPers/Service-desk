using FluentAssertions;
using Newtonsoft.Json;
using ServiceDesk.IntegrationTests.TestData;
using ServiceDeskAPI.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace ServiceDesk.IntegrationTests;

public class ExecutionRequestControllerIntegrationTests : BaseIntegrationTestClass
{
    public ExecutionRequestControllerIntegrationTests(TestingWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteExecutionRequest_ValidExecutionRequestId_ReturnOk()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var executionRequest = await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
        (Endpoints.ExecutionRequestEndpoint, TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id));

        //Act
        var response =
            await _client.DeleteAsync(
                Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestEndpoint, executionRequest.Id));

        //Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateExecutionRequest_ValidExecutionRequestModel_ReturnExecutionRequest()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var executionRequest = await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
            (Endpoints.ExecutionRequestEndpoint, newExecutionRequest);

        //Assert
        executionRequest.Should().NotBeNull();
        executionRequest.TicketId.Should().Be(newExecutionRequest.TicketId);
        executionRequest.ExecutorId.Should().Be(newExecutionRequest.ExecutorId);
    }

    [Fact]
    public async Task CreateExecutionRequest_InvalidTicketId_ReturnBadRequest()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = TestTicketViewModel.CreateTicketViewModel(user.Id);
        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var response = await _client.PostAsJsonAsync(Endpoints.ExecutionRequestEndpoint, newExecutionRequest);
        var responseContentString = await response.Content.ReadAsStringAsync();
        var responseContent = JsonConvert.DeserializeObject<ErrorModel>(responseContentString);

        // Assert
        responseContent.Should().NotBeNull();
        responseContent?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task CreateExecutionRequest_InvalidUserId_ReturnBadRequest()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var response = await _client.PostAsJsonAsync(Endpoints.ExecutionRequestEndpoint, newExecutionRequest);
        var responseContentString = await response.Content.ReadAsStringAsync();
        var responseContent = JsonConvert.DeserializeObject<ErrorModel>(responseContentString);

        // Assert
        responseContent.Should().NotBeNull();
        responseContent?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task UpdateExecutionRequest_ValidExecutionRequestId_ReturnExecutionRequest()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var executionRequest = await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
            (Endpoints.ExecutionRequestEndpoint, TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id));

        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestEndpoint, executionRequest.Id), newExecutionRequest);
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ExecutionRequestViewModel>(contentString);

        //Assert
        content.Should().NotBeNull();
        content?.TicketId.Should().Be(newExecutionRequest.TicketId);
        content?.ExecutorId.Should().Be(newExecutionRequest.ExecutorId);
    }

    [Fact]
    public async Task UpdateExecutionRequest_InvalidExecutionRequestModel_ReturnExecutionRequest()
    {
        //Arrange
        var user = TestUserViewModel.CreateUserViewModel;
        var ticket = TestTicketViewModel.CreateTicketViewModel(user.Id);
        var executionRequest = await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
            (Endpoints.ExecutionRequestEndpoint, TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id));

        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestEndpoint, executionRequest.Id), newExecutionRequest);
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ErrorModel>(contentString);

        //Assert
        content.Should().NotBeNull();
        content?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateExecutionRequest_InvalidExecutionRequestId_ReturnExecutionRequest()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var executionRequest = TestExecutionRequestViewModel.CreateExecutionRequestViewModel(user.Id, ticket.Id);
        var newExecutionRequest = TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id);

        //Act
        var response = await _client.PutAsJsonAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestEndpoint, executionRequest.Id), newExecutionRequest);
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ErrorModel>(contentString);

        //Assert
        content.Should().NotBeNull();
        content?.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetExecutionRequestsByExecutor_ValidExecutionRequestId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var newExecutionRequests = await AddListOfExecutionRequestsToDatabase(user.Id, ticket.Id);

        //Act
        var response =
            await _client.GetAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestWithUserIdEndpoint, user.Id));
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<ExecutionRequestViewModel>>(contentString);

        // Assert
        CheckingResponseList(content!, newExecutionRequests);
    }

    [Fact]
    public async Task GetExecutionRequestsByTicket_ValidExecutionRequestId_ReturnListOfExecutionRequests()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var newExecutionRequests = await AddListOfExecutionRequestsToDatabase(user.Id, ticket.Id);

        //Act
        var response =
            await _client.GetAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestWithTicketIdEndpoint, ticket.Id));
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<ExecutionRequestViewModel>>(contentString);

        // Assert
        CheckingResponseList(content!, newExecutionRequests);
    }

    [Fact]
    public async Task GetExecutionRequestById_ValidExecutionRequestId_ReturnExecutionRequest()
    {
        //Arrange
        var user = await AddModelToDatabase<UserViewModel, UserRegistrationViewModel>(Endpoints.UsersEndpoint,
            TestUserViewModel.RegistrationUser);
        var ticket = await AddModelToDatabase<TicketViewModel, TicketCreationViewModel>(Endpoints.TicketsEndpoint,
            TestTicketViewModel.CreateTicket(user.Id));
        var executionRequest = await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
            (Endpoints.ExecutionRequestEndpoint, TestExecutionRequestViewModel.CreateExecutionRequest(user.Id, ticket.Id));

        //Act
        var response =
            await _client.GetAsync(Endpoints.AddIdToEndpoint(Endpoints.ExecutionRequestEndpoint, executionRequest.Id));
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ExecutionRequestViewModel>(contentString);

        // Assert
        content.Should().NotBeNull();
        content?.Id.Should().Be(executionRequest.Id);
    }

    private void CheckingResponseList(List<ExecutionRequestViewModel> content, List<ExecutionRequestViewModel> initialList)
    {
        content.Should().NotBeNull();

        for (int i = 0; i < content?.Count; i++)
        {
            content[i].Id.Should().Be(initialList[i].Id);
        }
    }

    private async Task<List<ExecutionRequestViewModel>> AddListOfExecutionRequestsToDatabase(Guid userId, Guid ticketId)
    {
        var newExecutionRequests = new List<ExecutionRequestViewModel>();

        for (int i = 0; i < 5; i++)
        {
            var executionRequest =
                await AddModelToDatabase<ExecutionRequestViewModel, ExecutionRequestCreationViewModel>
                (Endpoints.ExecutionRequestEndpoint,
                    TestExecutionRequestViewModel.CreateExecutionRequest(userId, ticketId));
            newExecutionRequests.Add(executionRequest);
        }

        return newExecutionRequests;
    }
}
