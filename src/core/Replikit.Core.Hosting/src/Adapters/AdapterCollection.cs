using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Hosting.Adapters;

internal class AdapterCollection : IAdapterCollection
{
    private ImmutableList<IAdapter> _adapters = ImmutableList<IAdapter>.Empty;

    public IReadOnlyList<IAdapter> GetAdapters() => _adapters;

    public void Add(IAdapter adapter)
    {
        _adapters = _adapters.Add(adapter);
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        var eventSources = _adapters
            .Select(x => x.AdapterServices.GetService<IAdapterEventSource>())
            .Where(x => x is not null);

        return Task.WhenAll(eventSources.Select(x => x!.StartListeningAsync(cancellationToken)));
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        var eventSources = _adapters
            .Select(x => x.AdapterServices.GetService<IAdapterEventSource>())
            .Where(x => x is not null);

        return Task.WhenAll(eventSources.Select(x => x!.StopListeningAsync(cancellationToken)));
    }
}
