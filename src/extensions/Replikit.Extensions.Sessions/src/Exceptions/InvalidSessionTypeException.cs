using Replikit.Abstractions.Events;
using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Exceptions;

public class InvalidSessionTypeException : SessionsException
{
    public InvalidSessionTypeException(SessionType sessionType, IEvent? @event) :
        base(CreateMessage(sessionType, @event)) { }

    private static string CreateMessage(SessionType sessionType, IEvent? @event)
    {
        return @event is not null
            ? $"Session with type {sessionType} cannot be used in context of {@event.GetType().Name} event"
            : $"Session with type {sessionType} cannot be used out of the event context";
    }
}
