namespace ServiceDesk.BLL.Models;

public class ExecutionRequest : BaseModel
{
    public Guid TicketId { get; set; }

    public Ticket Ticket { get; set; } = null!;

    public Guid ExecutorId { get; set; }

    public User Executor { get; set; } = null!;
}
