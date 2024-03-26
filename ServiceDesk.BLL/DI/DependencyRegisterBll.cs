using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Mapping;
using ServiceDesk.BLL.Services;
using ServiceDesk.DAL.DI;

namespace ServiceDesk.BLL.DI;
public static class DependencyRegisterBll
{
    public static IServiceCollection AddBusinessLogicLevelServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDataAccessLevelServices(configuration);
        serviceCollection.AddAutoMapper(typeof(MappingProfile));
        serviceCollection.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        serviceCollection.AddScoped<ITicketService, TicketService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IExecutionRequestService, ExecutionRequestService>();
        serviceCollection.AddScoped<IMessageService, MessageService>();

        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host(configuration["RabbitMQ:HostName"], configuration["RabbitMQ:VirtualHostName"], configure =>
                {
                    configure.Username(configuration["RabbitMQ:UserName"]);
                    configure.Password(configuration["RabbitMQ:Password"]);
                });

                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });

        return serviceCollection;
    }
}
