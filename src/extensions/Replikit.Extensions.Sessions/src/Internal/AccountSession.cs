using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Exceptions;

namespace Replikit.Extensions.Sessions.Internal;

internal class AccountSession<TValue> : Session<TValue>, IAccountSession<TValue> where TValue : class, new()
{
    private readonly IContextAccessor<IEventContext<IEvent>> _eventContextAccessor;

    public AccountSession(SessionManager sessionManager, IContextAccessor<IEventContext<IEvent>> eventContextAccessor) :
        base(sessionManager)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    protected override SessionKey CreateSessionKey()
    {
        if (_eventContextAccessor.Context?.Event is not IAccountEvent accountEvent)
        {
            throw new InvalidSessionTypeException(SessionType.Account, _eventContextAccessor.Context?.Event);
        }

        return new SessionKey(ValueType,
            SessionType.Account,
            accountEvent.AdapterId,
            accountId: accountEvent.Account.Id);
    }
}
