﻿using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ServiceDesk.IntegrationTests;

public class BaseIntegrationTestClass : IClassFixture<TestingWebApplicationFactory>
{
    protected readonly HttpClient _client;

    public BaseIntegrationTestClass(TestingWebApplicationFactory factory)
    {
        _client = factory.WebHost.CreateClient();
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data);
        var createdModelString = await responseCreatingModel.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TViewModel>(createdModelString)!;
    }
}
