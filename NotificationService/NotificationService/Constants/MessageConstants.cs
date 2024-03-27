namespace NotificationService.Constants;
public static class MessageConstants
{
    public const string BusListeningMessage = "Listening new messages...";
    public const string CommonSubject = "ServiceDesk: ";
    public const string SubjectForNewTicket = CommonSubject + "NEW ticket!";
    public const string SubjectForUpdatedTicket = CommonSubject + "Yout ticket's status was updated!";

    public static string LogNewTicketMessage(Guid id)
    {
        return $"Get message with ID: {id} about added ticket.";
    }

    public static string LogUpdatedTicketMessage(Guid id)
    {
        return $"Get message with ID: {id} about updated ticket.";
    }
}
