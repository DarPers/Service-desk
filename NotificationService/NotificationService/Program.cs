using Microsoft.Extensions.DependencyInjection;
using NotificationService.DI;
using NotificationService.Interfaces;

var services = DependencyRegister.AddServices();

var bus = services.GetService<IBusConfigureManager>()!.SetUpBus();

await bus.StartAsync();

while (Console.ReadKey().Key != ConsoleKey.Q) { }

await bus.StopAsync();
