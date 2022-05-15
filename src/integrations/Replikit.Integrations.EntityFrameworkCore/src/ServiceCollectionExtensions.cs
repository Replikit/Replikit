using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Integrations.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static EntityFrameworkCoreModuleBuilder AddEntityFrameworkCoreModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddModule<EntityFrameworkCoreModule>();

        return new EntityFrameworkCoreModuleBuilder(services);
    }
}
