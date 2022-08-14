using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Replikit.Core.Hosting.Logging;

internal static class ServiceCollectionExtensions
{
    public static void AddLoggingInternal(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole(options => options.FormatterName = "Replikit");
            builder.AddConsoleFormatter<ReplikitConsoleFormatter, ConsoleFormatterOptions>();
        });
    }
}
