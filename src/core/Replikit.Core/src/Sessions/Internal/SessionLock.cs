namespace Replikit.Core.Sessions.Internal;

internal class SessionLock : ISessionLock
{
    private readonly Func<ValueTask> _unlockAction;

    public SessionLock(ISession session, Func<ValueTask> unlockAction)
    {
        Session = session;
        _unlockAction = unlockAction;
    }

    public ISession Session { get; }

    public ValueTask DisposeAsync() => _unlockAction();
}
