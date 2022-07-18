using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Adapters.Common.Adapters.Internal;

internal class DefaultEventHandler : IAdapterEventHandler
{
    public Task HandleAsync(IAdapterEvent @event, IAdapter adapter, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public static DefaultEventHandler Instance { get; } = new();
}
