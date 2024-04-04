using MassTransit;
using Microsoft.Extensions.Configuration;
using NotificationService.Interfaces;
using ServiceDesk.BLL.Interfaces;

namespace NotificationService.Services;
public class BusConfigureManager : IBusConfigureManager
{
    private readonly IMessageHandler _messageHandler;
    private readonly IConfiguration _configuration;

    public BusConfigureManager(IMessageHandler messageHandler, IConfiguration configuration)
    {
        _messageHandler = messageHandler;
        _configuration = configuration;
    }

    public IBusControl SetUpBus()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(_configuration["RabbitMq:HostName"], _configuration["RabbitMq:VirtualHost"], h =>
            {
                h.Username(_configuration["RabbitMq:UserName"]);
                h.Password(_configuration["RabbitMq:Password"]);
            });

            cfg.ReceiveEndpoint(_configuration["RabbitMq:QueueName"], ep =>
            {
                ep.Handler<ITicketStatusChanged>(context => _messageHandler.HandleMessageUpdatedTicketStatus(context.Message));
                ep.Handler<ITicketAdded>(async context => await _messageHandler.HandleMessageAddedTicket(context.Message));
            });
        });

        return bus;
    }
}
