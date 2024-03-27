using NotificationService.Constants;
using System.Net.Mail;

namespace NotificationService;

public static class EmailSender
{
    public static async Task SendEmail(string email, string subject, string message)
    {
        var mail = SettingConstants.MainMail;

        try
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = SettingConstants.SmtpHostName;
                client.Credentials = new System.Net.NetworkCredential(mail, SettingConstants.PasswordForApp);
                client.Port = 587;
                client.EnableSsl = true;
                await client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));
            }
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
