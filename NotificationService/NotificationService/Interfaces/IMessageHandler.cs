using ServiceDesk.BLL.Interfaces;

namespace NotificationService.Interfaces;
public interface IMessageHandler
{
    public Task HandleMessageAddedTicket(ITicketAdded message);

    public Task HandleMessageUpdatedTicketStatus(ITicketStatusChanged message);
}
