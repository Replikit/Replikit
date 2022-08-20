namespace Replikit.Abstractions.Adapters;

/// <summary>
/// The service that can be used to access adapters.
/// </summary>
public interface IAdapterCollection
{
    /// <summary>
    /// Returns the list of all registered adapters in the collection at the moment of the call.
    /// <br/>
    /// The returned list is immutable and thread-safe, but next calls of this method may return a different list.
    /// </summary>
    /// <returns>An immutable list of all registered adapters.</returns>
    IReadOnlyList<IAdapter> GetAdapters();
}
