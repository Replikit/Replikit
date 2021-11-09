using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Internal;
using Replikit.Extensions.Sessions.Options;
using Replikit.Extensions.Sessions.Services;

namespace Replikit.Extensions.Sessions;

[ModuleFlags(ModuleFlags.Library)]
public class SessionsModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public SessionsModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISessionStorageProvider, SessionStorageProvider>();
        services.AddSingleton<MemorySessionStorage>();

        services.AddSingleton(provider => provider.GetRequiredService<ISessionStorageProvider>().Resolve());

        services.AddScoped<SessionManager>();
        services.AddScoped<ISessionManager>(provider => provider.GetRequiredService<SessionManager>());

        services.AddScoped(typeof(IGlobalSession<>), typeof(GlobalSession<>));
        services.AddScoped(typeof(IAdapterSession<>), typeof(AdapterSession<>));
        services.AddScoped(typeof(IChannelSession<>), typeof(ChannelSession<>));
        services.AddScoped(typeof(IAccountSession<>), typeof(AccountSession<>));

        services.ConfigureStorage<ISessionStorage, MemorySessionStorage>(MemorySessionStorage.Name);

        var viewsOptions = _configuration.GetSection("Replikit:Sessions").Get<SessionsOptions>();
        services.ConfigureSelectedStorage<ISessionStorage>(viewsOptions?.Storage);
    }
}
