using NotificationService;
using NotificationService.Constants;

var bus = BusConfigureManager.SetUpBus();

await bus.StartAsync();

Console.WriteLine(MessageConstants.BusListeningMessage);

Console.ReadKey();

await bus.StopAsync();
