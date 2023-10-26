using System.Runtime.CompilerServices;
using UnitTestingFundamentals.Services;

namespace UnitTestingFundamentals.UnitTests;

public class UnitTest1
{
	[Fact]
	public void TestPublicInstanceMethod()
	{
		// Arrange
		var value = "Darrel";
		var testingService = new TestingService();

		// Act
		var result = Caller.EchoString(testingService, value);

		//Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void TestPrivateInstanceMethod()
	{
		// Arrange
		var value = 10;
		var testingService = new TestingService();

		// Act
		var result = Caller.EchoInt(testingService, value);

		//Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void TestPrivateInstanceField()
	{
		// Arrange
		var value = "Darrel";
		var testingService = new TestingService();

		// Act
		_ = Caller.EchoString(testingService, value);
		var lastValue = Caller.LastValue(testingService);

		//Assert
		Assert.Equal(value, lastValue);
	}
}

public class Caller
{
	[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "EchoString")]
	public static extern string EchoString(TestingService testingService, string value);

	[UnsafeAccessor(UnsafeAccessorKind.Method, Name = "EchoInt")]
	public static extern int EchoInt(TestingService testingService, int value);

	[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_lastValue")]
	public static extern ref string LastValue(TestingService testingService);
}