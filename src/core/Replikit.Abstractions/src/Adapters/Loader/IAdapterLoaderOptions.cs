namespace Replikit.Abstractions.Adapters.Loader;

public interface IAdapterLoaderOptions
{
    Dictionary<string, IAdapterFactory> AdapterFactories { get; }
    List<AdapterDescriptor> AdapterDescriptors { get; }
}
