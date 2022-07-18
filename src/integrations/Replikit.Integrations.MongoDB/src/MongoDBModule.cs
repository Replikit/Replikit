using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Integrations.MongoDB.Internal;
using Replikit.Integrations.MongoDB.Serialization;

namespace Replikit.Integrations.MongoDB;

// ReSharper disable once InconsistentNaming
[Module(Flags = ModuleFlags.Library)]
public class MongoDBModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        SerializationConfigurationInitializer.Initialize();

        services.AddSingleton<RootDbContext>();
    }
}
