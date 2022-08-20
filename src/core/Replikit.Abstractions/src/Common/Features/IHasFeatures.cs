namespace Replikit.Abstractions.Common.Features;

/// <summary>
/// Represents service that can be implemented partially and support one or many features.
/// </summary>
/// <typeparam name="TFeatures">The type of the enum of features.</typeparam>
public interface IHasFeatures<out TFeatures> where TFeatures : Enum
{
    /// <summary>
    /// Features, that this service supports.
    /// </summary>
    TFeatures Features { get; }
}
