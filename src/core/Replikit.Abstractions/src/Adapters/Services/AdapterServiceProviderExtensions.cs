using System.Diagnostics.CodeAnalysis;
using Replikit.Abstractions.Adapters.Exceptions;

namespace Replikit.Abstractions.Adapters.Services;

public static class AdapterServiceProviderExtensions
{
    public static bool Supports<TService>(this IAdapterServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        return serviceProvider.GetService(typeof(TService)) is not null;
    }

    public static bool TryGetService<TService>(this IAdapterServiceProvider serviceProvider,
        [NotNullWhen(true)] out TService? feature)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        feature = (TService?) serviceProvider.GetService(typeof(TService));
        return feature is not null;
    }

    public static object GetRequiredService(this IAdapterServiceProvider serviceProvider, Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(serviceType);

        return serviceProvider.GetService(serviceType) ??
               throw new AdapterServiceNotImplementedException(serviceProvider, serviceType);
    }

    public static TService GetRequiredService<TService>(this IAdapterServiceProvider serviceProvider)
    {
        return (TService) GetRequiredService(serviceProvider, typeof(TService));
    }
}
