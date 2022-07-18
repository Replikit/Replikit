using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Integrations.MongoDB;

namespace Replikit.Examples.MongoDB;

// ReSharper disable once InconsistentNaming
public class MongoDBExampleModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public MongoDBExampleModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<MainModule>();

        var connectionString = _configuration.GetConnectionString("Default");

        services.AddMongoDBModule()
            .AddDatabase(connectionString)
            .AddDefaults();
    }
}
