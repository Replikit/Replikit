using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;

namespace Replikit.Core.Handlers;

internal static class ServiceCollectionExtensions
{
    public static void AddHandlersInternal(this IServiceCollection services)
    {
        services.AddSingleton<IModuleRoutingContributor, HandlerModuleRoutingContributor>();
    }
}
