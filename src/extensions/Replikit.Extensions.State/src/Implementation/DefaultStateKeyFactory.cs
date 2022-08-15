using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Routing.Context;
using Replikit.Extensions.State.Exceptions;

namespace Replikit.Extensions.State.Implementation;

public class DefaultStateKeyFactory : IStateKeyFactory
{
    private readonly IAdapterEventContextAccessor _eventContextAccessor;

    public DefaultStateKeyFactory(IAdapterEventContextAccessor eventContextAccessor)
    {
        _eventContextAccessor = eventContextAccessor;
    }

    public StateKey CreateStateKey(StateKind stateKind, Type type)
    {
        return stateKind switch
        {
            StateKind.GlobalState => new StateKey(StateKind.GlobalState),
            StateKind.ChannelState => CreateChannelStateKey(),
            StateKind.AccountState => CreateAccountStateKey(),
            StateKind.State => throw new InvalidStateTypeException(StateKind.State,
                _eventContextAccessor.CurrentContext),
            _ => throw new ArgumentOutOfRangeException(nameof(stateKind))
        };
    }

    private StateKey CreateChannelStateKey()
    {
        if (_eventContextAccessor.CurrentContext is not IAdapterEventContext<IChannelEvent> { Event: var channelEvent })
        {
            throw new InvalidStateTypeException(StateKind.ChannelState, _eventContextAccessor.CurrentContext);
        }

        return StateKey.FromChannelId(StateKind.ChannelState, channelEvent.Channel.Id);
    }

    private StateKey CreateAccountStateKey()
    {
        if (_eventContextAccessor.CurrentContext is not IAdapterEventContext<IAccountEvent> { Event: var accountEvent })
        {
            throw new InvalidStateTypeException(StateKind.AccountState, _eventContextAccessor.CurrentContext);
        }

        return StateKey.FromAccountId(StateKind.AccountState, accountEvent.Account.Id);
    }
}
