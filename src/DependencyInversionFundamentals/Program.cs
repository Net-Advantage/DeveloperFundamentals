using DependencyInversionFundamentals;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<TheHostService>();
services.AddSingleton<TheSingletonService>();
services.AddTransient<TheTransientService>();

var serviceProvider = services.BuildServiceProvider();

var theHostService = serviceProvider.GetRequiredService<TheHostService>();
theHostService.Output();

Console.WriteLine("Waiting 1 second...");
await Task.Delay(1000);

theHostService = serviceProvider.GetRequiredService<TheHostService>();
theHostService.Output();