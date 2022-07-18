using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Internal;

namespace Replikit.Core.Controllers;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitControllers(this IServiceCollection services)
    {
        services.AddSingleton<ControllerHandlerAccessor>();

        services.AddSingleton<IControllerIntrospectionInfoAccessor>(sp =>
            sp.GetRequiredService<ControllerHandlerAccessor>());
    }
}
