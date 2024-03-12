using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ServiceDesk.IntegrationTests;
public class BaseIntegrationTestClass : IClassFixture<TestingWebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;

    public BaseIntegrationTestClass(TestingWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data);
        var createdModelString = await responseCreatingModel.Content.ReadAsStringAsync();
        JsonConvert.DeserializeObject<TViewModel>(createdModelString);
        return JsonConvert.DeserializeObject<TViewModel>(createdModelString);
    }
}
