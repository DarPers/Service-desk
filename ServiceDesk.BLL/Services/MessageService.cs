using MassTransit;
using ServiceDesk.BLL.Constants;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;

namespace ServiceDesk.BLL.Services;
public class MessageService : IMessageService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task SendCommandChangedStatus(Ticket ticket, string userEmail)
    {
        return _publishEndpoint.Publish<ITicketStatusChanged>(new
        {
            CommandId = Guid.NewGuid(),
            Message = MessageConstants.CreateMessageAboutChangedStatus(ticket),
            UserEmail = userEmail

        });
    }

    public Task SendCommandTicketAdded(TicketModel ticket, List<string> usersEmails)
    {
        return _publishEndpoint.Publish<ITicketAdded>(new
        {
            CommandId = Guid.NewGuid(),
            Message = MessageConstants.CreateMessageAboutAddedTicket(ticket),
            ExecutorsEmails = usersEmails

        });
    }
}
