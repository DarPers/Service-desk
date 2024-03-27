namespace ServiceDesk.BLL.Interfaces;

public interface ITicketStatusChanged
{
    public Guid CommandId { get; set; }

    public string UserEmail { get; set; }

    public string Message { get; set; }

}
