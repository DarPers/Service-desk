namespace ServiceDeskAPI.Constants;

public class EndpointsConstants
{
    public const string RequestWithId = "{id:Guid}";
    public const string RequestWithUsersAndId = "users/" + RequestWithId;
    public const string RequestWithExecutorAndId = "executors/" + RequestWithId;
    public const string RequestWithTicketAndId = "tickets/" + RequestWithId;
}