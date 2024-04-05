using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationService.Interfaces;
using NotificationService.Services;
using System.Net.Mail;

namespace NotificationService.DI;
public static class DependencyRegister
{
    public static IServiceProvider AddServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        var services = new ServiceCollection()
            .AddLogging(loggerBuilder =>
            {
                loggerBuilder.ClearProviders();
                loggerBuilder.AddConsole();
            })
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<IEmailSender, EmailSender>()
            .AddTransient<SmtpClient>()
            .AddSingleton<IMessageHandler, MessageHandler>()
            .AddSingleton<IBusConfigureManager, BusConfigureManager>()
            .BuildServiceProvider();

        return services;
    }
}
