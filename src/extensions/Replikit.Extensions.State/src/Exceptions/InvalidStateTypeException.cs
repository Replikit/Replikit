using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Events;
using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.State.Exceptions;

public class InvalidStateTypeException : ReplikitException
{
    public InvalidStateTypeException(StateType stateType, IContext context) :
        base(CreateMessage(stateType, context)) { }

    private static string CreateMessage(StateType stateType, IContext context)
    {
        var @event = context is IEventContext<IEvent> eventContext ? eventContext.Event : null;

        return @event is not null
            ? $"{stateType} cannot be used in context of {@event.GetType().Name} event"
            : $"{stateType} cannot be used out of the event context";
    }
}
