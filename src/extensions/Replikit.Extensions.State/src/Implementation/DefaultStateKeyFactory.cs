using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Extensions.State.Exceptions;

namespace Replikit.Extensions.State.Implementation;

internal class DefaultStateKeyFactory : IStateKeyFactory
{
    public StateKey? CreateStateKey(StateType stateType, IContext context)
    {
        return stateType switch
        {
            StateType.GlobalState => new StateKey(),
            StateType.ChannelState => CreateChannelStateKey(context),
            StateType.AccountState => CreateAccountStateKey(context),
            StateType.State => throw new InvalidStateTypeException(StateType.State, context),
            _ => throw new ArgumentOutOfRangeException(nameof(stateType))
        };
    }

    private static StateKey CreateChannelStateKey(IContext context)
    {
        if (context is not IEventContext<IChannelEvent> { Event: var channelEvent })
        {
            throw new InvalidStateTypeException(StateType.ChannelState, context);
        }

        return StateKey.FromChannelId(StateType.ChannelState, channelEvent.Channel.Id);
    }

    private static StateKey CreateAccountStateKey(IContext context)
    {
        if (context is not IEventContext<IAccountEvent> { Event: var accountEvent })
        {
            throw new InvalidStateTypeException(StateType.AccountState, context);
        }

        return StateKey.FromAccountId(StateType.AccountState, accountEvent.Account.Id);
    }

    public static DefaultStateKeyFactory Instance { get; } = new();
}
