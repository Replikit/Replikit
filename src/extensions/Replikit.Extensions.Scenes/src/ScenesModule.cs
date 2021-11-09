using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Scenes;
using Replikit.Extensions.Scenes.Internal;
using Replikit.Extensions.Scenes.Options;

namespace Replikit.Extensions.Scenes;

[ModuleFlags(ModuleFlags.Library)]
public class ScenesModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ScenesModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MemorySceneStorage>();
        services.AddSingleton<SceneRequestHandlerAccessor>();
        services.AddSingleton<ISceneStorageProvider, SceneStorageProvider>();

        services.AddScoped<SceneRequestContextAccessor>();

        services.AddScoped<SceneManager>();
        services.AddScoped<ISceneManager>(sp => sp.GetRequiredService<SceneManager>());

        services.ConfigureStorage<ISceneStorage, MemorySceneStorage>(MemorySceneStorage.Name, false);

        var viewsOptions = _configuration.GetSection("Replikit:Scenes").Get<ScenesOptions>();
        services.ConfigureSelectedStorage<ISceneStorage>(viewsOptions?.Storage);
    }
}
