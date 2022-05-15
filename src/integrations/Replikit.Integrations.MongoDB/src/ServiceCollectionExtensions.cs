using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Integrations.MongoDB;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static MongoDBModuleBuilder AddMongoDBModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddModule<MongoDBModule>();

        return new MongoDBModuleBuilder(services);
    }
}
