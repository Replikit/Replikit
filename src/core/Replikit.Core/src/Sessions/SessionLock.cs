using Replikit.Core.Sessions.Internal;

namespace Replikit.Core.Sessions;

/// <summary>
/// Represents a locked session.
/// <br/>
/// The session data cannot be changed by any other code until the lock is released.
/// The session will be automatically persisted when the lock is released.
/// </summary>
public struct SessionLock : IAsyncDisposable
{
    private readonly string _key;
    private Session? _session;
    private readonly SessionManager _sessionManager;
    private readonly IDisposable _locker;
    private readonly CancellationToken _cancellationToken;

    internal SessionLock(string key, Session session, SessionManager sessionManager, IDisposable locker,
        CancellationToken cancellationToken)
    {
        _key = key;
        _session = session;
        _sessionManager = sessionManager;
        _locker = locker;
        _cancellationToken = cancellationToken;
    }

    /// <summary>
    /// The session that is locked.
    /// </summary>
    public ISession Session => _session ?? throw new InvalidOperationException("Session lock is disposed.");

    public async ValueTask DisposeAsync()
    {
        if (_session is null)
        {
            throw new InvalidOperationException("Session lock is already disposed.");
        }

        await _sessionManager.UpdateSessionAsync(_key, _session, _cancellationToken);
        _locker.Dispose();
        _session = null;
    }
}
