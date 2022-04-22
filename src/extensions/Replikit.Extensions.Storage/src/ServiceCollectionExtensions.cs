using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Extensions.Storage;

public static class ServiceCollectionExtensions
{
    public static void AddStorage<TKey, TValue>(this IServiceCollection services, string name)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure<StorageOptions>(options =>
        {
            options.RegisterStorageDefinition(name, typeof(TKey), typeof(TValue));
        });
    }
}
