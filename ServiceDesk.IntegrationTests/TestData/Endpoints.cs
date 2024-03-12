namespace ServiceDesk.IntegrationTests.TestData;

public class Endpoints
{
    public const string TicketsEndpoint = "/tickets";
    public const string UsersEndpoint = "/users";
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
