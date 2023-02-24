using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Sessions.Storage;

namespace Replikit.Core.Sessions.Internal;

internal static class ServiceCollectionExtensions
{
    public static void AddSessionsInternal(this IServiceCollection services)
    {
        services.AddSingleton<ISessionManager, SessionManager>();
        services.AddSingleton<ISessionStorage, MemorySessionStorage>();

        services.AddTransient<AccountSessionMiddleware>();
        services.AddTransient<ChannelSessionMiddleware>();
    }
}
