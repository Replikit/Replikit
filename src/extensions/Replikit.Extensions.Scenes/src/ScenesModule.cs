using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Scenes;
using Replikit.Extensions.Scenes.Internal;
using Replikit.Extensions.Scenes.Options;

namespace Replikit.Extensions.Scenes;

[Module(Flags = ModuleFlags.Library)]
public class ScenesModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ScenesModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MemorySceneStorage>();
        services.AddSingleton<SceneHandlerAccessor>();
        services.AddSingleton<ISceneStorageProvider, SceneStorageProvider>();

        services.AddSingleton<ISceneIntrospectionInfoAccessor>(sp => sp.GetRequiredService<SceneHandlerAccessor>());

        services.AddScoped<SceneManager>();
        services.AddScoped<ISceneManager>(sp => sp.GetRequiredService<SceneManager>());

        services.ConfigureStorage<ISceneStorage, MemorySceneStorage>(MemorySceneStorage.Name, false);

        var viewsOptions = _configuration.GetSection("Replikit:Scenes").Get<ScenesOptions>();
        services.ConfigureSelectedStorage<ISceneStorage>(viewsOptions?.Storage);
    }
}
