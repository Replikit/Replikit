using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Scenes.Internal;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Scenes;

[Module(Flags = ModuleFlags.Library)]
public class ScenesModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<StateModule>();

        services.AddSingleton<SceneHandlerAccessor>();
        services.AddSingleton<ISceneIntrospectionInfoAccessor>(sp => sp.GetRequiredService<SceneHandlerAccessor>());

        services.AddScoped<SceneManager>();
        services.AddScoped<ISceneManager>(sp => sp.GetRequiredService<SceneManager>());
    }
}
