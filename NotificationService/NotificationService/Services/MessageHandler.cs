using Microsoft.Extensions.Logging;
using NotificationService.Constants;
using NotificationService.Interfaces;
using ServiceDesk.BLL.Interfaces;

namespace NotificationService.Services;
public class MessageHandler : IMessageHandler
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<MessageHandler> _logger;

    public MessageHandler(IEmailSender emailSender, ILogger<MessageHandler> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    public Task HandleMessageAddedTicket(ITicketAdded message)
    {
        _logger.Log(LogLevel.Information, MessageConstants.LogNewTicketMessage(message.CommandId));

        var emailTasks = message.ExecutorsEmails.Select(receiver =>
            _emailSender.SendEmail(receiver, MessageConstants.SubjectForNewTicket, message.Message));

        return Task.WhenAll(emailTasks);
    }

    public Task HandleMessageUpdatedTicketStatus(ITicketStatusChanged message)
    {
        _logger.Log(LogLevel.Information, MessageConstants.LogUpdatedTicketMessage(message.CommandId));
        return _emailSender.SendEmail(message.UserEmail, MessageConstants.SubjectForUpdatedTicket, message.Message);
    }
}
