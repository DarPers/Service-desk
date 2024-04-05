using ServiceDesk.Domain.Enums;

namespace ServiceDeskAPI.ViewModels;

public class TicketUpdatingViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Status Status { get; set; }

    public DateTime DateTimeAccepted { get; set; }
}
