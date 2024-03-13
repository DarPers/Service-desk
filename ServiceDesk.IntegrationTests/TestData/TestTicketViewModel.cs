using ServiceDesk.Domain.Enums;
using ServiceDeskAPI.ViewModels;

namespace ServiceDesk.IntegrationTests.TestData;

public class TestTicketViewModel
{
    public static TicketCreationViewModel CreateTicket(Guid userId) => new()
    {
        Name = "Laptop",
        Description = "I need laptop",
        UserId = userId
    };

    public static TicketViewModel CreateTicketViewModel(Guid userId) => new()
    {
        Id = Guid.NewGuid(),
        Name = "Laptop",
        Description = "I need laptop",
        UserId = userId,
        DateTimeAccepted = new DateTime(2024, 03, 12),
        Status = Status.Free
    };

    public static TicketUpdatingViewModel UpdateTicket(Guid userId) => new()
    {
        Name = "Mouse",
        Description = "I need a mouse",
        UserId = userId,
        Status = Status.InProgress,
        DateTimeAccepted = new DateTime(2024, 03, 12)
    };
}
