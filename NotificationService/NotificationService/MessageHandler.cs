using NotificationService.Constants;
using ServiceDesk.BLL.Interfaces;

namespace NotificationService;
public static class MessageHandler
{
    public static async Task HandleMessageAddedTicket(ITicketAdded message)
    {
        Console.WriteLine(MessageConstants.LogNewTicketMessage(message.CommandId));
        foreach (var receiver in message.ExecutorsEmails)
        {
            await EmailSender.SendEmail(receiver, MessageConstants.SubjectForNewTicket, message.Message);

        }
    }

    public static Task HandleMessageUpdatedTicketStatus(ITicketStatusChanged message)
    {
        Console.WriteLine(MessageConstants.LogUpdatedTicketMessage(message.CommandId));
        return EmailSender.SendEmail(message.UserEmail, MessageConstants.SubjectForUpdatedTicket, message.Message);
    }
}
