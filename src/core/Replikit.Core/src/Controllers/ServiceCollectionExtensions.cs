using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Internal;
using Replikit.Core.Controllers.Lifecycle;

namespace Replikit.Core.Controllers;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitControllers(this IServiceCollection services)
    {
        services.AddSingleton<ControllerHandlerAccessor>();

        services.AddSingleton<IControllerIntrospectionInfoAccessor>(sp =>
            sp.GetRequiredService<ControllerHandlerAccessor>());

        services.AddSingleton<IControllerLifecycle, ControllerLifecycle>();
    }
}
