using Kantaiko.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Integrations.MongoDB;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static void AddReplikitMongoDBIntegration(this IServiceCollection services,
        Action<ReplikitMongoDBModuleBuilder>? configureDelegate)
    {
        services.AddModule<ReplikitMongoDBModule>();

        configureDelegate?.Invoke(new ReplikitMongoDBModuleBuilder(services));
    }
}
