using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Exceptions;

namespace Replikit.Extensions.Sessions.Internal;

internal class AdapterSession<TValue> : Session<TValue>, IAdapterSession<TValue> where TValue : class, new()
{
    private readonly IContextAccessor<IEventContext<IEvent>> _eventContextAccessor;

    public AdapterSession(SessionManager sessionManager, IContextAccessor<IEventContext<IEvent>> eventContextAccessor) :
        base(sessionManager)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    protected override SessionKey CreateSessionKey()
    {
        if (_eventContextAccessor.Context?.Event is not { } adapterEvent)
        {
            throw new InvalidSessionTypeException(SessionType.Adapter, _eventContextAccessor.Context?.Event);
        }

        return new SessionKey(ValueType, SessionType.Adapter, adapterEvent.AdapterId);
    }
}
