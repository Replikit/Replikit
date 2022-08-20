using Replikit.Abstractions.Adapters;

namespace Replikit.Abstractions.Events;

/// <summary>
/// Defines a mechanism for planning and executing events handlers.
/// </summary>
public interface IAdapterEventDispatcher
{
    /// <summary>
    /// Adds an event to the processing queue.
    /// </summary>
    /// <param name="event">An event to dispatch.</param>
    /// <param name="adapter">An <see cref="IAdapter"/> that dispatched the event.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel event processing.</param>
    /// <returns>A task that will complete when the event has been processed.</returns>
    Task DispatchAsync(IBotEvent @event, IAdapter adapter, CancellationToken cancellationToken = default);
}
