using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;

namespace Replikit.Extensions.Storage;

[Module(Flags = ModuleFlags.Library)]
public class StorageModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IStorage<,>), typeof(MemoryStorage<,>));
    }
}
