using LoggingFundamentals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var services = new ServiceCollection();
services.AddLogging(builder =>
{
    builder.AddConfiguration(configuration.GetSection("Logging"));
    builder.AddConsole();
});


var provider = services.BuildServiceProvider();

var logger = provider.GetRequiredService<ILogger<Program>>();

var person = new Person()
{
    Id = Guid.NewGuid(),
    Username = "TheUsername"
};

// The bad way to log - boxing
logger.LogInformation($"Person Created: Person Id: {person.Id}, Username: {person.Username}");

// The better way to log - still boxing
logger.LogInformation("Person Created: Person Id: {Id}, Username: {Username}", person.Id, person.Username);

// The best way to log - no boxing
logger.LogPersonCreated(person.Id, person.Username);

Console.Read();