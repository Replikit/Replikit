using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Replikit.Core.Hosting;

public static class ReplikitHostBuilderExtensions
{
    public static void AddDevelopmentUserSecrets<T>(this IHostBuilder hostBuilder) where T : class
    {
        hostBuilder.ConfigureAppConfiguration((context, configurationBuilder) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<T>();
            }
        });
    }
}
