using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Exceptions;

namespace Replikit.Extensions.Sessions.Internal;

internal class ChannelSession<TValue> : Session<TValue>, IChannelSession<TValue> where TValue : class, new()
{
    private readonly IEventContextAccessor _eventContextAccessor;

    public ChannelSession(SessionManager sessionManager, IEventContextAccessor eventContextAccessor) : base(
        sessionManager)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    protected override SessionKey CreateSessionKey()
    {
        if (_eventContextAccessor.Context?.Event is not IChannelEvent channelEvent)
        {
            throw new InvalidSessionTypeException(SessionType.Channel, _eventContextAccessor.Context?.Event);
        }

        return new SessionKey(ValueType,
            SessionType.Channel,
            channelEvent.AdapterId,
            channelEvent.Channel.Id);
    }
}
