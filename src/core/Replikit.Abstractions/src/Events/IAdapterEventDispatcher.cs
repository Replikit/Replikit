using Replikit.Abstractions.Adapters;

namespace Replikit.Abstractions.Events;

/// <summary>
/// Defines a mechanism for planning and executing adapter events.
/// </summary>
public interface IAdapterEventDispatcher
{
    /// <summary>
    /// Adds an event to the processing queue.
    /// Returns a task that will complete when the event has been processed.
    /// </summary>
    /// <param name="event"></param>
    /// <param name="adapter"></param>
    /// <param name="cancellationToken"></param>
    Task DispatchAsync(IAdapterEvent @event, IAdapter adapter, CancellationToken cancellationToken = default);
}
