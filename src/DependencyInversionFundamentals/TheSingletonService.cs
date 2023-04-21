namespace DependencyInversionFundamentals;

public class TheSingletonService
{
	public TheSingletonService()
	{
		Console.WriteLine($"TheSingletonService created: {DateTime.UtcNow}");
	}
}