using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Adapters.Common.Adapters.Internal;

internal class DefaultEventDispatcher : IAdapterEventDispatcher
{
    public Task DispatchAsync(IAdapterEvent @event, IAdapter adapter, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public static DefaultEventDispatcher Instance { get; } = new();
}
