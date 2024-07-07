using Replikit.Abstractions.Adapters;

namespace Replikit.Abstractions.Events;

/// <summary>
/// Defines a mechanism for planning the execution of the event handlers.
/// </summary>
public interface IAdapterEventDispatcher
{
    /// <summary>
    /// Adds an event to the processing queue.
    /// </summary>
    /// <param name="event">The event to dispatch.</param>
    /// <param name="adapter">The <see cref="IAdapter"/> that dispatched the event.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> that can be used to cancel event processing.</param>
    /// <returns>A task that will complete when the event has been processed.</returns>
    Task DispatchAsync(IBotEvent @event, IAdapter adapter, CancellationToken cancellationToken = default);
}
