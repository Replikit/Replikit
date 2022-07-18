using Replikit.Abstractions.Adapters;

namespace Replikit.Abstractions.Events;

public interface IAdapterEventHandler
{
    Task HandleAsync(IAdapterEvent @event, IAdapter adapter, CancellationToken cancellationToken = default);
}
