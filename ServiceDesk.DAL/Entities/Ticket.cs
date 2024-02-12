using ServiceDesk.DAL.Enums;

namespace ServiceDesk.DAL.Entities;

internal class Ticket
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Status Status { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public DateTime DateTimeAccepted { get; set; }
}
