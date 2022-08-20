using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Common.CustomData;

/// <summary>
/// Extensions for <see cref="IHasCustomData"/>.
/// </summary>
public static class HasCustomDataExtensions
{
    /// <summary>
    /// Gets the custom data object of the specified type.
    /// </summary>
    /// <param name="hasCustomData">The <see cref="IHasCustomData"/> object to get data from.</param>
    /// <typeparam name="TData">The type of the data object.</typeparam>
    /// <returns>The <see cref="TData"/> instance.</returns>
    /// <exception cref="CustomDataNotFoundException">The data of the specified type is not associated with this object.</exception>
    public static TData GetCustomData<TData>(this IHasCustomData hasCustomData) where TData : notnull
    {
        ArgumentNullException.ThrowIfNull(hasCustomData);

        if (hasCustomData.CustomData.OfType<TData>().FirstOrDefault() is { } original)
        {
            return original;
        }

        throw new CustomDataNotFoundException(typeof(TData));
    }

    /// <summary>
    /// Gets the custom data object of the specified type or default value.
    /// </summary>
    /// <param name="hasCustomData">The <see cref="IHasCustomData"/> object to get data from.</param>
    /// <typeparam name="TData">The type of the data object.</typeparam>
    /// <returns>
    /// The <see cref="TData"/> instance or default if no data of the specified type is associated with this object.
    /// </returns>
    public static TData? GetCustomDataOrDefault<TData>(this IHasCustomData hasCustomData) where TData : notnull
    {
        ArgumentNullException.ThrowIfNull(hasCustomData);

        return hasCustomData.CustomData.OfType<TData>().FirstOrDefault();
    }
}
