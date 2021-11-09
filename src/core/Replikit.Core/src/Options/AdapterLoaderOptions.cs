using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Loader;

namespace Replikit.Core.Options;

public class AdapterLoaderOptions : IAdapterLoaderOptions
{
    public Dictionary<string, IAdapterFactory> AdapterFactories { get; } = new();

    public List<AdapterDescriptor> AdapterDescriptors { get; } = new();
}
