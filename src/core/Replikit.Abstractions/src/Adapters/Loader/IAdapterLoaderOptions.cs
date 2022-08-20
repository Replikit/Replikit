using Replikit.Abstractions.Adapters.Factory;

namespace Replikit.Abstractions.Adapters.Loader;

/// <summary>
/// Represents the adapter loading configuration.
/// </summary>
public interface IAdapterLoaderOptions
{
    /// <summary>
    /// Registers the factory which will be used to create the adapter.
    /// <br/>
    /// If the factory for the specified type is already registered, it will be overwritten.
    /// </summary>
    /// <param name="adapterType">A type of the adapter.</param>
    /// <param name="factory">A factory which will be used to create the adapter.</param>
    void RegisterFactory(string adapterType, IAdapterFactory factory);

    /// <summary>
    /// Adds a new adapter descriptor.
    /// </summary>
    /// <param name="descriptor">An adapter descriptor.</param>
    void AddDescriptor(AdapterDescriptor descriptor);
}
