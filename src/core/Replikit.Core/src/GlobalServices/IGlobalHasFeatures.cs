using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

public interface IGlobalHasFeatures<out TFeatures> where TFeatures : Enum
{
    /// <summary>
    /// Get the features supported by the service of the adapter with the specified identifier.
    /// </summary>
    TFeatures GetFeatures(BotIdentifier botId);
}
