using System.ComponentModel.DataAnnotations;
using ServiceDesk.Domain.Enums;

namespace ServiceDeskAPI.ViewModels;

public class TicketViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Status Status { get; set; } 

    public DateTime DateTimeAccepted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
