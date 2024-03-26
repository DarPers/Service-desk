using ServiceDeskAPI.ViewModels;

namespace ServiceDesk.IntegrationTests.TestData;
public class TestExecutionRequestViewModel
{
    public static ExecutionRequestCreationViewModel CreateExecutionRequest(Guid userId, Guid ticketId) => new()
    {
        TicketId = ticketId,
        ExecutorId = userId
    };

    public static ExecutionRequestViewModel CreateExecutionRequestViewModel(Guid userId, Guid ticketId) => new()
    {
        Id = Guid.NewGuid(),
        TicketId = ticketId,
        ExecutorId = userId
    };
}
