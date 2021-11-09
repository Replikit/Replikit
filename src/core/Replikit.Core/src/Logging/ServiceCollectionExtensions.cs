using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Replikit.Core.Logging;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitLogging(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole(options => options.FormatterName = "Replikit");
            builder.AddConsoleFormatter<ReplikitConsoleFormatter, ConsoleFormatterOptions>();
        });
    }
}
