using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.State;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Views;

public static class StateManagerExtensions
{
    public static Task<IState<TState>> GetViewStateAsync<TState>(this IStateManager stateManager,
        AdapterIdentifier adapterId,
        Identifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
        where TState : class, new()
    {
        ArgumentNullException.ThrowIfNull(stateManager);

        var key = new StateKey(
            StateKind.State,
            adapterId,
            channelId,
            MessagePartId: messageId.PartIdentifiers[0]
        );

        return stateManager.GetStateAsync<TState>(key, cancellationToken);
    }

    public static Task<IState<TState>> GetViewStateAsync<TState>(this IStateManager stateManager,
        GlobalIdentifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
        where TState : class, new()
    {
        return stateManager.GetViewStateAsync<TState>(channelId.AdapterId, channelId, messageId, cancellationToken);
    }

    public static Task<IState<TState>> GetViewStateAsync<TState>(this IStateManager stateManager,
        GlobalMessageIdentifier messageId, CancellationToken cancellationToken = default)
        where TState : class, new()
    {
        return stateManager.GetViewStateAsync<TState>(messageId.ChannelId, messageId, cancellationToken);
    }
}
