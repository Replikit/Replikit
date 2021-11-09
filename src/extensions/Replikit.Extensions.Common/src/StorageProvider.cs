using Microsoft.Extensions.Options;
using Replikit.Extensions.Common.Options;

namespace Replikit.Extensions.Common;

public abstract class StorageProvider<TStorage> : IStorageProvider<TStorage>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly StorageProviderOptions<TStorage> _options;

    protected StorageProvider(IServiceProvider serviceProvider, IOptions<StorageProviderOptions<TStorage>> options)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public virtual TStorage Resolve(string? type = null)
    {
        var resolvedTypeName = type ?? _options.DefaultType;

        if (resolvedTypeName is null)
        {
            throw new InvalidOperationException("Default storage implementation was not configured");
        }

        var resolvedType = _options.StorageTypes[resolvedTypeName];
        return (TStorage) _serviceProvider.GetService(resolvedType)!;
    }
}
