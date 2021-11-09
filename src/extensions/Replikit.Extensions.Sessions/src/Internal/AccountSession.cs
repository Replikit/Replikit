using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Exceptions;

namespace Replikit.Extensions.Sessions.Internal;

internal class AccountSession<TValue> : Session<TValue>, IAccountSession<TValue> where TValue : class, new()
{
    private readonly IEventContextAccessor _eventContextAccessor;

    public AccountSession(SessionManager sessionManager, IEventContextAccessor eventContextAccessor) : base(
        sessionManager)
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
