using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;

namespace Replikit.Core.Controllers;

internal static class ServiceCollectionExtensions
{
    public static void AddControllersInternal(this IServiceCollection services)
    {
        services.AddSingleton<IModuleRoutingContributor, ControllerModuleRoutingContributor>();
    }
}
