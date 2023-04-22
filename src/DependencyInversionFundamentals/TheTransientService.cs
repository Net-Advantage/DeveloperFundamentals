namespace DependencyInversionFundamentals;

public class TheTransientService
{
	private readonly DateTime _createdOn = DateTime.UtcNow;

	public void Output()
	{
		Console.WriteLine($"TheTransientService created on: {_createdOn}");
		Console.WriteLine($"TheTransientService called on: {DateTime.UtcNow}");
	}
}