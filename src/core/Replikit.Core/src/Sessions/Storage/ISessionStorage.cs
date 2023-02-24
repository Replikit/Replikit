using Replikit.Core.Serialization.Dynamic;

namespace Replikit.Core.Sessions.Storage;

/// <summary>
/// The persistent storage for sessions.
/// </summary>
public interface ISessionStorage
{
    /// <summary>
    /// Gets the session data - a dictionary of <see cref="DynamicValue"/>s.
    /// This data can be used to populate the session.
    /// </summary>
    /// <param name="sessionId">The session id. </param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The session data or null if the session does not exist.</returns>
    Task<IReadOnlyDictionary<string, DynamicValue>?> GetSessionDataAsync(string sessionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the session data.
    /// </summary>
    /// <param name="sessionId">The session id.</param>
    /// <param name="data">The session data.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The task that represents the operation.</returns>
    Task SetSessionDataAsync(string sessionId, IReadOnlyDictionary<string, DynamicValue> data,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears the session data.
    /// </summary>
    /// <param name="sessionId">The session id.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>The task that represents the operation.</returns>
    Task ClearSessionDataAsync(string sessionId, CancellationToken cancellationToken = default);
}
