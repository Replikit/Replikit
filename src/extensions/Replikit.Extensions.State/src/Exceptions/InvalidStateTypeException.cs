using Replikit.Abstractions.Common.Exceptions;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.State.Exceptions;

public class InvalidStateTypeException : ReplikitException
{
    public InvalidStateTypeException(StateKind stateKind, IBotEventContext? context) :
        base(CreateMessage(stateKind, context)) { }

    private static string CreateMessage(StateKind stateKind, IBotEventContext? context)
    {
        return context is not null
            ? $"{stateKind} cannot be used in context of {context.Event.GetType().Name} event"
            : $"{stateKind} cannot be used out of the event context";
    }
}
