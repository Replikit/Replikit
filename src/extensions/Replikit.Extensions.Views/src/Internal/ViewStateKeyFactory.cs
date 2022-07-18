using Replikit.Core.Abstractions.State;
using Replikit.Extensions.State.Exceptions;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Views.Internal;

internal class ViewStateKeyFactory : IStateKeyFactory
{
    public StateKey? CreateStateKey(StateType stateType, object context)
    {
        var viewContext = (ViewContext) context;

        if (stateType is StateType.GlobalState)
        {
            return new StateKey(StateType.GlobalState);
        }

        if (viewContext.MessageId is null)
        {
            return null;
        }

        return stateType switch
        {
            StateType.State => StateKey.FromMessageId(StateType.State, viewContext.MessageId.Value),
            StateType.ChannelState => StateKey.FromChannelId(
                StateType.ChannelState,
                viewContext.MessageId.Value.ChannelId),
            StateType.AccountState => throw new InvalidStateTypeException(stateType, context),
            _ => throw new ArgumentOutOfRangeException(nameof(stateType))
        };
    }

    public static ViewStateKeyFactory Instance { get; } = new();
}
