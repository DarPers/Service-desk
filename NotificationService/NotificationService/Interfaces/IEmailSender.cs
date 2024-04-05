namespace NotificationService.Interfaces;
public interface IEmailSender
{
    public Task SendEmail(string email, string subject, string message);
}
