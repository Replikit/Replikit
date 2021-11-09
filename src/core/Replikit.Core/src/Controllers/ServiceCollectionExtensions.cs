using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Internal;

namespace Replikit.Core.Controllers;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitControllers(this IServiceCollection services)
    {
        services.AddSingleton<RequestHandlerAccessor>();

        services.AddSingleton(sp => sp.GetRequiredService<RequestHandlerAccessor>().RequestHandler.Info);
    }
}
