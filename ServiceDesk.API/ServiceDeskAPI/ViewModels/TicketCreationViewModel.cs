namespace ServiceDeskAPI.ViewModels;

public class TicketCreationViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}
