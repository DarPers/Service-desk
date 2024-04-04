using Microsoft.Extensions.Configuration;
using NotificationService.Interfaces;
using System.Net.Mail;

namespace NotificationService.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _client;
    private readonly IConfiguration _config;

    public EmailSender(SmtpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
    }

    public async Task SendEmail(string email, string subject, string message)
    {
        var mail = _config["Smtp:MainMail"];

        try
        {
            _client.Host = _config["Smtp:SmtpHostName"]!;
            _client.Credentials = new System.Net.NetworkCredential(mail, _config["Smtp:PasswordForApp"]);
            _client.Port = 587;
            _client.EnableSsl = true;
            await _client.SendMailAsync(new MailMessage(from: mail, to: email, subject, message));

        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
