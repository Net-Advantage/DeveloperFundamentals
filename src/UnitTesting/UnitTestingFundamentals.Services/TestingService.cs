namespace UnitTestingFundamentals.Services
{
	public class TestingService
	{
		private string? _lastValue;

		public string EchoString(string value)
		{
			_lastValue = value;
			return value;
		}

		private int EchoInt(int value)
		{
			return value;
		}
	}
}
