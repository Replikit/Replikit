using Replikit.Core.Abstractions.State;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Exceptions;

namespace Replikit.Extensions.Views.Internal;

internal class ViewStateKeyFactory : IStateKeyFactory
{
    private readonly InternalViewContext _viewContext;

    public ViewStateKeyFactory(InternalViewContext viewContext)
    {
        _viewContext = viewContext;
    }

    public StateKey? CreateStateKey(StateKind stateKind, Type type)
    {
        if (stateKind is StateKind.GlobalState)
        {
            return new StateKey(StateKind.GlobalState);
        }

        if (_viewContext.ViewMessageId is not { } messageId)
        {
            return null;
        }

        return stateKind switch
        {
            StateKind.State => StateKey.FromMessageId(StateKind.State, messageId),
            StateKind.ChannelState => StateKey.FromChannelId(StateKind.State, messageId.ChannelId),
            StateKind.AccountState => throw new InvalidViewStateTypeException(stateKind),
            _ => throw new ArgumentOutOfRangeException(nameof(stateKind))
        };
    }
}
