using Replikit.Abstractions.Adapters.Services;

namespace Replikit.Adapters.Common.Adapters;

internal class AdapterServiceProvider : IAdapterServiceProvider
{
    private readonly Dictionary<Type, object?> _services = new();

    public void SetService<TService>(TService instance)
    {
        _services[typeof(TService)] = instance;
    }

    public object? GetService(Type serviceType)
    {
        return _services.GetValueOrDefault(serviceType);
    }
}
