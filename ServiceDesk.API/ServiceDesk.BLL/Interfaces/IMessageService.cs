using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Interfaces;
public interface IMessageService
{
    public Task SendCommandChangedStatus(Ticket ticket, string userEmail);

    public Task SendCommandTicketAdded(TicketModel ticket, List<string> usersEmails);
}
