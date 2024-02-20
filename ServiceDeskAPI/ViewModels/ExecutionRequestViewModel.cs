namespace ServiceDeskAPI.ViewModels;

public class ExecutionRequestViewModel
{
    public Guid Id { get; set; }

    public TicketViewModel Ticket { get; set; } = null!;

    public UserViewModel ExecutorViewModel { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
