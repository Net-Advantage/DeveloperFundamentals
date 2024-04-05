using Microsoft.Extensions.Logging;

namespace LoggingFundamentals;

public static partial class GlobalLogging
{
    [LoggerMessage(EventId = 1,
    Level = LogLevel.Information,
    Message = "Person Created: Person Id: {Id}, Username: {Username}")]
    public static partial void LogPersonCreated(this ILogger logger, Guid id, string username);
}
