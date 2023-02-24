namespace Replikit.Core.Sessions;

/// <summary>
/// Represents a locked session.
/// <br/>
/// The session data cannot be changed by any other code until the lock is released.
/// The session will be automatically persisted when the lock is released.
/// </summary>
public interface ISessionLock : IAsyncDisposable
{
    /// <summary>
    /// The session that is locked.
    /// </summary>
    public ISession Session { get; }
}
