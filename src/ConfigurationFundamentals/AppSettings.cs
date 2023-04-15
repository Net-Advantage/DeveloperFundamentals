namespace ConfigurationFundamentals;

public class AppSettings
{
	public string AppName { get; set; } = default!;
	public SecretSettings SecretSettings { get; set; } = default!;
}

public class SecretSettings
{
	public string SuperSecret { get; set; } = default!;
}