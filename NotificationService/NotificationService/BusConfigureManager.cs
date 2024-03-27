using MassTransit;
using NotificationService.Constants;
using ServiceDesk.BLL.Interfaces;

namespace NotificationService;
public static class BusConfigureManager
{
    public static IBusControl SetUpBus()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(SettingConstants.RabbitMqHostName, SettingConstants.RabbitMqVirtualHost, h =>
            {
                h.Username(SettingConstants.RabbitMqUserName);
                h.Password(SettingConstants.RabbitMqPassword);
            });

            cfg.ReceiveEndpoint(SettingConstants.RabbitMqQueueName, ep =>
            {
                ep.Handler<ITicketStatusChanged>(context => MessageHandler.HandleMessageUpdatedTicketStatus(context.Message));
                ep.Handler<ITicketAdded>(async context => await MessageHandler.HandleMessageAddedTicket(context.Message));
            });
        });

        return bus;
    }
}
