using System.Diagnostics;
using System.Drawing;
using Kantaiko.ConsoleFormatting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Replikit.Core.Hosting.Logging;

public class ReplikitConsoleFormatter : ConsoleFormatter
{
    public ReplikitConsoleFormatter() : base("Replikit") { }

    private static Color GetLogLevelColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => Color.Gray,
            LogLevel.Debug => Color.Gray,
            LogLevel.Information => Color.Cyan,
            LogLevel.Warning => Color.Yellow,
            LogLevel.Error => Color.Red,
            LogLevel.Critical => Color.Red,
            LogLevel.None => Color.Cyan,
            _ => Color.Cyan
        };
    }

    private static Color GetTextColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => Color.White,
            LogLevel.Debug => Color.White,
            LogLevel.Information => Color.White,
            LogLevel.Warning => Color.Yellow,
            LogLevel.Error => Color.Red,
            LogLevel.Critical => Color.Red,
            LogLevel.None => Color.White,
            _ => Color.White
        };
    }

    private static string GetLogLevelText(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "TRC",
            LogLevel.Debug => "DBG",
            LogLevel.Information => "INF",
            LogLevel.Warning => "WRN",
            LogLevel.Error => "ERR",
            LogLevel.Critical => "CRL",
            LogLevel.None => "UNK",
            _ => "UNW"
        };
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider,
        TextWriter textWriter)
    {
        Debug.Assert(logEntry.Formatter is not null);

        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        var logLevel = logEntry.LogLevel;

        var logLevelText = Colors.FgColor(GetLogLevelText(logLevel), GetLogLevelColor(logLevel));
        var messageText = Colors.FgColor(message, GetTextColor(logLevel));

        var category = logEntry.Category.Split(".").Last();

        if (category.EndsWith("Module"))
        {
            category = category.Replace("Module", "");
        }

        textWriter.WriteLine($"[{logLevelText}] [{category}] {messageText}");

        if (logEntry.Exception is not null)
        {
            textWriter.WriteLine(Colors.FgRed(logEntry.Exception.ToString()));
        }
    }
}
