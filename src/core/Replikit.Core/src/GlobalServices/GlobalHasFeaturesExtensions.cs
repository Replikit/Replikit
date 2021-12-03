using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

public static class GlobalHasFeaturesExtensions
{
    public static bool Supports<TFeatures>(this IGlobalHasFeatures<TFeatures> hasFeatures, AdapterIdentifier adapterId,
        TFeatures features)
        where TFeatures : Enum
    {
        return hasFeatures.GetFeatures(adapterId).HasFlag(features);
    }

    public static bool Supports<TFeatures>(this IGlobalHasFeatures<TFeatures> hasFeatures, GlobalIdentifier globalId,
        TFeatures features) where TFeatures : Enum
    {
        return hasFeatures.GetFeatures(globalId.AdapterId).HasFlag(features);
    }

    public static TFeatures GetFeatures<TFeatures>(this IGlobalHasFeatures<TFeatures> hasFeatures,
        GlobalIdentifier globalId) where TFeatures : Enum
    {
        return hasFeatures.GetFeatures(globalId.AdapterId);
    }
}
