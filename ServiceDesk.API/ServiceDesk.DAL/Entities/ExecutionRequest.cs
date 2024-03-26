namespace ServiceDesk.DAL.Entities;

public class ExecutionRequest : BaseEntity
{
    public Guid TicketId { get; set; }

    public Ticket Ticket { get; set; } = null!;

    public Guid ExecutorId { get; set; }

    public User Executor { get; set; } = null!;
}
