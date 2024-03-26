namespace ServiceDesk.BLL.Interfaces;
public interface ITicketAdded
{
    public Guid CommandId { get; set; }

    public List<string> ExecutorsEmails { get; set; }

    public string Message { get; set; }
}
