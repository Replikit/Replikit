using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Hosting.Adapters;

internal class AdapterCollection : IAdapterCollection
{
    private readonly List<IAdapter> _adapters = new();

    public void Add(IAdapter adapter)
    {
        _adapters.Add(adapter);
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        var eventSources = _adapters
            .Where(x => x.Supports<IEventSource>())
            .Select(x => x.EventSource);

        return Task.WhenAll(eventSources.Select(x => x.StartAsync(cancellationToken)));
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        var eventSources = _adapters
            .Where(x => x.Supports<IEventSource>())
            .Select(x => x.EventSource);

        return Task.WhenAll(eventSources.Select(x => x.StartAsync(cancellationToken)));
    }

    public IAdapter? Resolve(string type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return _adapters.FirstOrDefault(x => x.Id.Type == type);
    }

    public IAdapter? Resolve(AdapterIdentifier identifier)
    {
        ArgumentNullException.ThrowIfNull(identifier);

        return _adapters.FirstOrDefault(x => x.Id == identifier);
    }
}
