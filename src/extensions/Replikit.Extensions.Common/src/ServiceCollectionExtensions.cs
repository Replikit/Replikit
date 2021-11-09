using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.Common.Options;

namespace Replikit.Extensions.Common;

public static class ServiceCollectionExtensions
{
    public static void ConfigureStorage<TStorage, TImplementation>(this IServiceCollection services, string name,
        bool overrideDefaultType = true)
        where TImplementation : class, TStorage
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(name);

        services.AddSingleton<TImplementation>();

        services.Configure<StorageProviderOptions<TStorage>>(options =>
        {
            if (options.DefaultType is null || overrideDefaultType)
            {
                options.DefaultType = name;
            }

            options.StorageTypes[name] = typeof(TImplementation);
        });
    }

    public static void ConfigureSelectedStorage<TStorage>(this IServiceCollection services, string? name)
    {
        ArgumentNullException.ThrowIfNull(services);

        if (name is null) return;

        services.PostConfigure<StorageProviderOptions<TStorage>>(options =>
        {
            if (!options.StorageTypes.ContainsKey(name))
            {
                var expectedString = string.Join(", ", options.StorageTypes.Keys);
                throw new ValidationException($"Invalid storage type: Expected: {expectedString}, got: {name}");
            }

            options.DefaultType = name;
        });
    }
}
