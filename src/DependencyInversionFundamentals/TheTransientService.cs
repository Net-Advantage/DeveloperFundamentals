namespace DependencyInversionFundamentals;

public class TheTransientService
{
	public TheTransientService()
	{
		Console.WriteLine($"TheTransientService created: {DateTime.UtcNow}");
	}
}