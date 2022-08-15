using Kantaiko.Modularity;
using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Core.Modularity;
using Replikit.Core.Routing;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State;

[Module(Flags = ModuleFlags.Library)]
public class ReplikitStateModule : ReplikitModule
{
    protected override bool ConfigureDefaults => false;

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddEntityUsageIndicator<StateItem>();

        services.AddScoped(typeof(IState<>), typeof(State<>));
        services.AddScoped(typeof(IGlobalState<>), typeof(GlobalState<>));
        services.AddScoped(typeof(IAccountState<>), typeof(AccountState<>));
        services.AddScoped(typeof(IChannelState<>), typeof(ChannelState<>));

        services.AddScoped<StateManager>();

        services.AddTransient<IStateManager>(sp => sp.GetRequiredService<StateManager>());
        services.AddTransient<IStateLoader>(sp => sp.GetRequiredService<StateManager>());
        services.AddTransient<IStateTracker>(sp => sp.GetRequiredService<StateManager>());
        services.AddTransient<IStateKeyFactoryAcceptor>(sp => sp.GetRequiredService<StateManager>());

        services.AddScoped<IHandlerInstanceInterceptor, LoadStateHandlerInstanceInterceptor>();

        services.TryAddSingleton<IStateStore, MemoryStateStore>();
    }

    protected override void PostConfigure(IApplicationBuilder app)
    {
        app.UseMiddleware(new SaveStateMiddleware());
    }
}
