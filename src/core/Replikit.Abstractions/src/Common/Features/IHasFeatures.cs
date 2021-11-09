namespace Replikit.Abstractions.Common.Features;

public interface IHasFeatures<out TFeatures> where TFeatures : Enum
{
    /// <summary>
    /// Features, that this class supports.
    /// </summary>
    TFeatures Features { get; }
}
