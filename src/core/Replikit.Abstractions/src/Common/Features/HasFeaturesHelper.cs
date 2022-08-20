using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Common.Features;

internal static class HasFeaturesHelper
{
    public static Exception CreateUnsupportedException<TFeatures>(IHasFeatures<TFeatures> hasFeatures,
        TFeatures feature) where TFeatures : Enum
    {
        if (hasFeatures.Supports(feature))
        {
            return new UnexpectedlyUnsupportedFeatureException(hasFeatures, feature);
        }

        return new UnsupportedFeatureException(hasFeatures, feature);
    }
}
