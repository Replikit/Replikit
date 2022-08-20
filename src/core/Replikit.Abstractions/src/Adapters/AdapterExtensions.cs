using System.Diagnostics.CodeAnalysis;
using Replikit.Abstractions.Adapters.Exceptions;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// The extensions for <see cref="IAdapter"/>.
/// </summary>
public static class AdapterExtensions
{
    /// <summary>
    /// Checks if the adapter supports the service of the specified type.
    /// </summary>
    /// <param name="adapter">An adapter to check.</param>
    /// <typeparam name="TService">The type of the service to check.</typeparam>
    /// <returns>True if the adapter supports the service of the specified type, otherwise false.</returns>
    public static bool Supports<TService>(this IAdapter adapter) where TService : notnull
    {
        Check.NotNull(adapter);

        return adapter.AdapterServices.GetService(typeof(TService)) is not null;
    }

    /// <summary>
    /// Tries to get the service of the specified type from the adapter.
    /// </summary>
    /// <param name="adapter">An adapter to get the service from.</param>
    /// <param name="service">A variable to store the service in.</param>
    /// <typeparam name="TService">The type of the service to get.</typeparam>
    /// <returns>True if the service is implemented by the adapter, otherwise false.</returns>
    public static bool TryGetService<TService>(this IAdapter adapter, [NotNullWhen(true)] out TService? service)
        where TService : notnull
    {
        Check.NotNull(adapter);

        service = (TService?) adapter.AdapterServices.GetService(typeof(TService));
        return service is not null;
    }

    /// <summary>
    /// Gets the service of the specified type from the adapter.
    /// </summary>
    /// <param name="adapter">An adapter to get the service from.</param>
    /// <typeparam name="TService">The type of the service to get.</typeparam>
    /// <returns>A service of the specified type.</returns>
    /// <exception cref="AdapterServiceNotImplementedException">
    /// The adapter does not support the service of the specified type.
    /// </exception>
    public static TService GetRequiredService<TService>(this IAdapter adapter)
    {
        ArgumentNullException.ThrowIfNull(adapter);

        return (TService?) adapter.AdapterServices.GetService(typeof(TService)) ??
               throw new AdapterServiceNotImplementedException(adapter, typeof(TService));
    }
}
