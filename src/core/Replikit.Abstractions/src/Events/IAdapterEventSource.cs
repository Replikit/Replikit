namespace Replikit.Abstractions.Events;

/// <summary>
/// The adapter service that is responsible for listening and dispatching events.
/// </summary>
public interface IAdapterEventSource
{
    /// <summary>
    /// Starts the listening for events.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that
    /// can be used to cancel the operation.</param>
    /// <returns>A Task indicating the completion of the operation.</returns>
    Task StartListeningAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the listening for events.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that
    /// can be used to indicate that stop operation should complete immediately.</param>
    /// <returns>A Task indicating the completion of the operation.</returns>
    Task StopListeningAsync(CancellationToken cancellationToken = default);
}
