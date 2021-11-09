using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Internal;

internal class GlobalSession<TValue> : Session<TValue>, IGlobalSession<TValue> where TValue : class, new()
{
    public GlobalSession(SessionManager sessionManager) : base(sessionManager) { }

    protected override SessionKey CreateSessionKey()
    {
        return new SessionKey(ValueType, SessionType.Global);
    }
}
