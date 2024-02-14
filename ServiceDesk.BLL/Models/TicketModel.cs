using ServiceDesk.Domain.Enums;

namespace ServiceDesk.BLL.Models;

public class TicketModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public UserModel UserModel { get; set; } = null!;

    public Status Status { get; set; }

    public DateTime DateTimeAccepted { get; set; }
}
