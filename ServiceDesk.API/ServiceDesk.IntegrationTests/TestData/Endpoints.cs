namespace ServiceDesk.IntegrationTests.TestData;

public static class Endpoints
{
    public const string TicketsEndpoint = "/tickets";
    public const string UsersEndpoint = "/users";
    public const string ExecutionRequestEndpoint = "/executionrequests";
    public const string ExecutionRequestWithUserIdEndpoint = ExecutionRequestEndpoint + "/executors";
    public const string ExecutionRequestWithTicketIdEndpoint = ExecutionRequestEndpoint + TicketsEndpoint;
    public const string TicketsWithUserIdEndpoint = TicketsEndpoint + UsersEndpoint;

    public static string AddIdToEndpoint(string endpoint, Guid id)
    {
        return endpoint + $"/{id}";
    }

    public static string AddStatusToEndpoint(string endpoint, string parameterValue)
    {
        return endpoint + $"?status={parameterValue}";
    }
}
