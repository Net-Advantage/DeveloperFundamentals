namespace DependencyInversionFundamentals;

public class TheHostService
{
	private readonly TheSingletonService _theSingletonService;
	private readonly TheTransientService _theTransientService;

	public TheHostService(
		TheSingletonService theSingletonService,
		TheTransientService theTransientService)
	{
		_theSingletonService = theSingletonService;
		_theTransientService = theTransientService;
	}

	public void Output()
	{
		_theSingletonService.Output();
		_theTransientService.Output();
	}
}