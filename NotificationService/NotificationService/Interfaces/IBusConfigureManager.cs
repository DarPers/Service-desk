using MassTransit;

namespace NotificationService.Interfaces;
public interface IBusConfigureManager
{
    public IBusControl SetUpBus();
}
