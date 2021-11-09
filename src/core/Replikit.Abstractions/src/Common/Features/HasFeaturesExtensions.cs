namespace Replikit.Abstractions.Common.Features;

public static class HasFeaturesExtensions
{
    /// <summary>
    /// Checks that this class supports specified feature(s).
    /// </summary>
    /// <param name="hasFeatures"></param>
    /// <param name="features"></param>
    /// <returns></returns>
    public static bool Supports<TFeatures>(this IHasFeatures<TFeatures> hasFeatures, TFeatures features)
        where TFeatures : Enum
    {
        return hasFeatures.Features.HasFlag(features);
    }
}
