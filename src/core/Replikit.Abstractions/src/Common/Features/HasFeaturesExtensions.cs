namespace Replikit.Abstractions.Common.Features;

/// <summary>
/// Extensions for <see cref="IHasFeatures{TFeatures}"/>.
/// </summary>
public static class HasFeaturesExtensions
{
    /// <summary>
    /// Checks that the service supports specified feature(s).
    /// </summary>
    /// <param name="hasFeatures">The service which features are checked.</param>
    /// <param name="features">The features to check. Can be a single enum item or a union of enum items.</param>
    /// <returns></returns>
    public static bool Supports<TFeatures>(this IHasFeatures<TFeatures> hasFeatures, TFeatures features)
        where TFeatures : Enum
    {
        ArgumentNullException.ThrowIfNull(hasFeatures);

        return hasFeatures.Features.HasFlag(features);
    }
}
