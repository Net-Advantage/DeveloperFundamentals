using DependencyInversionFundamentals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("The host being set up!");

var hostBuilder = Host.CreateApplicationBuilder();
hostBuilder.Services.AddHostedService<TheHostService>();
	//.CreateDefaultBuilder()
	//.ConfigureServices(services =>
	//{
	//	services.AddHostedService<TheHostService>();

	//	services.AddSingleton<TheSingletonService>();
	//	services.AddTransient<TheTransientService>();
	//})
var host = hostBuilder.Build();

Console.WriteLine("The host is starting up!");
await host.RunAsync();
Console.WriteLine("The application has ended!");