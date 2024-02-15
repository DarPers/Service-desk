namespace ServiceDesk.BLL.Models;

public class ExecutionRequestModel : BaseModel
{
    public Guid TicketId { get; set; }

    public TicketModel TicketModel { get; set; } = null!;

    public Guid ExecutorId { get; set; }

    public UserModel Executor { get; set; } = null!;
}
