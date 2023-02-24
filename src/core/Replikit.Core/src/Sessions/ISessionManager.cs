namespace Replikit.Core.Sessions;

/// <summary>
/// This service allows to access sessions in safe way.
/// </summary>
public interface ISessionManager
{
    /// <summary>
    /// Locks session by the specified key and returns it's lock object.
    /// <br/>
    /// The session will be persisted automatically after the lock is released.
    /// No other thread can lock the same session until the lock is released.
    /// <br/>
    /// When the session becomes empty, it will be removed automatically.
    /// <br/>
    /// Note: do not acquire session lock inside another session lock. It will always lead to deadlock.
    /// The same goes for acquiring the same session inside an event handler which implicitly locks the session.
    /// </summary>
    /// <param name="key">The key of the session.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the lock object which can be used to access the session.
    /// </returns>
    Task<ISessionLock> AcquireSessionAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Merges the specified session with the session with the same key.
    /// <br/>
    /// This method allows should not be used in most cases.
    /// The most common use case is when you don't know the key of the session when you create it
    /// and you need to merge it with the session with the that you discovered later.
    /// <br/>
    /// The session will be locked before merging and persisted automatically after the existing lock is released.
    /// </summary>
    /// <param name="key">The key of the session.</param>
    /// <param name="session">The session to merge.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ApplySessionAsync(string key, Session session, CancellationToken cancellationToken = default);
}
