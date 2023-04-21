using Microsoft.Extensions.Hosting;

namespace DependencyInversionFundamentals;

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
		await Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken cancellationToken)
	{
		await Task.CompletedTask;
	}
}