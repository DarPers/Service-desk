namespace ServiceDeskAPI.ViewModels;

public class ExecutionRequestViewModel
{
    public Guid Id { get; set; }

    public Guid TicketId { get; set; }

    public Guid ExecutorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
