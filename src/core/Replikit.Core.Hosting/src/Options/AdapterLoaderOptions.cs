using Replikit.Abstractions.Adapters.Factory;
using Replikit.Abstractions.Adapters.Loader;

namespace Replikit.Core.Hosting.Options;

public class AdapterLoaderOptions : IAdapterLoaderOptions
{
    public Dictionary<string, IAdapterFactory> AdapterFactories { get; } = new();

    public List<AdapterDescriptor> AdapterDescriptors { get; } = new();

    public void RegisterFactory(string adapterType, IAdapterFactory factory)
    {
        AdapterFactories[adapterType] = factory;
    }

    public void AddDescriptor(AdapterDescriptor descriptor)
    {
        AdapterDescriptors.Add(descriptor);
    }
}
