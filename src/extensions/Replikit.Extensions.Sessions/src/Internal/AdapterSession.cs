using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Exceptions;

namespace Replikit.Extensions.Sessions.Internal;

internal class AdapterSession<TValue> : Session<TValue>, IAdapterSession<TValue> where TValue : class, new()
{
    private readonly IEventContextAccessor _eventContextAccessor;

    public AdapterSession(SessionManager sessionManager, IEventContextAccessor eventContextAccessor) : base(
        sessionManager)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    protected override SessionKey CreateSessionKey()
    {
        if (_eventContextAccessor.Context?.Event is null)
        {
            throw new InvalidSessionTypeException(SessionType.Adapter, _eventContextAccessor.Context?.Event);
        }

        return new SessionKey(ValueType, SessionType.Adapter, _eventContextAccessor.Context.Event.AdapterId);
    }
}
