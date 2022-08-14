using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.GlobalServices;

internal static class ServiceCollectionExtensions
{
    public static void AddGlobalServicesInternal(this IServiceCollection services)
    {
        services.AddSingleton<IGlobalMessageService, GlobalMessageService>();
        services.AddSingleton<IGlobalMemberService, GlobalMemberService>();
        services.AddSingleton<IGlobalAdapterRepository, GlobalAdapterRepository>();
        services.AddSingleton<IGlobalChannelService, GlobalChannelService>();
    }
}
