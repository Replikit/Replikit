using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Storage;
using Replikit.Extensions.Storage.Models;

namespace Replikit.Extensions.State;

[Module(Flags = ModuleFlags.Library)]
public class StateModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<StorageModule>();

        services.AddStorage<StateKey, DynamicValue>("States");

        services.AddScoped(typeof(IState<>), typeof(State<>));
        services.AddScoped(typeof(IGlobalState<>), typeof(GlobalState<>));
        services.AddScoped(typeof(IAccountState<>), typeof(AccountState<>));
        services.AddScoped(typeof(IChannelState<>), typeof(ChannelState<>));

        services.AddScoped<StateManager>();

        services.AddTransient<IStateManager>(sp => sp.GetRequiredService<StateManager>());
        services.AddTransient<IStateLoader>(sp => sp.GetRequiredService<StateManager>());
        services.AddTransient<IStateTracker>(sp => sp.GetRequiredService<StateManager>());
    }
}
