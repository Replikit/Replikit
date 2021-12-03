using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.GlobalServices;

public static class ServiceCollectionExtensions
{
    public static void AddReplikitGlobalServices(this IServiceCollection services)
    {
        services.AddSingleton<IGlobalMessageService, GlobalMessageService>();
    }
}
