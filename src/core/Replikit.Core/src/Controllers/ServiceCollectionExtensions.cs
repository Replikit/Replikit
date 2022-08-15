using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Configuration;
using Replikit.Core.Controllers.Converters;
using Replikit.Core.Controllers.Options;
using Replikit.Core.Modularity;

namespace Replikit.Core.Controllers;

public static class ServiceCollectionExtensions
{
    internal static void AddControllersInternal(this IServiceCollection services)
    {
        services.AddSingleton<IModuleRoutingContributor, ControllerModuleRoutingContributor>();

        services.ConfigureReplikitControllers(builder =>
        {
            //
            builder.RegisterConverter<IdentifierConverter>();
        });
    }

    public static void ConfigureReplikitControllers(this IServiceCollection services,
        Action<ControllerConfigurationBuilder> configureDelegate)
    {
        services.Configure<GlobalControllerOptions>(options => options.ConfigureDelegates.Add(configureDelegate));
    }
}
