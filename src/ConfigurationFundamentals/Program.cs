using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ConfigurationFundamentals;

var builder = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true)
	.AddUserSecrets<Program>();

var configuration = builder.Build();


Console.WriteLine("Reading directly from configuration");
var appName = configuration["AppName"];
var superSecret = configuration["SecretSettings:SuperSecret"];
Console.WriteLine($"AppName: {appName}");
Console.WriteLine($"SecretSettings - SuperSecret: {superSecret}");


Console.WriteLine();
Console.WriteLine(new string('=', 50));
Console.WriteLine("Reading from a strongly typed class");
var appSettingsOptions = new OptionsWrapper<AppSettings>(new());
configuration.Bind(appSettingsOptions.Value);
var appSettings = appSettingsOptions.Value;
Console.WriteLine($"AppName: {appSettings.AppName}");
Console.WriteLine($"SecretSettings - SuperSecret: {appSettings.SecretSettings.SuperSecret}");
