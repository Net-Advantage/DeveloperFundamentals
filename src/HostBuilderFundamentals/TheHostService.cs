using Microsoft.Extensions.Hosting;

namespace HostBuilderFundamentals;

public class TheHostService : IHostedService
{
	//public TheHostService(
	//	TheSingletonService theSingletonService,
	//	TheScopedService theScopedService,
	//	TheTransientService theTransientService)
	//{

	//}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine("The host service is starting up!");
		await Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine("The host service is shutting down!");
		await Task.CompletedTask;
	}
}