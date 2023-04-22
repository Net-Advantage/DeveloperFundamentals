namespace DependencyInversionFundamentals;

public class TheSingletonService
{
	private readonly DateTime _createdOn = DateTime.UtcNow;
	
	public void Output()
	{
		Console.WriteLine($"TheSingletonService created on: {_createdOn}");
		Console.WriteLine($"TheSingletonService called on: {DateTime.UtcNow}");
	}
}