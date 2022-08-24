using Kantaiko.Modularity;
using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Integrations.MongoDB.Internal;
using Replikit.Integrations.MongoDB.Serialization;

namespace Replikit.Integrations.MongoDB;

// ReSharper disable once InconsistentNaming
[Module(Flags = ModuleFlags.Library)]
public class ReplikitMongoDBModule : ReplikitModule
{
    protected override bool ConfigureDefaults => false;

    protected override void ConfigureServices(IServiceCollection services)
    {
        SerializationConfigurationInitializer.Initialize();

        services.AddSingleton<RootDbContext>();
    }
}
